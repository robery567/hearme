using HearMe.Properties;
using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HearMe
{
    public partial class LoggedinForm : Form
    {
        HearMeApi hearMe = new HearMeApi();
        Sound sound = new Sound();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        
        User user = new User();
        Thread friendListUpdate;

        public LoggedinForm(string email)
        {
            InitializeComponent();
            /*
            var values = new Dictionary<string, string>();
            values["status"] = "200";
            values["type"] = "user";
            values["email"] = email;

            User response = JsonConvert.DeserializeObject<User>(hearMe.CallApi(values));
            values.Clear();

            var values = new Dictionary<string, string>();
            values["status"] = "200";
            values["type"] = "online";
            values["email"] = email;

            string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
            */
            friendListUpdate = new Thread(() => populateFriendList());
            friendListUpdate.Start();
        }

        private void header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void exit_MouseEnter(object sender, EventArgs e) => exit.Image = (Image)Resources.ResourceManager.GetObject("exit_hover");
        private void exit_MouseLeave(object sender, EventArgs e) => exit.Image = (Image)Resources.ResourceManager.GetObject("exit");
        private void exit_Click(object sender, EventArgs e)
        {
            /*var values = new Dictionary<string, string> ();
            values["status"] = "200";
            values["type"] = "offline";
            values["email"] = user.email;

            string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));*/
            sound.StopSound();
            friendListUpdate.Abort();
            Close();
        }
        private void minimize_MouseEnter(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize_hover");
        private void minimize_MouseLeave(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize");
        private void minimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void populateFriendList()
        {
            while (true)
            {
                //friendList.Controls.Clear();
                /*foreach(string friend in user.friendsList)
                {
                    User thisOne = new User();

                    var values = new Dictionary<string, string>();
                    values["status"] = "200";
                    values["type"] = "user";
                    values["email"] = friend;

                    User response = JsonConvert.DeserializeObject<User>(hearMe.CallApi(values));
                    values.Clear();
                }*/
                Thread.Sleep(1000);
            }
        }

        private void addFriend_Click(object sender, EventArgs e)
        {
            string friendEmail = Prompt.ShowDialog("Your friend's email:", "Add Friend");
            if (friendEmail != "")
            {
                var values = new Dictionary<string, string>();
                values["status"] = "200";
                values["type"] = "addfriend";
                values["email"] = user.email;
                values["friendEmail"] = friendEmail;

                string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
                values.Clear();

                if (response == "friendadded") MessageBox.Show("The account " + friendEmail + " was added successfully as your friend!", "Friend Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (response == "notfound") MessageBox.Show("The account " + friendEmail + " does not exist!", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void avatar_Click(object sender, EventArgs e)
        {
            sound.PlaySound(new Uri("https://ia802508.us.archive.org/5/items/testmp3testfile/mpthreetest.mp3"), avatar);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sound.PlaySound(new Uri("http://www.music.helsinki.fi/tmt/opetus/uusmedia/esim/a2002011001-e02.wav"), pictureBox3);
        }
        
    }
}
