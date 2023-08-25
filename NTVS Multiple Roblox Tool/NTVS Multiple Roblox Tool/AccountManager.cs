using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace NTVS_Multiple_Roblox_Tool
{
    public partial class AccountManager : Form
    {

        private string cookiesFilePath;
        private Form1 mainForm;

        public AccountManager(Form1 form1)
        {
            InitializeComponent();
            this.mainForm = form1;

            // Initialize cookiesFilePath
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            cookiesFilePath = Path.Combine(exeDirectory, "cookies.csv");
            if (!File.Exists(cookiesFilePath))
            {
                mainForm.AddToConsole("> No Cookies File Found! Please restart the program.", Color.Red);
            }
            else
            {
                mainForm.AddToConsole($"> {cookiesFilePath}", Color.White);
                mainForm.AddToConsole("> Cookies Files Found...", Color.Green);

            }

        }

        private List<string> LoadUsernamesFromCookiesFile()
        {
            List<string> usernames = new List<string>();

            if (File.Exists(cookiesFilePath))
            {
                string[] lines = File.ReadAllLines(cookiesFilePath);

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] fields = line.Split(',');
                        if (fields.Length > 0)
                        {
                            string username = fields[0];
                            usernames.Add(username);
                        }
                    }
                }
            }
            else
            {
                mainForm.AddToConsole($"File not found: {cookiesFilePath}", Color.White);
            }

            return usernames;
        }

        private void PopulateUsernamesListBox()
        {
            List<string> usernames = LoadUsernamesFromCookiesFile();
            foreverListBox1.Clear();

            foreach (string username in usernames)
            {
                foreverListBox1.AddItem(username);
            }
        }


        private void InitializeContextMenu()
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ToolStripMenuItem option1 = new ToolStripMenuItem("Option 1");
            ToolStripMenuItem option2 = new ToolStripMenuItem("Option 2");

            contextMenu.Items.AddRange(new ToolStripItem[] { option1, option2 });
            foreverListBox1.ContextMenuStrip = contextMenu;

            option1.Click += tESTToolStripMenuItem_Click;
            option2.Click += tESTToolStripMenuItem1_Click;
        }

        private void dreamForm1_Enter(object sender, EventArgs e)
        {

        }

        private void foreverListBox1_Click(object sender, EventArgs e)
        {

        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tESTToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
    }
}
