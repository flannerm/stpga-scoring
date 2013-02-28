Imports MySql.Data.MySqlClient

Public Class DbConnection
  Private _sqlConnection As MySqlConnection

  Public Sub New()
    Dim conString As String = "Server=" & My.Settings.DatabaseIP & ";Database=golfscoring_ids;UID=root;PWD=espn;PORT=3306"

    Try
      _sqlConnection = New MySqlConnection(conString)
      _sqlConnection.Open()
    Catch ex As Exception
      MsgBox("Error connecting to: " & My.Settings.DatabaseIP)
      _sqlConnection = Nothing
    End Try
  End Sub

  Public Sub ChangeDatabase()
    Dim conString As String = "Server=" & My.Settings.DatabaseIP & ";Database=golfscoring_ids;UID=root;PWD=espn;PORT=3306"

    Try
      If _sqlConnection.State = System.Data.ConnectionState.Open Then
        _sqlConnection.Close()
      End If

      _sqlConnection.ConnectionString = conString
      _sqlConnection.Open()
    Catch ex As Exception

    End Try
  End Sub

  Public Sub UpdatePlayer(ByVal playerId As Integer, ByVal firstName As String, ByVal lastName As String, ByVal status As String)
    Dim sql As String = "select * from players where tvid = " & playerId
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim adp As New MySqlDataAdapter(cmd)
    Dim bldr As New MySqlCommandBuilder(adp)
    Dim tbl As DataTable = New DataTable
    Dim row As DataRow = Nothing

    adp.Fill(tbl)

    If tbl.Rows.Count = 0 Then
      row = tbl.Rows.Add()
      row.Item("playerid") = playerId
      row.Item("tvid") = playerId
      row.Item("countryid") = 36
      row.Item("headshot") = lastName & "_" & firstName
      row.Item("tvname") = lastName
      row.Item("firstname") = firstName
      row.Item("lastname") = lastName
    Else
      row = tbl.Rows(0)
    End If

    row.Item("status") = GetStatus(status)
    row.Item("statusname") = status

    adp.Update(tbl.GetChanges)
    tbl.AcceptChanges()

    cmd.Dispose()
    adp.Dispose()
    bldr.Dispose()
    tbl.Dispose()
  End Sub

  Public Sub UpdatePlayerHole(ByVal tvId As Integer, ByVal holeNum As Integer, ByVal courseId As Integer, ByVal roundNum As Integer, ByVal score As Integer)
    Dim playerId As Integer = GetPlayerId(tvId)

    If playerId > 0 Then
      Dim holeId As Integer = GetHoleId(holeNum, courseId)
      Dim roundId As Integer = GetRoundId(roundNum)

      If holeId > 0 Then
        Dim sql As String = "select * from playerholes where playerid = " & playerId & " and holeid = " & holeId & " and roundid = " & roundId
        Dim cmd As New MySqlCommand(sql, _sqlConnection)
        Dim adp As New MySqlDataAdapter(cmd)
        Dim bldr As New MySqlCommandBuilder(adp)
        Dim tbl As DataTable = New DataTable
        Dim row As DataRow = Nothing

        adp.Fill(tbl)

        If tbl.Rows.Count = 0 Then
          row = tbl.Rows.Add()
          row.Item("playerid") = playerId
          row.Item("holeid") = holeId
          row.Item("roundid") = roundId
        Else
          row = tbl.Rows(0)
        End If

        If row.Item("manualedit").ToString = "" Then
          row.Item("scoreerror") = False
          row.Item("score") = score
        Else
          If row.Item("manualedit") = True Then
            If row.Item("score") <> score And row.Item("score") > 0 Then
              row.Item("scoreerror") = True
            Else
              row.Item("manualedit") = DBNull.Value
              row.Item("scoreerror") = False
              row.Item("score") = score
            End If
          Else
            row.Item("scoreerror") = False
            row.Item("score") = score
          End If
        End If

        adp.Update(tbl.GetChanges)
        tbl.AcceptChanges()

        cmd.Dispose()
        adp.Dispose()
        bldr.Dispose()
        tbl.Dispose()

        UpdatePlayerRounds(playerId, roundId)
      End If
      
    End If
  End Sub

  Public Sub UpdatePlayerRounds(ByVal playerId As Integer, ByVal roundId As Integer)
    Dim i As Integer
    Dim sql As String = ""
    Dim cmd As MySqlCommand
    Dim adp As MySqlDataAdapter
    Dim bldr As MySqlCommandBuilder
    Dim tbl As DataTable
    Dim row As DataRow = Nothing
    Dim rdr As MySqlDataReader
    Dim score As Integer = 0

    sql = "select * from playerrounds where playerid = " & playerId & " and roundid = " & roundId
    cmd = New MySqlCommand(sql, _sqlConnection)
    adp = New MySqlDataAdapter(cmd)
    bldr = New MySqlCommandBuilder(adp)
    tbl = New DataTable

    adp.Fill(tbl)

    If tbl.Rows.Count = 0 Then
      row = tbl.Rows.Add()
      row.Item("playerid") = playerId
      row.Item("roundid") = roundId
    Else
      row = tbl.Rows(0)
    End If

    sql = "select sum(score) from playerholes where playerid = " & playerId & " and roundid = " & roundId
    cmd = New MySqlCommand(sql, _sqlConnection)
    rdr = cmd.ExecuteReader

    If rdr.HasRows Then
      rdr.Read()
      score = rdr(0)
    End If
    rdr.Close()

    row.Item("Score") = score

    adp.Update(tbl.GetChanges)
    tbl.AcceptChanges()

    cmd.Dispose()
    adp.Dispose()
    bldr.Dispose()
    tbl.Dispose()


  End Sub

  Public Sub UpdatePlayerRoundStats(ByVal tvId As Integer, ByVal roundNum As Integer, ByVal driveAvg As String, ByVal fairwayPct As String,
                                    ByVal girPct As String, ByVal putts As String, ByVal fairwayOpp As Integer, ByVal girOpp As Integer,
                                    ByVal longestDrive As String, ByVal fairwaysHit As String, ByVal greensHit As String, ByVal puttsAvg As String)
    Dim playerId As Integer = GetPlayerId(tvId)

    If playerId > 0 Then
      Dim roundId As Integer = GetRoundId(roundNum)
      Dim sql As String = "select * from playerrounds where playerid = " & playerId & " and roundid = " & roundId
      Dim cmd As New MySqlCommand(sql, _sqlConnection)
      Dim adp As New MySqlDataAdapter(cmd)
      Dim bldr As New MySqlCommandBuilder(adp)
      Dim tbl As DataTable = New DataTable
      Dim row As DataRow = Nothing

      adp.Fill(tbl)

      If tbl.Rows.Count = 0 Then
        row = tbl.Rows.Add()
        row.Item("playerid") = playerId
        'row.Item("tvid") = tvId
        row.Item("roundid") = roundId
      Else
        row = tbl.Rows(0)
      End If

      'row.Item("drivingavg") = driveAvg
      'row.Item("fairwayspct") = fairwayPct
      'row.Item("greenspct") = girPct
      'row.Item("putts") = putts

      If fairwayOpp = 0 Then
        If row("fairwayOpp") Is DBNull.Value Then
          'row.Item("fairwayopp") = fairwayOpp
        End If
      Else
        'row.Item("fairwayopp") = fairwayOpp
      End If

      If girOpp = 0 Then
        If row("greensopp") Is DBNull.Value Then
          'row.Item("greensopp") = girOpp
        End If
      Else
        'row.Item("greensopp") = girOpp
      End If

      'row.Item("longestdrive") = longestDrive
      'row.Item("fairwayshit") = fairwaysHit
      'row.Item("greenshit") = greensHit
      'row.Item("puttsavg") = puttsAvg

      adp.Update(tbl.GetChanges)
      tbl.AcceptChanges()

      cmd.Dispose()
      adp.Dispose()
      bldr.Dispose()
      tbl.Dispose()
    End If
  End Sub

  Public Sub UpdateTeeTime(ByVal courseId As Integer, ByVal pairingId As Integer, ByVal roundNum As Integer, ByVal startTime As String, ByVal startTee As Integer, ByVal players As Collection)
    If courseId > 0 Then
      Dim sql As String = "select * from pairings where pairingid = " & pairingId
      Dim cmd As New MySqlCommand(sql, _sqlConnection)
      Dim adp As New MySqlDataAdapter(cmd)
      Dim bldr As New MySqlCommandBuilder(adp)
      Dim tbl As DataTable = New DataTable
      Dim row As DataRow = Nothing
      Dim startTimeTemp As DateTime = Nothing

      adp.Fill(tbl)

      If tbl.Rows.Count = 0 Then
        row = tbl.Rows.Add()
        row.Item("pairingid") = pairingId
      Else
        row = tbl.Rows(0)
      End If

            row.Item("courseid") = 1 'courseId
      row.Item("starttime") = startTime
      row.Item("starthole") = startTee
      row.Item("roundid") = GetRoundId(roundNum)

      adp.Update(tbl.GetChanges)
      tbl.AcceptChanges()

      cmd.Dispose()
      adp.Dispose()
      bldr.Dispose()
      tbl.Dispose()

      Dim i As Integer

      For i = 1 To players.Count
        sql = "select * from playerpairings where pairingid = " & pairingId & " and playerid = " & players.Item(i)
        cmd = New MySqlCommand(sql, _sqlConnection)
        adp = New MySqlDataAdapter(cmd)
        bldr = New MySqlCommandBuilder(adp)
        tbl = New DataTable

        adp.Fill(tbl)

        If tbl.Rows.Count = 0 Then
          row = tbl.Rows.Add
          row.Item("pairingid") = pairingId
          row.Item("playerorder") = i
          row.Item("playerid") = GetPlayerId(players.Item(i))
          adp.Update(tbl.GetChanges)
          tbl.AcceptChanges()
        End If

        cmd.Dispose()
        adp.Dispose()
        bldr.Dispose()
        tbl.Dispose()

      Next
    End If
  End Sub

  Public Function GetPlayers() As DataTable
    Dim sql As String = "CALL sp_GetPlayers(true)"
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim rdr As MySqlDataReader = cmd.ExecuteReader
    Dim tbl As New DataTable

    tbl.Load(rdr)
    rdr.Close()

    Return tbl
  End Function

  Private Function GetPlayerId(ByVal tvID As Integer) As Integer
    Dim sql As String = "select * from players where tvid = " & tvID
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim rdr As MySqlDataReader = cmd.ExecuteReader
    Dim playerId As Integer = 0

    rdr.Read()

    If rdr.HasRows Then
      playerId = rdr("playerid")
    End If

    rdr.Close()
    cmd.Dispose()

    Return playerId
  End Function

  Private Function GetHoleId(ByVal holeNum As Integer, ByVal courseId As Integer) As Integer
    Dim sql As String = "select * from holes where courseid = " & courseId & " and holenum = " & holeNum
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim rdr As MySqlDataReader = cmd.ExecuteReader
    Dim holeId As Integer = 0

    rdr.Read()

    If rdr.HasRows Then
      holeId = rdr("holeid")
    End If

    rdr.Close()
    cmd.Dispose()

    Return holeId
  End Function

  Private Function GetRoundId(ByVal roundNum As Integer) As Integer
    Dim sql As String = "select * from rounds where roundnum = " & roundNum
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim rdr As MySqlDataReader = cmd.ExecuteReader
    Dim round As Integer = 0

    rdr.Read()

    If rdr.HasRows Then
      round = rdr("roundid")
    End If

    rdr.Close()
    cmd.Dispose()

    Return round
  End Function

  Public Function GetCurrentRound() As Integer
    Dim sql As String = "select currentround from tournament"
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim rdr As MySqlDataReader = cmd.ExecuteReader
    Dim round As Integer = 0

    rdr.Read()

    If rdr.HasRows Then
      round = rdr(0)
    End If

    rdr.Close()
    cmd.Dispose()

    Return round
  End Function

  Public Sub BuildLeaderboard()
    Dim sql As String = "call sp_buildleaderboard(0, 'Y', 'Y')"
    Dim cmd As New MySqlCommand(sql, _sqlConnection)

    Try
      cmd.ExecuteNonQuery()
    Catch ex As Exception

    End Try

    cmd.Dispose()
  End Sub

  Private Function GetStatus(ByVal status As String) As Integer
    Select Case status.ToUpper
      Case "P", "UNKNOWN"
        Return 1
      Case "WD"
        Return 3
      Case Else
        Return 2
    End Select
  End Function
End Class
