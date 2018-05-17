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
            if (System.Text.RegularExpressions.Regex.IsMatch(emailRegister.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
            {
                var values = new NameValueCollection();
                values["type"] = "login";
                values["email"] = emailLogin.Text;
                values["password"] = passwordLogin.Text;

                string responseString = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));

                switch (responseString.ToString())
                {
                    case "nouser": MessageBox.Show("The email " + emailLogin.Text + " does not exist!", "Invalid User", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); break;
                    case "wrongpassword": MessageBox.Show("The password is wrong!", "Wrong password!", MessageBoxButtons.OK, MessageBoxIcon.Stop); break;
                    case "allgood":
                        this.Hide();
                        var loggedinForm = new LoggedinForm(emailLogin.Text);
                        loggedinForm.Closed += (s, args) => this.Close();
                        loggedinForm.Show();
                        break;
                    default: MessageBox.Show("It looks like there is a problem with the server!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
                }
            }
            else MessageBox.Show("The email is invalid!", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(firstNameRegister.Text, @"^[a-zA-Z-]+$"))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(lastNameRegister.Text, @"^[a-zA-Z-]+$"))
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(emailRegister.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
                    {
                        if (maleRegister.Checked || femaleRegister.Checked)
                        {
                            var values = new NameValueCollection();
                            values["type"] = "register";
                            values["firstName"] = firstNameRegister.Text;
                            values["lastName"] = lastNameRegister.Text;
                            values["email"] = emailRegister.Text;
                            values["password"] = passwordRegister.Text;
                            if (maleRegister.Checked)
                                values["gender"] = "male";
                            else values["gender"] = "female";

                            string responseString = JsonConvert.DeserializeObject<string>(hearMe.CallApi(values));
                            if (responseString == "created")
                                MessageBox.Show("The account was created successfully!", "New Account Registered", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            else MessageBox.Show("Error connecting to the server!", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else MessageBox.Show("You didn't select the gender!", "Gender not selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("The email is invalid!", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("The Last Name should contain only letters and '-'!", "Last Name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("The First Name should contain only letters and '-'!", "First Name Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void forgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(emailRegister.Text, @"^(([a-zA-Z0-9-.]+)(@[a-zA-Z0-9-]+)\.([a-z-]+))$"))
            {
                var values = new NameValueCollection();
                values["type"] = "forgot";
                values["email"] = emailLogin.Text;

                string responseString = hearMe.CallApi(values);
            }
            else MessageBox.Show("The email is invalid!", "Email Invalid", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
