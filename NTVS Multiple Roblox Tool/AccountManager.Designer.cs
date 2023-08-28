namespace NTVS_Multiple_Roblox_Tool
{
    partial class AccountManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dreamForm1 = new ReaLTaiizor.Forms.DreamForm();
            spaceButton2 = new ReaLTaiizor.Controls.SpaceButton();
            listView1 = new ListView();
            foreverContextMenuStrip1 = new ReaLTaiizor.Controls.ForeverContextMenuStrip();
            tESTToolStripMenuItem = new ToolStripMenuItem();
            tESTToolStripMenuItem1 = new ToolStripMenuItem();
            foreverClose1 = new ReaLTaiizor.Controls.ForeverClose();
            dreamForm1.SuspendLayout();
            foreverContextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dreamForm1
            // 
            dreamForm1.ColorA = Color.FromArgb(40, 218, 255);
            dreamForm1.ColorB = Color.FromArgb(63, 63, 63);
            dreamForm1.ColorC = Color.FromArgb(41, 41, 41);
            dreamForm1.ColorD = Color.FromArgb(27, 27, 27);
            dreamForm1.ColorE = Color.FromArgb(0, 0, 0, 0);
            dreamForm1.ColorF = Color.FromArgb(25, 255, 255, 255);
            dreamForm1.Controls.Add(foreverClose1);
            dreamForm1.Controls.Add(spaceButton2);
            dreamForm1.Controls.Add(listView1);
            dreamForm1.Dock = DockStyle.Fill;
            dreamForm1.ForeColor = Color.White;
            dreamForm1.Location = new Point(0, 0);
            dreamForm1.Name = "dreamForm1";
            dreamForm1.Size = new Size(321, 312);
            dreamForm1.TabIndex = 0;
            dreamForm1.TabStop = false;
            dreamForm1.Text = "Account Manager";
            dreamForm1.TitleAlign = HorizontalAlignment.Center;
            dreamForm1.TitleHeight = 25;
            // 
            // spaceButton2
            // 
            spaceButton2.Customization = "Kioq/zIyMv8yMjL/Kioq/y8vL/8nJyf//v7+/yMjI/8qKir/";
            spaceButton2.Font = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point);
            spaceButton2.Image = null;
            spaceButton2.Location = new Point(11, 4);
            spaceButton2.Name = "spaceButton2";
            spaceButton2.NoRounding = false;
            spaceButton2.Size = new Size(72, 17);
            spaceButton2.TabIndex = 2;
            spaceButton2.Text = "Refresh";
            spaceButton2.TextAlignment = HorizontalAlignment.Center;
            spaceButton2.Transparent = false;
            spaceButton2.Click += spaceButton2_Click;
            // 
            // listView1
            // 
            listView1.BackColor = Color.FromArgb(63, 63, 63);
            listView1.BorderStyle = BorderStyle.None;
            listView1.ForeColor = Color.White;
            listView1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            listView1.Location = new Point(0, 45);
            listView1.Name = "listView1";
            listView1.Size = new Size(321, 267);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.List;
            // 
            // foreverContextMenuStrip1
            // 
            foreverContextMenuStrip1.Font = new Font("Segoe UI", 8F, FontStyle.Regular, GraphicsUnit.Point);
            foreverContextMenuStrip1.ForeColor = Color.White;
            foreverContextMenuStrip1.Items.AddRange(new ToolStripItem[] { tESTToolStripMenuItem, tESTToolStripMenuItem1 });
            foreverContextMenuStrip1.Name = "foreverContextMenuStrip1";
            foreverContextMenuStrip1.ShowImageMargin = false;
            foreverContextMenuStrip1.Size = new Size(74, 48);
            // 
            // tESTToolStripMenuItem
            // 
            tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            tESTToolStripMenuItem.Size = new Size(73, 22);
            tESTToolStripMenuItem.Text = "TEST";
            // 
            // tESTToolStripMenuItem1
            // 
            tESTToolStripMenuItem1.Name = "tESTToolStripMenuItem1";
            tESTToolStripMenuItem1.Size = new Size(73, 22);
            tESTToolStripMenuItem1.Text = "TEST";
            // 
            // foreverClose1
            // 
            foreverClose1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            foreverClose1.BackColor = Color.White;
            foreverClose1.BaseColor = Color.FromArgb(45, 47, 49);
            foreverClose1.DefaultLocation = true;
            foreverClose1.DownColor = Color.FromArgb(30, 0, 0, 0);
            foreverClose1.Font = new Font("Marlett", 10F, FontStyle.Regular, GraphicsUnit.Point);
            foreverClose1.Location = new Point(291, 4);
            foreverClose1.Name = "foreverClose1";
            foreverClose1.OverColor = Color.FromArgb(30, 255, 255, 255);
            foreverClose1.Size = new Size(18, 18);
            foreverClose1.TabIndex = 3;
            foreverClose1.Text = "foreverClose1";
            foreverClose1.TextColor = Color.FromArgb(243, 243, 243);
            foreverClose1.Click += foreverClose1_Click;
            // 
            // AccountManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(321, 312);
            Controls.Add(dreamForm1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AccountManager";
            Text = "AccountManager";
            dreamForm1.ResumeLayout(false);
            foreverContextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Forms.DreamForm dreamForm1;
        private ReaLTaiizor.Controls.ForeverContextMenuStrip foreverContextMenuStrip1;
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStripMenuItem tESTToolStripMenuItem1;
        private ListView listView1;
        private ReaLTaiizor.Controls.SpaceButton spaceButton2;
        private ReaLTaiizor.Controls.ForeverClose foreverClose1;
    }
}