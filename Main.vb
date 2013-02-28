Imports System.Environment
Imports System.ComponentModel

Public Class Main
  Private WithEvents svcPGA As com.disney.corp.stpga.PGAWebService = New com.disney.corp.stpga.PGAWebService
  Private dbConn As New DbConnection()
  Private _loading As Boolean = True
  Private _selectedPlayers As New List(Of ListViewItem)
  Private WithEvents refreshTimer As System.Timers.Timer
  Private Delegate Sub RefreshLeaderboardDelegate()
  Private Delegate Sub UpdateGridDelegate(ByVal players As List(Of com.disney.corp.stpga.HoleByHolePlayer))
  Private Delegate Sub UpdateStatusDelegate(ByVal status As String)
  Private Delegate Sub UpdateFinishedDelegate()

  Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    txtDatabase.Text = My.Settings.DatabaseIP

    cboTournaments.Enabled = False

    _loading = True

    txtYear.Text = My.Settings.Year
    txtRefreshInt.Text = My.Settings.RefreshInterval.ToString

    ' Add any initialization after the InitializeComponent() call.
    svcPGA.GetTournamentsAsync(My.Settings.Year)
    chkRefeshCurrentRoundOnly.Checked = My.Settings.RefreshCurrentRoundOnly

    refreshTimer = New Timers.Timer(My.Settings.RefreshInterval * 1000)
    ' refreshTimer.Start()

    LoadPlayers()
  End Sub

  Private Sub LoadPlayers()
    Dim row As DataRow
    Dim item As ListViewItem

    lstPlayers.Items.Clear()

    For Each row In dbConn.GetPlayers.Rows
      item = New ListViewItem(row("pos").ToString)
      item.SubItems.Add(row("playerid").ToString)
      item.SubItems.Add(row("firstname").ToString & " " & row("lastname").ToString)
      item.SubItems.Add(row("total").ToString)
      item.SubItems.Add(row("hole").ToString)
      lstPlayers.Items.Add(item)
    Next
  End Sub

  Private Sub svcPGA_GetHoleByHoleCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetHoleByHoleCompletedEventArgs) Handles svcPGA.GetHoleByHoleCompleted
    Dim result As com.disney.corp.stpga.HoleByHole = e.Result
    Dim player As com.disney.corp.stpga.HoleByHolePlayer
    Dim hole As com.disney.corp.stpga.Hole
    Dim players As List(Of com.disney.corp.stpga.HoleByHolePlayer) = result.player.ToList
    Dim playerCount As Integer = players.Count
    Dim i As Integer = 0
    Dim currentRound As Integer = dbConn.GetCurrentRound
    Dim driving As String = ""
    Dim longestDrive As String = ""
    Dim fairways As String = ""
    Dim gir As String = ""
    Dim putts As String = ""
    Dim fairwayOpp As Double = 0
    Dim girOpp As Double = 0
    Dim fairwaysHit As String = ""
    Dim greensHit As String = ""
    Dim puttsAvg As String = ""
    Dim update As Boolean
    Dim item As ListViewItem

    Invoke(New UpdateGridDelegate(AddressOf UpdateGrid), players)

    For Each player In players
      i += 1

      update = False

      If _selectedPlayers.Count > 0 Then
        For Each item In _selectedPlayers
          If item.Text = player.playerid.ToString Then
            update = True
          End If
        Next
      Else
        update = True
      End If

      If Not player.round Is Nothing Then
        If update = True Then
          For Each round In player.round
            driving = ""
            fairways = ""
            gir = ""
            putts = ""
            fairwayOpp = 0
            girOpp = 0
            fairwaysHit = ""
            greensHit = ""
            puttsAvg = ""

            If My.Settings.RefreshCurrentRoundOnly = True Then
              If round.round_number = currentRound Then
                For Each hole In round.hole
                  dbConn.UpdatePlayerHole(player.playerid, hole.number, 1, round.round_number, hole.strokes)

                  If player.currenthole > 0 Then
                    driving = round.drivedistavg.ToString
                    fairways = round.fairwayshit.ToString
                    gir = round.gir.ToString
                    putts = round.puttsgir.ToString
                    fairwaysHit = round.fairwayshit.ToString
                    greensHit = ""
                    puttsAvg = round.avgputtsgir

                    If IsNumeric(round.fairwaysopp.ToString) Then
                      fairwayOpp = 0
                    Else
                      fairwayOpp = CDbl(round.fairwaysopp.ToString)
                    End If

                    If IsNumeric(round.giropp.ToString) Then
                      girOpp = 0
                    Else
                      girOpp = CDbl(round.giropp.ToString)
                    End If

                    longestDrive = round.longestdrive.ToString
                  End If

                  'dbConn.UpdatePlayerRoundStats(player.playerid, round.round_number, driving, fairways, gir, putts, fairwayOpp, girOpp, longestDrive, fairwaysHit, greensHit, puttsAvg)
                Next
              End If
            Else
              For Each hole In round.hole
                                dbConn.UpdatePlayerHole(player.playerid, hole.number, 1, round.round_number, hole.strokes)

                If player.currenthole > 0 Then
                  driving = round.drivedistavg.ToString
                  fairways = round.fairwayshit.ToString
                  gir = round.gir.ToString
                  putts = round.puttsgir.ToString
                  fairwaysHit = round.fairwayshit.ToString
                  greensHit = ""
                  puttsAvg = round.avgputtsgir

                  If IsNumeric(round.fairwaysopp.ToString) Then
                    fairwayOpp = 0
                  Else
                    fairwayOpp = CDbl(round.fairwaysopp.ToString)
                  End If

                  If IsNumeric(round.giropp.ToString) Then
                    girOpp = 0
                  Else
                    girOpp = CDbl(round.giropp.ToString)
                  End If

                  longestDrive = round.longestdrive.ToString
                End If

                'dbConn.UpdatePlayerRoundStats(player.playerid, round.round_number, driving, fairways, gir, putts, fairwayOpp, girOpp, longestDrive, fairwaysHit, greensHit, puttsAvg)
              Next
            End If
          Next

          Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Updated (" & i & " of " & playerCount & ") " & player.firstname & " " & player.lastname & " scores")

        End If 'update = true
      End If

    Next

    Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Rebuilding leaderboard")

    dbConn.BuildLeaderboard()

    Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Scores updated at " & Now())

    Invoke(New UpdateFinishedDelegate(AddressOf UpdateFinished))

    _selectedPlayers.Clear()
  End Sub

  Private Sub UpdateGrid(ByVal players As List(Of com.disney.corp.stpga.HoleByHolePlayer))
    'dgvLeaderboard.DataSource = players
    LoadPlayers()
  End Sub

  Private Sub UpdateStatus(ByVal status As String)
    txtCallStatus.Text = status
    Me.Refresh()
  End Sub

  Private Sub UpdateFinished()
    Button2.Enabled = True
    Button1.Enabled = True
    btnRefreshSelected.Enabled = True

    LoadPlayers()

    If chkAutoRefresh.Checked Then
      refreshTimer.Start()
    End If
  End Sub

  Private Sub svcPGA_GetTeeTimesCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetTeeTimesCompletedEventArgs) Handles svcPGA.GetTeeTimesCompleted
    Dim result As com.disney.corp.stpga.TeeTimes = e.Result
    Dim teeTime As com.disney.corp.stpga.TeeTime = Nothing
    Dim player As com.disney.corp.stpga.TeetimePlayer = Nothing
    Dim startTime As String
    Dim players As New Collection
    Dim currentRound As Integer = dbConn.GetCurrentRound

    Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Parsing tee times")

    For Each teeTime In result.teetime
      If My.Settings.RefreshCurrentRoundOnly = True Then
        If teeTime.round = currentRound Then
          For Each player In teeTime.golfer
            players.Add(player.player_id)
            dbConn.UpdatePlayer(player.player_id, player.firstname, player.lastname, player.playerstatus)
            Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Updated " & player.firstname & " " & player.lastname)
          Next

          startTime = FormatDateTime(CDate(teeTime.teedatetime), DateFormat.ShortTime).ToString

          dbConn.UpdateTeeTime(teeTime.courseid, teeTime.round.ToString() & teeTime.group.ToString(), teeTime.round, startTime, teeTime.starttee, players)
        End If
      Else
        For Each player In teeTime.golfer
          players.Add(player.player_id)
          dbConn.UpdatePlayer(player.player_id, player.firstname, player.lastname, player.playerstatus)
          Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Updated " & player.firstname & " " & player.lastname)
        Next

        startTime = FormatDateTime(CDate(teeTime.teedatetime), DateFormat.ShortTime).ToString

        dbConn.UpdateTeeTime(teeTime.courseid, teeTime.round.ToString() & teeTime.group.ToString(), teeTime.round, startTime, teeTime.starttee, players)
      End If

      Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Updated round " & teeTime.round & " " & startTime & " starting time")

      players.Clear()
    Next

    Invoke(New UpdateStatusDelegate(AddressOf UpdateStatus), "Tee times updated at " & Now())

    Invoke(New UpdateFinishedDelegate(AddressOf UpdateFinished))

    'tGetTeeTimes.Abort()
  End Sub

  Private Sub svcPGA_GetTournamentsCompleted(ByVal sender As Object, ByVal e As com.disney.corp.stpga.GetTournamentsCompletedEventArgs) Handles svcPGA.GetTournamentsCompleted
    Dim result As com.disney.corp.stpga.TournamentInfo = e.Result
    Dim tourneys As List(Of com.disney.corp.stpga.Tournament) = result.tournament.ToList
    Dim i As Integer

    _loading = True

    cboTournaments.DataSource = tourneys
    cboTournaments.DisplayMember = "name"
    cboTournaments.ValueMember = "tourn_id"

    If My.Settings.TournamentId > 0 Then
      For i = 0 To cboTournaments.Items.Count - 1
        If cboTournaments.Items(i).tourn_id = My.Settings.TournamentId Then
          cboTournaments.SelectedIndex = i
          Exit For
        End If
      Next
    End If

    _loading = False
  End Sub

  Private Sub LoadCurrentTourney()
    If My.Settings.TournamentId > 0 Then
      cboTournaments.SelectedValue = My.Settings.TournamentId
    End If
  End Sub

  Private Sub cboTournaments_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTournaments.SelectedIndexChanged
    If _loading = False Then
      cboTournaments.Enabled = False
      Dim tourney As com.disney.corp.stpga.Tournament = cboTournaments.SelectedItem
      If Not tourney Is Nothing Then
        My.Settings.TournamentId = tourney.tourn_id
        My.Settings.Save()
      End If
    End If
  End Sub

  Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
    RefreshLeaderboard()
  End Sub

  Private Sub RefreshLeaderboard()
    Button2.Enabled = False
    txtCallStatus.Text = "Refreshing scores"
    Me.Refresh()

    Dim tGetHoles As New Threading.Thread(AddressOf svcPGA.GetHoleByHoleAsync)
    tGetHoles.Start(My.Settings.TournamentId)
  End Sub

  Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
    txtCallStatus.Text = "Refreshing tee times"
    Me.Refresh()

    Dim tGetTeeTimes As New Threading.Thread(AddressOf svcPGA.GetTeeTimesAsync)
    tGetTeeTimes.Start(My.Settings.TournamentId)
  End Sub

  Private Sub chkRefeshCurrentRoundOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRefeshCurrentRoundOnly.CheckedChanged

  End Sub

  Private Sub chkRefeshCurrentRoundOnly_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkRefeshCurrentRoundOnly.Click
    My.Settings.RefreshCurrentRoundOnly = chkRefeshCurrentRoundOnly.Checked
    My.Settings.Save()
  End Sub

  Private Sub refreshTimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs) Handles refreshTimer.Elapsed
    refreshTimer.Stop()

    If chkAutoRefresh.Checked Then
      Invoke(New RefreshLeaderboardDelegate(AddressOf RefreshLeaderboard))
    End If
  End Sub

  Private Sub txtRefreshInt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRefreshInt.TextChanged
    If _loading = False Then
      If Not IsNumeric(txtRefreshInt.Text) Then
        txtRefreshInt.Text = "60"
      End If

      My.Settings.RefreshInterval = CDbl(txtRefreshInt.Text)
      My.Settings.Save()

      refreshTimer.Interval = (My.Settings.RefreshInterval * 1000)
    End If
  End Sub

  Private Sub chkAutoRefresh_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAutoRefresh.CheckedChanged
    If chkAutoRefresh.Checked Then
      Invoke(New RefreshLeaderboardDelegate(AddressOf RefreshLeaderboard))
    End If
  End Sub

  Private Sub txtYear_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtYear.TextChanged
    If _loading = False Then
      My.Settings.Year = txtYear.Text
      My.Settings.Save()
    End If
  End Sub

  Private Sub btnChange_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnChange.Click
    cboTournaments.Enabled = True
  End Sub

  Private Sub btnRefreshSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshSelected.Click
    For Each item In lstPlayers.SelectedItems
      _selectedPlayers.Add(item)
    Next

    Button2.Enabled = False
    btnRefreshSelected.Enabled = False
    txtCallStatus.Text = "Refreshing scores"
    Me.Refresh()

    Dim tGetHoles As New Threading.Thread(AddressOf svcPGA.GetHoleByHoleAsync)
    tGetHoles.Start(My.Settings.TournamentId)
  End Sub

  Private Sub btnRefreshPlayerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefreshPlayerList.Click
    LoadPlayers()
  End Sub

  Private Sub btnSaveDb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveDb.Click
    My.Settings.DatabaseIP = txtDatabase.Text
    My.Settings.Save()

    dbConn.ChangeDatabase()

    LoadPlayers()
  End Sub
End Class
