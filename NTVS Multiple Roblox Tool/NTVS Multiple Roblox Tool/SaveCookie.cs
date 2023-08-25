using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ReaLTaiizor.Controls;

namespace NTVS_Multiple_Roblox_Tool
{
    public partial class SaveCookie : Form
    {
        private Form1 mainForm;
        private string cookiesFilePath;


        public SaveCookie(Form1 form1)
        {
            InitializeComponent();
            this.mainForm = form1;
        }

        private void SaveCookie_Load(object sender, EventArgs e)
        {

        }

        private void SaveCookieInBackground()
        {
            this.mainForm.AddToConsole("Checking Cookie...", Color.White);
            Task.Run(() =>
            {
                string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                string exeDirectory = Path.GetDirectoryName(exePath);

                cookiesFilePath = Path.Combine(exeDirectory, "cookies.csv");

                if (string.IsNullOrEmpty(cookiesFilePath))
                {
                    mainForm.Invoke((Action)(() => mainForm.AddToConsole("Cookies file path is not initialized.", Color.Red)));
                    return;
                }

                if (!File.Exists(cookiesFilePath))
                {
                    mainForm.Invoke((Action)(() => mainForm.AddToConsole("Cookies file does not exist at the specified path.", Color.Red)));
                    return;
                }

                string cookie = dreamTextBox1.Text;

                string username = mainForm.LogInWithCookie(cookie);

                if (string.IsNullOrEmpty(username))
                {
                    mainForm.Invoke((Action)(() => mainForm.AddToConsole("Invalid cookie.", Color.Red)));
                    return;
                }

                string[] existingCookies = File.ReadAllLines(cookiesFilePath);

                foreach (string line in existingCookies)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length > 1 && fields[1] == cookie)
                    {
                        mainForm.Invoke((Action)(() => mainForm.AddToConsole("This cookie already exists.", Color.Black)));
                        return;
                    }
                }

                using (StreamWriter sw = File.AppendText(cookiesFilePath))
                {
                    sw.WriteLine($"{username},{cookie}");
                }

                mainForm.Invoke((Action)(() => mainForm.AddToConsole("Cookie added successfully.", Color.Green)));
                var item = new ReaLTaiizor.Child.Crown.CrownDropDownItem(); 
                item.Text = username; 
                mainForm.Invoke((Action)(() => mainForm.AddUsernameToDropDown(username)));
            });
        }


        private void dreamButton1_Click(object sender, EventArgs e)
        {
            SaveCookieInBackground();
        }

        private void dreamButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dreamForm1_Enter(object sender, EventArgs e)
        {

        }
    }
}
