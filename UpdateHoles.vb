Public Class UpdateHoles
  Public dbConn As DbConnection

  Public Sub Update(ByVal e As com.disney.corp.stpga.GetHoleByHoleCompletedEventArgs)
    Dim result As com.disney.corp.stpga.HoleByHole = e.Result
    Dim player As com.disney.corp.stpga.HoleByHolePlayer
    Dim hole As com.disney.corp.stpga.Hole
    Dim players As List(Of com.disney.corp.stpga.HoleByHolePlayer) = result.player.ToList
    Dim playerCount As Integer = players.Count
    Dim i As Integer = 0
    Dim currentRound As Integer = dbConn.GetCurrentRound
    Dim driving As String = ""
    Dim fairways As String = ""
    Dim gir As String = ""
    Dim putts As String = ""

    dgvLeaderboard.DataSource = players

    For Each player In players
      i += 1
      For Each round In player.round
        driving = ""
        fairways = ""
        gir = ""
        putts = ""

        If chkRefeshCurrentRoundOnly.Checked = True Then
          If round.round_number = currentRound Then
            For Each hole In round.hole
              dbConn.UpdatePlayerHole(player.playerid, hole.number, round.courseid, round.round_number, hole.strokes)

              If player.currenthole > 0 Then
                driving = round.drivedistavg.ToString
                fairways = round.fairwayshit.ToString
                gir = round.gir.ToString
                putts = round.puttsgir.ToString
              End If

              dbConn.UpdatePlayerRoundStats(player.playerid, round.round_number, driving, fairways, gir, putts)
            Next
          End If
        Else
          For Each hole In round.hole
            dbConn.UpdatePlayerHole(player.playerid, hole.number, round.courseid, round.round_number, hole.strokes)

            If player.currenthole > 0 Then
              driving = round.drivedistavg.ToString
              fairways = round.fairwayshit.ToString
              gir = round.gir.ToString
              putts = round.puttsgir.ToString
            End If

            dbConn.UpdatePlayerRoundStats(player.playerid, round.round_number, driving, fairways, gir, putts)
          Next
        End If
      Next
      txtCallStatus.Text = "Updated (" & i & " of " & playerCount & ") " & player.firstname & " " & player.lastname & " scores"
      Me.Refresh()
    Next

    txtCallStatus.Text = "Rebuilding leaderboard"
    Me.Refresh()
    dbConn.BuildLeaderboard()

    Button2.Enabled = True
    txtCallStatus.Text = "Scores updated at " & Now()
    Me.Refresh()

    If chkAutoRefresh.Checked Then
      refreshTimer.Start()
    End If
  End Sub
  
End Class
