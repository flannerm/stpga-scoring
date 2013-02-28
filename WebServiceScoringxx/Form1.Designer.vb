<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
    Me.cboTournaments = New System.Windows.Forms.ComboBox()
    Me.Button2 = New System.Windows.Forms.Button()
    Me.dgvLeaderboard = New System.Windows.Forms.DataGridView()
    Me.Button1 = New System.Windows.Forms.Button()
    Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
    Me.txtCallStatus = New System.Windows.Forms.ToolStripStatusLabel()
    Me.chkRefeshCurrentRoundOnly = New System.Windows.Forms.CheckBox()
    CType(Me.dgvLeaderboard, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.StatusStrip1.SuspendLayout()
    Me.SuspendLayout()
    '
    'cboTournaments
    '
    Me.cboTournaments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.cboTournaments.FormattingEnabled = True
    Me.cboTournaments.Location = New System.Drawing.Point(12, 67)
    Me.cboTournaments.Name = "cboTournaments"
    Me.cboTournaments.Size = New System.Drawing.Size(392, 21)
    Me.cboTournaments.TabIndex = 1
    '
    'Button2
    '
    Me.Button2.Location = New System.Drawing.Point(137, 12)
    Me.Button2.Name = "Button2"
    Me.Button2.Size = New System.Drawing.Size(119, 29)
    Me.Button2.TabIndex = 2
    Me.Button2.Text = "Refresh Leaderboard"
    Me.Button2.UseVisualStyleBackColor = True
    '
    'dgvLeaderboard
    '
    Me.dgvLeaderboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvLeaderboard.Location = New System.Drawing.Point(12, 105)
    Me.dgvLeaderboard.Name = "dgvLeaderboard"
    Me.dgvLeaderboard.Size = New System.Drawing.Size(555, 307)
    Me.dgvLeaderboard.TabIndex = 3
    '
    'Button1
    '
    Me.Button1.Location = New System.Drawing.Point(12, 12)
    Me.Button1.Name = "Button1"
    Me.Button1.Size = New System.Drawing.Size(119, 29)
    Me.Button1.TabIndex = 4
    Me.Button1.Text = "Refresh Tee Times"
    Me.Button1.UseVisualStyleBackColor = True
    '
    'StatusStrip1
    '
    Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtCallStatus})
    Me.StatusStrip1.Location = New System.Drawing.Point(0, 424)
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
    Me.chkRefeshCurrentRoundOnly.Location = New System.Drawing.Point(274, 19)
    Me.chkRefeshCurrentRoundOnly.Name = "chkRefeshCurrentRoundOnly"
    Me.chkRefeshCurrentRoundOnly.Size = New System.Drawing.Size(151, 17)
    Me.chkRefeshCurrentRoundOnly.TabIndex = 6
    Me.chkRefeshCurrentRoundOnly.Text = "Refresh current round only"
    Me.chkRefeshCurrentRoundOnly.UseVisualStyleBackColor = True
    '
    'Form1
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(579, 446)
    Me.Controls.Add(Me.chkRefeshCurrentRoundOnly)
    Me.Controls.Add(Me.StatusStrip1)
    Me.Controls.Add(Me.Button1)
    Me.Controls.Add(Me.dgvLeaderboard)
    Me.Controls.Add(Me.Button2)
    Me.Controls.Add(Me.cboTournaments)
    Me.Name = "Form1"
    Me.Text = "Form1"
    CType(Me.dgvLeaderboard, System.ComponentModel.ISupportInitialize).EndInit()
    Me.StatusStrip1.ResumeLayout(False)
    Me.StatusStrip1.PerformLayout()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents cboTournaments As System.Windows.Forms.ComboBox
  Friend WithEvents Button2 As System.Windows.Forms.Button
  Friend WithEvents dgvLeaderboard As System.Windows.Forms.DataGridView
  Friend WithEvents Button1 As System.Windows.Forms.Button
  Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
  Friend WithEvents txtCallStatus As System.Windows.Forms.ToolStripStatusLabel
  Friend WithEvents chkRefeshCurrentRoundOnly As System.Windows.Forms.CheckBox

End Class
