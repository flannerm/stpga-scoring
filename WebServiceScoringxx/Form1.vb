Imports System.Environment

Public Class Form1
  Private WithEvents svcPGA As com.disney.corp.stpga.PGAWebService = New com.disney.corp.stpga.PGAWebService
  Private dbConn As New DbConnection()
  Private _loading As Boolean = True

  Private Sub svcPGA_GetHoleByHoleCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetHoleByHoleCompletedEventArgs) Handles svcPGA.GetHoleByHoleCompleted
    Dim result As com.disney.corp.stpga.HoleByHole = e.Result
    Dim player As com.disney.corp.stpga.HoleByHolePlayer
    Dim hole As com.disney.corp.stpga.Hole
    Dim players As List(Of com.disney.corp.stpga.HoleByHolePlayer) = result.player.ToList
    Dim playerCount As Integer = players.Count
    Dim i As Integer = 0
    Dim currentRound As Integer = dbConn.GetCurrentRound

    dgvLeaderboard.DataSource = players

    For Each player In players
      i += 1
      For Each round In player.round
        If chkRefeshCurrentRoundOnly.Checked = True Then
          If round.round_number = currentRound Then
            For Each hole In round.hole
              dbConn.UpdatePlayerHole(player.playerid, hole.number, round.courseid, round.round_number, hole.strokes)
            Next
          End If
        Else
          For Each hole In round.hole
            dbConn.UpdatePlayerHole(player.playerid, hole.number, round.courseid, round.round_number, hole.strokes)
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
  End Sub

  Private Sub svcPGA_GetTeeTimesCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetTeeTimesCompletedEventArgs) Handles svcPGA.GetTeeTimesCompleted
    Dim result As com.disney.corp.stpga.TeeTimes = e.Result
    Dim teeTime As com.disney.corp.stpga.TeeTime = Nothing
    Dim player As com.disney.corp.stpga.TeetimePlayer = Nothing
    Dim startTime As String
    Dim players As New Collection

    txtCallStatus.Text = "Parsing tee times"
    Me.Refresh()

    For Each teeTime In result.teetime
      For Each player In teeTime.golfer
        players.Add(player.player_id)
        dbConn.UpdatePlayer(player.player_id, player.firstname, player.lastname, player.playerstatus)
        txtCallStatus.Text = "Updated " & player.firstname & " " & player.lastname
        Me.Refresh()
      Next

      startTime = CDate(teeTime.teedatetime).ToShortTimeString
      dbConn.UpdateTeeTime(teeTime.courseid, teeTime.group, teeTime.round, startTime, teeTime.starttee, players)
      txtCallStatus.Text = "Updated round " & teeTime.round & " " & startTime & " starting time"
      Me.Refresh()

      players.Clear()
    Next

    Button1.Enabled = True
    txtCallStatus.Text = "Tee times updated at " & Now()
    Me.Refresh()
  End Sub

  Private Sub svcPGA_GetTournamentsCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetTournamentsCompletedEventArgs) Handles svcPGA.GetTournamentsCompleted
    Dim result As com.disney.corp.stpga.TournamentInfo = e.Result
    Dim tourneys As List(Of com.disney.corp.stpga.Tournament) = result.tournament.ToList

    'tourneys(0).pgatourn_id

    cboTournaments.DataSource = tourneys
    cboTournaments.DisplayMember = "name"
    cboTournaments.ValueMember = "tourn_id"


    LoadCurrentTourney()

    'cboTournaments.ValueMember = "event_id"
  End Sub

  Private Sub LoadCurrentTourney()
    If My.Settings.TournamentId > 0 Then
      cboTournaments.SelectedValue = My.Settings.TournamentId
    End If
  End Sub

  Private Sub cboTournaments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTournaments.SelectedIndexChanged
     Dim tourney As com.disney.corp.stpga.Tournament = cboTournaments.SelectedItem
      If Not tourney Is Nothing Then
        My.Settings.TournamentId = tourney.tourn_id
        My.Settings.Save()
    End If
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    Button2.Enabled = False
    txtCallStatus.Text = "Refreshing scores"
    Me.Refresh()
    svcPGA.GetHoleByHoleAsync(My.Settings.TournamentId)
  End Sub

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    svcPGA.GetTournamentsAsync(My.Settings.Year)
    chkRefeshCurrentRoundOnly.Checked = My.Settings.RefreshCurrentRoundOnly

  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    txtCallStatus.Text = "Refreshing tee times"
    Me.Refresh()

    svcPGA.GetTeeTimesAsync(My.Settings.TournamentId)
  End Sub

  Private Sub chkRefeshCurrentRoundOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRefeshCurrentRoundOnly.CheckedChanged

  End Sub

  Private Sub chkRefeshCurrentRoundOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRefeshCurrentRoundOnly.Click
    My.Settings.RefreshCurrentRoundOnly = chkRefeshCurrentRoundOnly.Checked
    My.Settings.Save()
  End Sub
End Class
