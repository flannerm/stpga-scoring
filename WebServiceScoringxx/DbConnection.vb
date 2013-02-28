Imports MySql.Data.MySqlClient

Public Class DbConnection
  Private _sqlConnection As MySqlConnection

  Public Sub New()
    Dim conString As String = "Server=localhost;Database=golfscoring_ids;UID=root;PWD=espn;PORT=3306"

    _sqlConnection = New MySqlConnection(conString)
    _sqlConnection.Open()
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
    Else
      row = tbl.Rows(0)
    End If

    row.Item("firstname") = firstName
    row.Item("lastname") = lastName
    row.Item("tvname") = lastName
    row.Item("country") = "UNITED_STATES"
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
    Dim holeId As Integer = GetHoleId(holeNum, courseId)
    Dim roundId As Integer = GetRoundId(roundNum)
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

    row.Item("score") = score

    adp.Update(tbl.GetChanges)
    tbl.AcceptChanges()

    cmd.Dispose()
    adp.Dispose()
    bldr.Dispose()
    tbl.Dispose()
  End Sub

  Public Sub UpdateTeeTime(ByVal courseId As Integer, ByVal pairingId As Integer, ByVal roundNum As Integer, ByVal startTime As String, ByVal startTee As Integer, ByVal players As Collection)
    Dim sql As String = "select * from pairings where pairingid = " & pairingId
    Dim cmd As New MySqlCommand(sql, _sqlConnection)
    Dim adp As New MySqlDataAdapter(cmd)
    Dim bldr As New MySqlCommandBuilder(adp)
    Dim tbl As DataTable = New DataTable
    Dim row As DataRow = Nothing

    adp.Fill(tbl)

    If tbl.Rows.Count = 0 Then
      row = tbl.Rows.Add()
      row.Item("pairingid") = pairingId
    Else
      row = tbl.Rows(0)
    End If

    row.Item("courseid") = courseId
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
        row.Item("playerid") = players.Item(i)
        adp.Update(tbl.GetChanges)
        tbl.AcceptChanges()
      End If

      cmd.Dispose()
      adp.Dispose()
      bldr.Dispose()
      tbl.Dispose()

    Next
  End Sub

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
    cmd.ExecuteNonQuery()
  End Sub

  Private Function GetStatus(ByVal status As String) As Integer
    Select Case status.ToUpper
      Case "P"
        Return 1
      Case Else
        Return 2
    End Select
  End Function
End Class
