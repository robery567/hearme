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
        string email = "";
        Thread friendListUpdate;

        public LoggedinForm(string Email)
        {
            InitializeComponent();
            email = Email;
            var values = new Dictionary<string, string>();
            values["status"] = "200";
            values["type"] = "user";
            values["email"] = email;

            user = JsonConvert.DeserializeObject<User>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
            values.Clear();
            nameLabel.Text = user.first_name + " " + user.last_name;
            /*
            var values = new Dictionary<string, string>();
            values["status"] = "200";
            values["type"] = "online";
            values["email"] = email;

            string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
            */
            popFriendList(user.friends);
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

        private void popFriendList(List<string> friendsList)
        {
            int i = 0;
            LinkedList friends = new LinkedList();
            foreach (string friend in friendsList)
            {
                User friend_user = new User();

                var values = new Dictionary<string, string>();
                values["status"] = "200";
                values["type"] = "user";
                values["email"] = friend;

                friend_user = JsonConvert.DeserializeObject<User>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
                values.Clear();

                Friend actual = new Friend();
                EventHandler handler = (s, e) => sound.PlaySound(new Uri(friend_user.avatar), actual.avatar);
                actual.avatar.Click += handler;
                actual.name.Text = friend_user.first_name + " " + friend_user.last_name;
                actual.email.Text = friend_user.email;
                actual.email.Location = new Point(50, 33 + i);
                actual.name.Location = new Point(48, 8 + i);
                actual.avatar.Location = new Point(5, 5 + i);
                friends.Append(actual);

                i += 45;
            }

            void popFriend()
            {
                friendPanel.Controls.Clear();
                Node curr = friends.head;
                while (curr.Next != null)
                {
                    curr = curr.Next;
                    curr.Value.avatar.Parent = curr.Value.email.Parent = curr.Value.name.Parent = friendPanel;
                }
            }

            if (friendPanel.InvokeRequired)
            {
                friendPanel.Invoke((MethodInvoker)delegate
                {
                    popFriend();
                });
            }
            else
            {
                popFriend();
            }
        }

        private void populateFriendList()
        {
            while (true)
            {
                if (user.friends[0] != "0")
                {
                    if (friendSearchCriteria.Text != "")
                    {
                        var values = new Dictionary<string, string>();
                        values["status"] = "200";
                        values["type"] = "searchfriend";
                        values["origin_email"] = email;
                        values["friend_email"] = friendSearchCriteria.Text;

                        List<string> friendsAfterCriteria = JsonConvert.DeserializeObject<List<string>>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message); ;
                        values.Clear();

                        popFriendList(friendsAfterCriteria);
                    }
                }

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
                values["origin_email"] = user.email;
                values["friend_email"] = friendEmail;

                string response = JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message;
                values.Clear();

                if (response == "OK")
                {
                    MessageBox.Show("The account " + friendEmail + " was added successfully as your friend!", "Friend Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    values = new Dictionary<string, string>();
                    values["status"] = "200";
                    values["type"] = "user";
                    values["email"] = email;

                    user = JsonConvert.DeserializeObject<User>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
                    values.Clear();

                    popFriendList(user.friends);
                }
                else if (response == "ADD_ERROR") MessageBox.Show("The account " + friendEmail + " does not exist!", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else if (response == "ALREADY_FRIEND") MessageBox.Show("The account " + friendEmail + " is already in your friend list!", "Already Friends", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void avatar_Click(object sender, EventArgs e)
        {
            sound.PlaySound(new Uri(user.avatar), avatar);
        }

        private void changeAvatar_Click(object sender, EventArgs e)
        {
            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile("http://your_server/upload.php", "POST", "C:\test.jpg");
        
            string response = Encoding.UTF8.GetString(result, 0, result.Length);
        }
    }
}
