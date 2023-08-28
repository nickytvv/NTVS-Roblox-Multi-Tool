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
            InitializeListView();
            this.mainForm = form1;
            this.mainForm.AddToConsole("> Populated Usernames Successfully.", Color.Green);
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            listView1.Columns.Add("Username", 150);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Action 1", null, Action1_Click);
            contextMenu.Items.Add("Action 2", null, Action2_Click);
            listView1.ContextMenuStrip = contextMenu;

            PopulateListView();
        }

        public void PopulateListView()
        {
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string exeDirectory = Path.GetDirectoryName(exePath);
            cookiesFilePath = Path.Combine(exeDirectory, "cookies.csv");

            listView1.Items.Clear();

            if (File.Exists(cookiesFilePath))
            {
                string[] lines = File.ReadAllLines(cookiesFilePath);
                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    string username = fields[0];
                    ListViewItem item = new ListViewItem(username);
                    listView1.Items.Add(item);
                }
            }
            else
            {
                // Handle the case where the file doesn't exist, if needed
            }
        }


        private void Action1_Click(object sender, EventArgs e)
        {
            this.mainForm.AddToConsole("> Action 1 Executed.", Color.White);
        }

        private void Action2_Click(object sender, EventArgs e)
        {
            this.mainForm.AddToConsole("> Action 2 Executed.", Color.White);
        }

        private void spaceButton2_Click(object sender, EventArgs e)
        {
            PopulateListView();
            this.mainForm.AddToConsole("> Refreshed.", Color.Green);
        }

        private void foreverClose1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }

}
