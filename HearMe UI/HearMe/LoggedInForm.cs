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
        Thread messagesUpdate;

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
            
            popFriendList(user.friends);
            
            friendListUpdate = new Thread(() => populateFriendList());
            friendListUpdate.Start();

            messagesUpdate = new Thread(() => populateMessagesList());
            messagesUpdate.Start();
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
            sound.StopSound();
            friendListUpdate.Abort();
            messagesUpdate.Abort();
            Close();
        }
        private void minimize_MouseEnter(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize_hover");
        private void minimize_MouseLeave(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize");
        private void minimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void sendMessage(string friend_email, string fileName)
        {
            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile("http://sandbox.robertcolca.me/request?type=add_message&destination_email=" + friend_email + "&origin_email=" + user.email, "POST", fileName);

            File.Delete(fileName);
            //string result2 = Encoding.UTF8.GetString(result, 0, result.Length);
            //MessageBox.Show(result2);
        }

        private void populateMessagesList()
        {
            while (true)
            {
                if (chatPanel.TabPages.Count != 0)
                {
                    chatPanel.Invoke((MethodInvoker)delegate
                    {
                        var values = new Dictionary<string, string>();
                        values["status"] = "200";
                        values["type"] = "get_messages";
                        values["origin_email"] = email;
                        values["friend_email"] = chatPanel.SelectedTab.Text;

                        List<string> messages = JsonConvert.DeserializeObject<List<string>>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
                        values.Clear();
                        
                        if (messages[0] != "0")
                        foreach (string msg in messages)
                        {
                            ((ListBox)chatPanel.SelectedTab.Controls[4]).Items.Add(msg);
                        }
                    });
                }
                Thread.Sleep(1000);
            }
        }

        private void makeNewChatPanelTabPage(string friend_email)
        {
            chatPanel.TabPages.Add(friend_email);
            Button send = new Button();
            Button recorder = new Button();
            Label recording = new Label();
            PictureBox recordingPicture = new PictureBox();
            ListBox listBox = new ListBox();

            send.Text = "Send";
            send.Size = new Size(100, 50);
            EventHandler sendClick = (s, e) =>
            {
                string fileName = sound.WaveFile.Filename;
                sound.WaveSource.StopRecording();
                sound.WaveFile.Dispose();

                sendMessage(friend_email, fileName);

                send.Enabled = false;
                recorder.Enabled = true;
                recordingPicture.Visible = recording.Visible = false;
            };
            send.Click += sendClick;
            send.Location = new Point(chatPanel.Width - 110, chatPanel.Height - 85);
            send.Enabled = false;

            recorder.Text = "Record";
            recorder.Size = new Size(100, 50);
            recorder.Location = new Point(send.Location.X - 110, send.Location.Y);
            EventHandler recorderClick = (s, e) => 
            {
                sound.StartRecording();
                send.Enabled = true;
                recorder.Enabled = false;

                recordingPicture.Visible = recording.Visible = true;
            };
            recorder.Click += recorderClick;

            recording.Text = "Recording";
            recording.Location = new Point(recorder.Location.X - 55, recorder.Location.Y + 18);

            recordingPicture.Image = (Image)Resources.ResourceManager.GetObject("recording");
            recordingPicture.Size = new Size(50, 50);
            recordingPicture.Location = new Point(recording.Location.X - 55, recorder.Location.Y);
            recordingPicture.Visible = recording.Visible = false;

            listBox.Location = new Point(20, 10);
            listBox.Size = new Size(chatPanel.Width - 50, chatPanel.Height - 100);
            EventHandler listBoxDoubleClick = (s, e) =>
            {
                sound.PlaySound(new Uri(listBox.SelectedItem.ToString()), avatar);
                listBox.Items.Remove(listBox.SelectedItem);
            };

            listBox.DoubleClick += listBoxDoubleClick;

            listBox.Parent = recordingPicture.Parent = recording.Parent = recorder.Parent = send.Parent = chatPanel.SelectedTab;

        }

        private void popFriendList(List<string> friendsList)
        {
            int i = 0;
            LinkedList friends = new LinkedList();
            foreach (string friend in friendsList)
            {
                if (friend != "0")
                {
                    User friend_user = new User();

                    var values = new Dictionary<string, string>();
                    values["status"] = "200";
                    values["type"] = "user";
                    values["email"] = friend;

                    friend_user = JsonConvert.DeserializeObject<User>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
                    values.Clear();

                    Friend actual = new Friend();
                    EventHandler avatarHandler = (s, e) => sound.PlaySound(new Uri(friend_user.avatar), actual.avatar);
                    actual.avatar.Click += avatarHandler;
                    actual.name.Text = friend_user.first_name + " " + friend_user.last_name;
                    actual.email.Text = friend_user.email;
                    actual.email.Location = new Point(50, 33 + i);
                    EventHandler emailHandler = (s, e) => { makeNewChatPanelTabPage(friend_user.email); };
                    actual.email.Click += emailHandler;
                    actual.name.Location = new Point(48, 8 + i);
                    actual.avatar.Location = new Point(5, 5 + i);
                    actual.avatar.Cursor = actual.email.Cursor = Cursors.Hand;
                    friends.Append(actual);

                    i += 45;
                }
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
                    chatPanel.Invoke((MethodInvoker)delegate
                    {
                        popFriend();
                    });
                });
            }
            else
            {
                popFriend();
            }
        }

        bool entered_once = false;
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
                        entered_once = false;
                    }
                    else if (!entered_once) { popFriendList(user.friends); entered_once = true; }
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
            OpenFileDialog getWav = new OpenFileDialog();
            getWav.Filter = "Wave Files (.wav)|*.wav";
            getWav.ShowDialog();
            string userFile = getWav.FileName;

            System.Net.WebClient Client = new System.Net.WebClient();
            Client.Headers.Add("Content-Type", "binary/octet-stream");

            byte[] result = Client.UploadFile("http://sandbox.robertcolca.me/request?type=upload_avatar&user=" + user.email, "POST", userFile);

            string result2 = Encoding.UTF8.GetString(result, 0, result.Length);
            string response = JsonConvert.DeserializeObject<Message>(JsonConvert.DeserializeObject<JsonStatusCut>(result2).response).message;
            
            if (response == "OK")
            {
                var values = new Dictionary<string, string>();
                values["status"] = "200";
                values["type"] = "user";
                values["email"] = email;

                user = JsonConvert.DeserializeObject<User>(JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values)).message);
                values.Clear();

                MessageBox.Show("Your avatar was successfully updated!", "Avatar Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sound.StartRecording();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sound.WaveSource.StopRecording();
        }
    }
}
