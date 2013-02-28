<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
  Inherits System.Windows.Forms.Form

  'Form overrides dispose to clean up the component list.
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Required by the Windows Form Designer
  Private components As System.ComponentModel.IContainer

  'NOTE: The following procedure is required by the Windows Form Designer
  'It can be modified using the Windows Form Designer.  
  'Do not modify it using the code editor.
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
    Me.cboTournaments = New System.Windows.Forms.ComboBox()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.txtCallStatus = New System.Windows.Forms.ToolStripStatusLabel()
    Me.chkRefeshCurrentRoundOnly = New System.Windows.Forms.CheckBox()
    Me.txtYear = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.chkAutoRefresh = New System.Windows.Forms.CheckBox()
    Me.txtRefreshInt = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lstPlayers = New System.Windows.Forms.ListView()
    Me.colPos = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colID = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colPlayer = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colScore = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.colThru = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
    Me.btnChange = New System.Windows.Forms.Button()
    Me.btnRefreshSelected = New System.Windows.Forms.Button()
    Me.btnRefreshPlayerList = New System.Windows.Forms.Button()
    Me.txtDatabase = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.btnSaveDb = New System.Windows.Forms.Button()
    Me.StatusStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cboTournaments
    '
    Me.cboTournaments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboTournaments.FormattingEnabled = True
    Me.cboTournaments.Location = New System.Drawing.Point(120, 12)
    Me.cboTournaments.Name = "cboTournaments"
    Me.cboTournaments.Size = New System.Drawing.Size(378, 21)
    Me.cboTournaments.TabIndex = 1
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(140, 44)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(119, 29)
    Me.Button2.TabIndex = 2
    Me.Button2.Text = "Refresh Leaderboard"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(15, 43)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(119, 29)
    Me.Button1.TabIndex = 4
    Me.Button1.Text = "Refresh Tee Times"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtCallStatus})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 466)
    Me.StatusStrip1.Name = "StatusStrip1"
    Me.StatusStrip1.Size = New System.Drawing.Size(579, 22)
    Me.StatusStrip1.TabIndex = 5
    Me.StatusStrip1.Text = "StatusStrip1"
    '
    'txtCallStatus
    '
    Me.txtCallStatus.Name = "txtCallStatus"
    Me.txtCallStatus.Size = New System.Drawing.Size(0, 17)
    '
    'chkRefeshCurrentRoundOnly
    '
    Me.chkRefeshCurrentRoundOnly.AutoSize = True
    Me.chkRefeshCurrentRoundOnly.Location = New System.Drawing.Point(15, 88)
    Me.chkRefeshCurrentRoundOnly.Name = "chkRefeshCurrentRoundOnly"
    Me.chkRefeshCurrentRoundOnly.Size = New System.Drawing.Size(151, 17)
    Me.chkRefeshCurrentRoundOnly.TabIndex = 6
    Me.chkRefeshCurrentRoundOnly.Text = "Refresh current round only"
    Me.chkRefeshCurrentRoundOnly.UseVisualStyleBackColor = True
    '
    'txtYear
    '
    Me.txtYear.Location = New System.Drawing.Point(50, 12)
    Me.txtYear.Name = "txtYear"
    Me.txtYear.Size = New System.Drawing.Size(64, 20)
    Me.txtYear.TabIndex = 7
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Location = New System.Drawing.Point(12, 15)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(32, 13)
    Me.Label2.TabIndex = 9
    Me.Label2.Text = "Year:"
    '
    'chkAutoRefresh
    '
    Me.chkAutoRefresh.AutoSize = True
    Me.chkAutoRefresh.Location = New System.Drawing.Point(174, 86)
    Me.chkAutoRefresh.Name = "chkAutoRefresh"
    Me.chkAutoRefresh.Size = New System.Drawing.Size(112, 17)
    Me.chkAutoRefresh.TabIndex = 10
    Me.chkAutoRefresh.Text = "Auto refresh every"
    Me.chkAutoRefresh.UseVisualStyleBackColor = True
    '
    'txtRefreshInt
    '
    Me.txtRefreshInt.Location = New System.Drawing.Point(285, 85)
    Me.txtRefreshInt.Name = "txtRefreshInt"
    Me.txtRefreshInt.Size = New System.Drawing.Size(34, 20)
    Me.txtRefreshInt.TabIndex = 11
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Location = New System.Drawing.Point(321, 87)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(29, 13)
    Me.Label1.TabIndex = 12
    Me.Label1.Text = "secs"
    '
    'lstPlayers
    '
    Me.lstPlayers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colPos, Me.colID, Me.colPlayer, Me.colScore, Me.colThru})
    Me.lstPlayers.FullRowSelect = True
    Me.lstPlayers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable
    Me.lstPlayers.Location = New System.Drawing.Point(15, 121)
    Me.lstPlayers.Name = "lstPlayers"
    Me.lstPlayers.Size = New System.Drawing.Size(539, 307)
    Me.lstPlayers.TabIndex = 13
    Me.lstPlayers.UseCompatibleStateImageBehavior = False
    Me.lstPlayers.View = System.Windows.Forms.View.Details
    '
    'colPos
    '
    Me.colPos.Text = ""
    Me.colPos.Width = 50
    '
    'colID
    '
    Me.colID.Text = "ID"
    '
    'colPlayer
    '
    Me.colPlayer.Text = ""
    Me.colPlayer.Width = 295
    '
    'colScore
    '
    Me.colScore.Text = "Total"
    '
    'colThru
    '
    Me.colThru.Text = "Thru"
    '
    'btnChange
    '
    Me.btnChange.Location = New System.Drawing.Point(504, 11)
    Me.btnChange.Name = "btnChange"
    Me.btnChange.Size = New System.Drawing.Size(63, 23)
    Me.btnChange.TabIndex = 14
    Me.btnChange.Text = "Change"
    Me.btnChange.UseVisualStyleBackColor = True
    '
    'btnRefreshSelected
    '
    Me.btnRefreshSelected.Location = New System.Drawing.Point(416, 433)
    Me.btnRefreshSelected.Name = "btnRefreshSelected"
    Me.btnRefreshSelected.Size = New System.Drawing.Size(138, 29)
    Me.btnRefreshSelected.TabIndex = 15
    Me.btnRefreshSelected.Text = "Refresh Selected Players"
    Me.btnRefreshSelected.UseVisualStyleBackColor = True
    '
    'btnRefreshPlayerList
    '
    Me.btnRefreshPlayerList.Location = New System.Drawing.Point(15, 433)
    Me.btnRefreshPlayerList.Name = "btnRefreshPlayerList"
    Me.btnRefreshPlayerList.Size = New System.Drawing.Size(119, 29)
    Me.btnRefreshPlayerList.TabIndex = 16
    Me.btnRefreshPlayerList.Text = "Refresh Player List"
    Me.btnRefreshPlayerList.UseVisualStyleBackColor = True
    '
    'txtDatabase
    '
    Me.txtDatabase.Location = New System.Drawing.Point(406, 51)
    Me.txtDatabase.Name = "txtDatabase"
    Me.txtDatabase.Size = New System.Drawing.Size(96, 20)
    Me.txtDatabase.TabIndex = 17
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Location = New System.Drawing.Point(315, 54)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(90, 13)
    Me.Label3.TabIndex = 18
    Me.Label3.Text = "Database Server:"
    '
    'btnSaveDb
    '
    Me.btnSaveDb.Location = New System.Drawing.Point(504, 49)
    Me.btnSaveDb.Name = "btnSaveDb"
    Me.btnSaveDb.Size = New System.Drawing.Size(63, 23)
    Me.btnSaveDb.TabIndex = 19
    Me.btnSaveDb.Text = "Save"
    Me.btnSaveDb.UseVisualStyleBackColor = True
    '
    'Main
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(579, 488)
    Me.Controls.Add(Me.btnSaveDb)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.txtDatabase)
    Me.Controls.Add(Me.btnRefreshPlayerList)
    Me.Controls.Add(Me.btnRefreshSelected)
    Me.Controls.Add(Me.btnChange)
    Me.Controls.Add(Me.lstPlayers)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.txtRefreshInt)
    Me.Controls.Add(Me.chkAutoRefresh)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.txtYear)
    Me.Controls.Add(Me.chkRefeshCurrentRoundOnly)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.cboTournaments)
    Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
    Me.Name = "Main"
    Me.Text = "STPGA Scoring"
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cboTournaments As System.Windows.Forms.ComboBox
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents txtCallStatus As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents chkRefeshCurrentRoundOnly As System.Windows.Forms.CheckBox
  Friend WithEvents txtYear As System.Windows.Forms.TextBox
  Friend WithEvents Label2 As System.Windows.Forms.Label
  Friend WithEvents chkAutoRefresh As System.Windows.Forms.CheckBox
  Friend WithEvents txtRefreshInt As System.Windows.Forms.TextBox
  Friend WithEvents Label1 As System.Windows.Forms.Label
  Friend WithEvents lstPlayers As System.Windows.Forms.ListView
  Friend WithEvents colID As System.Windows.Forms.ColumnHeader
  Friend WithEvents colPlayer As System.Windows.Forms.ColumnHeader
  Friend WithEvents colScore As System.Windows.Forms.ColumnHeader
  Friend WithEvents colThru As System.Windows.Forms.ColumnHeader
  Friend WithEvents btnChange As System.Windows.Forms.Button
  Friend WithEvents btnRefreshSelected As System.Windows.Forms.Button
  Friend WithEvents btnRefreshPlayerList As System.Windows.Forms.Button
  Friend WithEvents colPos As System.Windows.Forms.ColumnHeader
  Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
  Friend WithEvents Label3 As System.Windows.Forms.Label
  Friend WithEvents btnSaveDb As System.Windows.Forms.Button

End Class
