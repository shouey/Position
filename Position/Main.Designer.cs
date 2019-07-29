namespace Position
{
	// Token: 0x02000002 RID: 2
	[global::System.Runtime.InteropServices.ComVisible(true)]
	public partial class Main : global::System.Windows.Forms.Form
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002218 File Offset: 0x00000418
		protected override void Dispose(bool disposing)
		{
			bool flag = disposing && this.components != null;
			if (flag)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002250 File Offset: 0x00000450
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Position.Main));
			this.rtbxLog = new global::System.Windows.Forms.TextBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.reload = new global::System.Windows.Forms.Button();
			this.button1 = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.rtbxLog.BackColor = global::System.Drawing.SystemColors.Control;
			this.rtbxLog.Font = new global::System.Drawing.Font("微软雅黑", 9.5f);
			this.rtbxLog.Location = new global::System.Drawing.Point(4, 502);
			this.rtbxLog.Multiline = true;
			this.rtbxLog.Name = "rtbxLog";
			this.rtbxLog.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.rtbxLog.Size = new global::System.Drawing.Size(560, 92);
			this.rtbxLog.TabIndex = 9;
			this.panel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.panel1.Location = new global::System.Drawing.Point(2, 3);
			this.panel1.Margin = new global::System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(700, 500);
			this.panel1.TabIndex = 10;
			this.reload.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.reload.Location = new global::System.Drawing.Point(568, 515);
			this.reload.Name = "reload";
			this.reload.Size = new global::System.Drawing.Size(135, 30);
			this.reload.TabIndex = 13;
			this.reload.Text = "刷新网页";
			this.reload.UseVisualStyleBackColor = true;
			this.reload.Click += new global::System.EventHandler(this.reload_Click);
			this.button1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.button1.Location = new global::System.Drawing.Point(568, 555);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(135, 30);
			this.button1.TabIndex = 12;
			this.button1.Text = "还原定位";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.clear_Click);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new global::System.Drawing.Size(704, 597);
			base.Controls.Add(this.reload);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.rtbxLog);
			base.Icon = (global::System.Drawing.Icon)resources.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Main";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Position";
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
			base.Load += new global::System.EventHandler(this.frmMain_Load);
			base.Paint += new global::System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000006 RID: 6
		private global::System.ComponentModel.IContainer components = null;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.TextBox rtbxLog;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.Button reload;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.Button button1;
	}
}
