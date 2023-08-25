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
            foreverListBox1 = new ReaLTaiizor.Controls.ForeverListBox();
            foreverContextMenuStrip1 = new ReaLTaiizor.Controls.ForeverContextMenuStrip();
            tESTToolStripMenuItem = new ToolStripMenuItem();
            tESTToolStripMenuItem1 = new ToolStripMenuItem();
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
            dreamForm1.Controls.Add(foreverListBox1);
            dreamForm1.Dock = DockStyle.Fill;
            dreamForm1.Location = new Point(0, 0);
            dreamForm1.Name = "dreamForm1";
            dreamForm1.Size = new Size(321, 312);
            dreamForm1.TabIndex = 0;
            dreamForm1.TabStop = false;
            dreamForm1.Text = "Account Manager";
            dreamForm1.TitleAlign = HorizontalAlignment.Center;
            dreamForm1.TitleHeight = 25;
            dreamForm1.Enter += dreamForm1_Enter;
            // 
            // foreverListBox1
            // 
            foreverListBox1.BackColor = Color.FromArgb(45, 47, 49);
            foreverListBox1.items = new string[] { "" };
            foreverListBox1.Location = new Point(0, 30);
            foreverListBox1.Name = "foreverListBox1";
            foreverListBox1.SelectedColor = Color.FromArgb(35, 168, 109);
            foreverListBox1.Size = new Size(321, 282);
            foreverListBox1.TabIndex = 0;
            foreverListBox1.Text = "foreverListBox1";
            foreverListBox1.Click += foreverListBox1_Click;
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
            tESTToolStripMenuItem.Click += tESTToolStripMenuItem_Click;
            // 
            // tESTToolStripMenuItem1
            // 
            tESTToolStripMenuItem1.Name = "tESTToolStripMenuItem1";
            tESTToolStripMenuItem1.Size = new Size(73, 22);
            tESTToolStripMenuItem1.Text = "TEST";
            tESTToolStripMenuItem1.Click += tESTToolStripMenuItem1_Click;
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
        private ReaLTaiizor.Controls.ForeverListBox foreverListBox1;
        private ReaLTaiizor.Controls.ForeverContextMenuStrip foreverContextMenuStrip1;
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStripMenuItem tESTToolStripMenuItem1;
    }
}