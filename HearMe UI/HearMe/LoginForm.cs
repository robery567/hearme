using HearMe.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HearMe
{
    public partial class LoginForm : Form
    {
        HearMeApi hearMe = new HearMeApi();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public LoginForm()
        {
            InitializeComponent();
            var loggedinForm = new LoggedinForm(emailLogin.Text);
            loggedinForm.StartPosition = FormStartPosition.Manual;
            loggedinForm.Location = this.Location;
            loggedinForm.Closed += (s, args) => this.Close();
            loggedinForm.Show();
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
        private void exit_Click(object sender, EventArgs e) => Application.Exit();
        private void minimize_MouseEnter(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize_hover");
        private void minimize_MouseLeave(object sender, EventArgs e) => minimize.Image = (Image)Resources.ResourceManager.GetObject("minimize");
        private void minimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void logIn_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(emailLogin.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
            {
                var values = new Dictionary<string, string>();
                values["status"] = "200";
                values["type"] = "login";
                values["email"] = emailLogin.Text;
                values["password"] = passwordLogin.Text;

                Message response = JsonConvert.DeserializeObject<Message>(hearMe.CallApi(values));
                values.Clear();

                switch (response.message)
                {
                    case "notfound": MessageBox.Show("The email " + emailLogin.Text + " does not exist!", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
                    case "wrongpassword": MessageBox.Show("The password is wrong!", "Wrong password!", MessageBoxButtons.OK, MessageBoxIcon.Stop); break;
                    case "allgood":
                        this.Hide();
                        var loggedinForm = new LoggedinForm(emailLogin.Text);
                        loggedinForm.StartPosition = FormStartPosition.Manual;
                        loggedinForm.Location = this.Location;
                        loggedinForm.Closed += (s, args) => this.Close();
                        loggedinForm.Show();
                        break;
                }
            }
            else MessageBox.Show("The email is invalid!", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(firstNameRegister.Text, @"^[a-zA-Z-]+$"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(lastNameRegister.Text, @"^[a-zA-Z-]+$"))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(emailRegister.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
                    {
                        if (passwordRegister.Text.Length >= 6)
                        {
                            if (maleRegister.Checked || femaleRegister.Checked)
                            {
                                var values = new Dictionary<string, string>();
                                values["type"] = "register";
                                values["firstName"] = firstNameRegister.Text;
                                values["lastName"] = lastNameRegister.Text;
                                values["email"] = emailRegister.Text;
                                values["password"] = passwordRegister.Text;
                                if (maleRegister.Checked)
                                    values["gender"] = "male";
                                else values["gender"] = "female";

                                string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
                                values.Clear();

                                if (response == "created") MessageBox.Show("The account was created successfully!", "New Account Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else if (response == "existing") MessageBox.Show("The email " + emailRegister.Text + " already exists!", "Existing Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else MessageBox.Show("You didn't select the gender!", "Gender not selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("The password must be longer than 5 characters!", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("The email is invalid!", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("The Last Name should contain only letters and '-'!", "Last Name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("The First Name should contain only letters and '-'!", "First Name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void forgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(emailRegister.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
            {
                var values = new Dictionary<string, string>();
                values["status"] = "200";
                values["type"] = "forgot";
                values["email"] = emailLogin.Text;

                string response = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
                values.Clear();

                if (response == "emailsent") MessageBox.Show("The password was sent to " + emailLogin.Text + " email!", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else if (response == "notfound") MessageBox.Show("The email " + emailLogin.Text + " does not exist!", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else MessageBox.Show("The email is invalid!", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
