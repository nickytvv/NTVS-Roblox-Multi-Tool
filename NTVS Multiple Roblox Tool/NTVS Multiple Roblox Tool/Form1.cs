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
using System.Threading;
using SeleniumExtras.WaitHelpers;
using System.Net.Sockets;
using HtmlAgilityPack;

namespace NTVS_Multiple_Roblox_Tool
{
    public partial class Form1 : Form
    {
        private string cookiesFilePath;
        private Mutex robloxMutex;


        public Form1()
        {
            InitializeComponent();

            robloxMutex = new Mutex(true, "ROBLOX_singletonMutex");
            AddToConsole("You can now run multiple instances of Roblox...", Color.White);
            string exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            string exeDirectory = Path.GetDirectoryName(exePath);

            cookiesFilePath = Path.Combine(exeDirectory, "cookies.csv");

            if (!File.Exists(cookiesFilePath))
            {
                File.Create(cookiesFilePath).Close();
                AddToConsole("> Created cookies file.", Color.White);
            }
            else
            {
                AddToConsole("> Cookies file already exists.", Color.Green);
            }

            try
            {
                string[] lines = File.ReadAllLines(cookiesFilePath);

                foreach (string line in lines)
                {
                    string[] fields = line.Split(',');
                    string username = fields[0];
                    var item = new ReaLTaiizor.Child.Crown.CrownDropDownItem();
                    item.Text = username;
                    crownDropDownList1.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                AddToConsole("> An error occurred while reading the cookies file: " + e.Message, Color.Red);
            }

            crownDropDownList1.SelectedItemChanged += crownDropDownList1_SelectedIndexChanged;
        }


        public void AddToConsole(string message, Color color)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => AddToConsole(message, color)));
                return;
            }

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;

            richTextBox1.SelectionColor = color;
            richTextBox1.AppendText(message + "\n");
            richTextBox1.SelectionColor = richTextBox1.ForeColor;
        }

        private string GetCookieForUsername(string username)
        {
            string[] lines = File.ReadAllLines(cookiesFilePath);
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields.Length < 2)
                {
                    continue;
                }
                string storedUsername = fields[0];
                string cookie = fields[1];
                if (username == storedUsername)
                {
                    return cookie;
                }
            }
            return null;
        }

        private async Task<string> AuthenticateWithCookieAsync(string cookie)
        {
            return await Task.Run(() =>
            {
                var driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                var options = new ChromeOptions();
                options.AddArgument("headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
                var driver = new ChromeDriver(driverService, options);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

                string warningMessage = "_|WARNING:-DO-NOT-SHARE-THIS.--Sharing-this-will-allow-someone-to-log-in-as-you-and-to-steal-your-ROBUX-and-items.|_";
                if (cookie.StartsWith(warningMessage))
                {
                    cookie = cookie.Substring(warningMessage.Length);
                }

                try
                {
                    driver.Navigate().GoToUrl("https://www.roblox.com/");
                    driver.Manage().Cookies.DeleteAllCookies();
                    OpenQA.Selenium.Cookie newCookie = new OpenQA.Selenium.Cookie(".ROBLOSECURITY", cookie, "www.roblox.com", "/", DateTime.Now.AddHours(1));
                    driver.Manage().Cookies.AddCookie(newCookie);
                    driver.Navigate().Refresh();

                    using (HttpClient client = new HttpClient())
                    {
                        var csrfRequest = new HttpRequestMessage(HttpMethod.Post, "https://auth.roblox.com/v1/authentication-ticket");
                        csrfRequest.Headers.Add("Cookie", ".ROBLOSECURITY=" + cookie);
                        var csrfResponse = client.SendAsync(csrfRequest).Result;
                        var csrfToken = csrfResponse.Headers.GetValues("x-csrf-token").FirstOrDefault();

                        var authRequest = new HttpRequestMessage(HttpMethod.Post, "https://auth.roblox.com/v1/authentication-ticket");
                        authRequest.Headers.Add("Cookie", ".ROBLOSECURITY=" + cookie);
                        authRequest.Headers.Add("X-CSRF-TOKEN", csrfToken);
                        var authResponse = client.SendAsync(authRequest).Result;
                        var authTicket = authResponse.Headers.GetValues("rbx-authentication-ticket").FirstOrDefault();

                        return authTicket;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                    AddToConsole(ex.ToString(), Color.Red);
                    return null;
                }
                finally
                {
                    driver.Quit();
                }
            });
        }



        private void ConnectToGame(string gameIp, int gamePort)
        {
            try
            {
                TcpClient client = new TcpClient(gameIp, gamePort);

                NetworkStream stream = client.GetStream();

                byte[] message = Encoding.ASCII.GetBytes("Hello, Game Server!");
                stream.Write(message, 0, message.Length);

                byte[] responseBuffer = new byte[256];
                int responseLength = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string serverResponse = Encoding.ASCII.GetString(responseBuffer, 0, responseLength);

                AddToConsole($"> Connected to the game at {gameIp}:{gamePort}. Server response: {serverResponse}", Color.Green);

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                AddToConsole($"> Failed to connect to the game at {gameIp}:{gamePort}. Error: {ex.Message}", Color.Red);
            }
        }


        private string GenerateBrowserTrackerId()
        {
            return Guid.NewGuid().ToString();
        }

        private async void joinGameButton_Click(object sender, EventArgs e)
        {
            string gameIdOrUrl = joinGameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(gameIdOrUrl))
            {
                AddToConsole("> Game ID or URL is required.", Color.Red);
                return;
            }

            if (!VerifyGameURL(gameIdOrUrl))
            {
                AddToConsole("> Invalid game URL.", Color.Red);
                return;
            }

            string gameId = ExtractGameIdFromUrl(gameIdOrUrl);
            if (string.IsNullOrEmpty(gameId))
            {
                AddToConsole("> Failed to extract game ID from URL.", Color.Red);
                return;
            }

            var selectedItem = crownDropDownList1.SelectedItem as ReaLTaiizor.Child.Crown.CrownDropDownItem;
            if (selectedItem == null)
            {
                AddToConsole("> No account selected.", Color.Red);
                return;
            }

            string selectedUsername = selectedItem.Text;
            string selectedCookie = GetCookieForUsername(selectedUsername);

            if (string.IsNullOrEmpty(selectedCookie))
            {
                AddToConsole($"> Cookie for {selectedUsername} not found.", Color.Red);
                return;
            }

            AddToConsole($"> Authenticating {selectedUsername}...", Color.White);
            string authCode = await AuthenticateWithCookieAsync(selectedCookie);
            AddToConsole(authCode, Color.Pink);
            if (string.IsNullOrEmpty(authCode))
            {
                AddToConsole("> Authentication failed.", Color.Red);
                return;
            }

            int placeId;
            if (!int.TryParse(authCode, out placeId))
            {
                AddToConsole($"> Invalid authCode format: {authCode}. Please enter a valid game ID or URL.", Color.Red);
                return;
            }

            var joinGameResponse = await JoinGameAsync(placeId, gameId, selectedCookie);
            if (joinGameResponse == null || !joinGameResponse.Success)
            {
                AddToConsole("> Failed to join the game.", Color.Red);
                return;
            }

            ConnectToGame(joinGameResponse.GameIp, joinGameResponse.GamePort);

            AddToConsole("> Roblox launched successfully.", Color.Green);
        }



        private string ExtractGameIdFromUrl(string gameIdOrUrl)
        {
            if (gameIdOrUrl.Contains("roblox.com/games/"))
            {
                var uri = new Uri(gameIdOrUrl);
                return uri.Segments.Last().TrimEnd('/');
            }

            return gameIdOrUrl;
        }


        private bool ValidateGame(string gameId)
        {
            return true;
        }

        private async Task<GameJoinResponse> JoinGameAsync(int placeId, string gameId, string customCookie)
        {
            string url = "https://gamejoin.roblox.com/v1/join-game-instance";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Referer", $"https://www.roblox.com/games/{placeId}/");
                client.DefaultRequestHeaders.Add("Origin", "https://roblox.com");
                client.DefaultRequestHeaders.Add("Cookie", customCookie);
                client.DefaultRequestHeaders.Add("User-Agent", "Roblox/WinInet");

                var csrfResponse = await client.PostAsync("https://auth.roblox.com/v1/authentication-ticket", null);
                var csrfToken = csrfResponse.Headers.GetValues("x-csrf-token").FirstOrDefault();

                var authHeaders = new Dictionary<string, string>
        {
            { "Cookie", ".ROBLOSECURITY=" + customCookie },
            { "Origin", "https://www.roblox.com" },
            { "Referer", "https://www.roblox.com/" },
            { "X-CSRF-TOKEN", csrfToken }
        };
                var authResponse = await client.PostAsync("https://auth.roblox.com/v1/authentication-ticket", new FormUrlEncodedContent(authHeaders));
                var ticket = authResponse.Headers.GetValues("rbx-authentication-ticket").FirstOrDefault();

                var requestBody = new
                {
                    placeId = placeId,
                    isTeleport = false,
                    gameId = gameId,
                    gameJoinAttemptId = gameId,
                    authenticationTicket = ticket
                };

                string jsonBody = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var gameJoinResponse = JsonConvert.DeserializeObject<GameJoinResponse>(responseContent);

                    ConnectToGame(gameJoinResponse.GameIp, gameJoinResponse.GamePort);

                    return gameJoinResponse;
                }
                else
                {
                    AddToConsole($"> Failed to join the game. Error: {responseContent}", Color.Red);
                    return null;
                }
            }
        }



        public class GameJoinResponse
        {
            public bool Success { get; set; }
            public string ServerEndpoint { get; set; }

            public string GameIp => ServerEndpoint.Split(':')[0];
            public int GamePort => int.Parse(ServerEndpoint.Split(':')[1]);

        }

        private bool VerifyGameURL(string url)
        {
            try
            {
                if (!url.Contains("roblox.com/games/"))
                {
                    AddToConsole("> URL does not contain 'roblox.com/games/'.", Color.Red);
                    return false;
                }

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                        document.Load(response.GetResponseStream());

                        HtmlNode gameNameNode = document.DocumentNode.SelectSingleNode("//h1[@class='game-name']");

                        if (gameNameNode != null)
                        {
                            string gameName = gameNameNode.InnerText.Trim();
                            AddToConsole($"> Game name: {gameName}", Color.Green);
                            return true;
                        }
                        else
                        {
                            AddToConsole("> Failed to extract game name from URL.", Color.Red);
                        }
                    }
                    else
                    {
                        AddToConsole($"> URL returned status code {response.StatusCode}.", Color.Red);
                    }
                }
            }
            catch (WebException ex)
            {
                AddToConsole($"> An error occurred while verifying the URL: {ex.Message}", Color.Red);
            }

            return false;
        }

        private void joinGameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        public string LogInWithCookie(string cookie)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddArgument("headless");
            var driver = new ChromeDriver(driverService, options);

            string warningMessage = "_|WARNING:-DO-NOT-SHARE-THIS.--Sharing-this-will-allow-someone-to-log-in-as-you-and-to-steal-your-ROBUX-and-items.|_";
            if (cookie.StartsWith(warningMessage))
            {
                cookie = cookie.Substring(warningMessage.Length);
            }


            try
            {
                driver.Navigate().GoToUrl("https://www.roblox.com/");

                driver.Manage().Cookies.DeleteAllCookies();

                OpenQA.Selenium.Cookie newCookie = new OpenQA.Selenium.Cookie(".ROBLOSECURITY", cookie, "www.roblox.com", "/", DateTime.Now.AddHours(1));

                driver.Manage().Cookies.AddCookie(newCookie);

                driver.Navigate().Refresh();

                System.Threading.Thread.Sleep(2000);

                driver.Navigate().GoToUrl("https://www.roblox.com/my/profile");

                System.Threading.Thread.Sleep(2000);

                string json = driver.FindElement(By.TagName("body")).Text;

                dynamic data = JsonConvert.DeserializeObject(json);
                string username = data.Username;

                return username;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                AddToConsole(ex.ToString(), Color.Red);
                return null;
            }
            finally
            {
                driver.Quit();
            }
        }

        public async void crownDropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = crownDropDownList1.SelectedItem as ReaLTaiizor.Child.Crown.CrownDropDownItem;
            if (selectedItem == null)
            {
                AddToConsole("> No item selected.", Color.Red);
                return;
            }

            string selectedUsername = selectedItem.Text;

            AddToConsole($"> Testing cookie for {selectedUsername}...", Color.White);

            string[] lines = File.ReadAllLines(cookiesFilePath);

            string selectedCookie = null;
            foreach (string line in lines)
            {
                string[] fields = line.Split(',');
                if (fields.Length < 2)
                {
                    continue;
                }
                string username = fields[0];
                string cookie = fields[1];
                if (username == selectedUsername)
                {
                    selectedCookie = cookie;
                    break;
                }
            }

            await Task.Run(() =>
            {
                if (selectedCookie != null)
                {
                    string username = LogInWithCookie(selectedCookie);
                    if (!string.IsNullOrEmpty(username))
                    {
                        this.Invoke((Action)(() => AddToConsole($"> Verified {username}'s Cookie!", Color.Green)));
                    }
                    else
                    {
                        this.Invoke((Action)(() => AddToConsole($"> Cookie for {username} needs to be refreshed, deleting it from the system file..", Color.Red)));

                        string[] lines = File.ReadAllLines(cookiesFilePath);

                        var validLines = lines.Where(line => !line.Contains(selectedCookie)).ToArray();

                        File.WriteAllLines(cookiesFilePath, validLines);

                        this.Invoke((Action)(() => AddToConsole($"> Invalid cookie {username} removed from file.", Color.Green)));
                    }
                }
            });
        }



        private void spaceButton1_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("chromedriver"))
            {
                try
                {
                    process.Kill();
                    AddToConsole("> ChromeDriver process ended successfully.", Color.Green);
                }
                catch (Exception ex)
                {
                    AddToConsole($"> Error ending ChromeDriver process: {ex.Message}", Color.Red);
                }
            }


            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                try
                {
                    process.Kill();
                    AddToConsole("> Roblox process ended successfully.", Color.Green);
                }
                catch (Exception ex)
                {
                    AddToConsole($"> Error ending Roblox process: {ex.Message}", Color.Red);
                }
            }
        }

        private void formTheme1_Click(object sender, EventArgs e)
        {

        }

        public void cyberRichTextBox1_Load(object sender, EventArgs e)
        {

        }

        public void crownDropDownList1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void spaceButton2_Click(object sender, EventArgs e)
        {
            SaveCookie saveCookieForm = new SaveCookie(this);

            saveCookieForm.Show();
            AddToConsole("> Running the Cookie Form...", Color.White);


        }

        public void AddUsernameToDropDown(string username)
        {
            var item = new ReaLTaiizor.Child.Crown.CrownDropDownItem();
            item.Text = username;
            crownDropDownList1.Items.Add(item);
        }

        private void spaceButton3_Click(object sender, EventArgs e)
        {
            AccountManager AccountManagerForm = new AccountManager(this);

            AccountManagerForm.Show();
            AddToConsole("> Running the Account Manager Form...", Color.White);
        }

        private void crownCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = crownCheckBox1.Checked;
            if (TopMost == true)
            {
                AddToConsole("> TopMost = true;", Color.White);
            }
            else
            {
                AddToConsole("> TopMost = false;", Color.White);
            }
        }
    }
}