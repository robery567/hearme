namespace HearMe
{
    partial class LoginForm
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
            this.emailLogin = new System.Windows.Forms.TextBox();
            this.passwordLogin = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.forgotPassword = new System.Windows.Forms.LinkLabel();
            this.logIn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.firstNameRegister = new System.Windows.Forms.TextBox();
            this.lastNameRegister = new System.Windows.Forms.TextBox();
            this.emailRegister = new System.Windows.Forms.TextBox();
            this.passwordRegister = new System.Windows.Forms.TextBox();
            this.maleRegister = new System.Windows.Forms.RadioButton();
            this.femaleRegister = new System.Windows.Forms.RadioButton();
            this.signUp = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.exit = new System.Windows.Forms.PictureBox();
            this.horizontalRuleMainForm = new System.Windows.Forms.PictureBox();
            this.header = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalRuleMainForm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.header)).BeginInit();
            this.SuspendLayout();
            // 
            // emailLogin
            // 
            this.emailLogin.Location = new System.Drawing.Point(125, 259);
            this.emailLogin.Name = "emailLogin";
            this.emailLogin.Size = new System.Drawing.Size(186, 20);
            this.emailLogin.TabIndex = 4;
            // 
            // passwordLogin
            // 
            this.passwordLogin.Location = new System.Drawing.Point(125, 285);
            this.passwordLogin.Name = "passwordLogin";
            this.passwordLogin.PasswordChar = '*';
            this.passwordLogin.Size = new System.Drawing.Size(186, 20);
            this.passwordLogin.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Lime;
            this.label1.Location = new System.Drawing.Point(74, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Email:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Lime;
            this.label2.Location = new System.Drawing.Point(48, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            // 
            // forgotPassword
            // 
            this.forgotPassword.AutoSize = true;
            this.forgotPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forgotPassword.Location = new System.Drawing.Point(147, 337);
            this.forgotPassword.Name = "forgotPassword";
            this.forgotPassword.Size = new System.Drawing.Size(145, 16);
            this.forgotPassword.TabIndex = 8;
            this.forgotPassword.TabStop = true;
            this.forgotPassword.Text = "Forgot your password?";
            this.forgotPassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotPassword_LinkClicked);
            // 
            // logIn
            // 
            this.logIn.Location = new System.Drawing.Point(125, 311);
            this.logIn.Name = "logIn";
            this.logIn.Size = new System.Drawing.Size(186, 23);
            this.logIn.TabIndex = 9;
            this.logIn.Text = "Log In";
            this.logIn.UseVisualStyleBackColor = true;
            this.logIn.Click += new System.EventHandler(this.logIn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(645, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(199, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Create a New Account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(667, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Hear your friends loud and clear.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Lime;
            this.label5.Location = new System.Drawing.Point(576, 234);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "First Name:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Lime;
            this.label6.Location = new System.Drawing.Point(575, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Last Name:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Lime;
            this.label7.Location = new System.Drawing.Point(606, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 16);
            this.label7.TabIndex = 15;
            this.label7.Text = "Email:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Lime;
            this.label8.Location = new System.Drawing.Point(581, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 16);
            this.label8.TabIndex = 16;
            this.label8.Text = "Password:";
            // 
            // firstNameRegister
            // 
            this.firstNameRegister.Location = new System.Drawing.Point(658, 233);
            this.firstNameRegister.Name = "firstNameRegister";
            this.firstNameRegister.Size = new System.Drawing.Size(186, 20);
            this.firstNameRegister.TabIndex = 17;
            // 
            // lastNameRegister
            // 
            this.lastNameRegister.Location = new System.Drawing.Point(658, 259);
            this.lastNameRegister.Name = "lastNameRegister";
            this.lastNameRegister.Size = new System.Drawing.Size(186, 20);
            this.lastNameRegister.TabIndex = 18;
            // 
            // emailRegister
            // 
            this.emailRegister.Location = new System.Drawing.Point(658, 285);
            this.emailRegister.Name = "emailRegister";
            this.emailRegister.Size = new System.Drawing.Size(186, 20);
            this.emailRegister.TabIndex = 19;
            // 
            // passwordRegister
            // 
            this.passwordRegister.Location = new System.Drawing.Point(658, 311);
            this.passwordRegister.Name = "passwordRegister";
            this.passwordRegister.PasswordChar = '*';
            this.passwordRegister.Size = new System.Drawing.Size(186, 20);
            this.passwordRegister.TabIndex = 20;
            // 
            // maleRegister
            // 
            this.maleRegister.AutoSize = true;
            this.maleRegister.Location = new System.Drawing.Point(796, 337);
            this.maleRegister.Name = "maleRegister";
            this.maleRegister.Size = new System.Drawing.Size(48, 17);
            this.maleRegister.TabIndex = 21;
            this.maleRegister.TabStop = true;
            this.maleRegister.Text = "Male";
            this.maleRegister.UseVisualStyleBackColor = true;
            // 
            // femaleRegister
            // 
            this.femaleRegister.AutoSize = true;
            this.femaleRegister.Location = new System.Drawing.Point(658, 337);
            this.femaleRegister.Name = "femaleRegister";
            this.femaleRegister.Size = new System.Drawing.Size(59, 17);
            this.femaleRegister.TabIndex = 22;
            this.femaleRegister.TabStop = true;
            this.femaleRegister.Text = "Female";
            this.femaleRegister.UseVisualStyleBackColor = true;
            // 
            // signUp
            // 
            this.signUp.Location = new System.Drawing.Point(658, 360);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(186, 23);
            this.signUp.TabIndex = 23;
            this.signUp.Text = "Sign Up";
            this.signUp.UseVisualStyleBackColor = true;
            this.signUp.Click += new System.EventHandler(this.signUp_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(186, 133);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Welcome back!";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(161, 109);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 24);
            this.label10.TabIndex = 24;
            this.label10.Text = "Hear Them All";
            // 
            // minimize
            // 
            this.minimize.Image = global::HearMe.Properties.Resources.minimize;
            this.minimize.Location = new System.Drawing.Point(902, 12);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(20, 20);
            this.minimize.TabIndex = 27;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            this.minimize.MouseEnter += new System.EventHandler(this.minimize_MouseEnter);
            this.minimize.MouseLeave += new System.EventHandler(this.minimize_MouseLeave);
            // 
            // exit
            // 
            this.exit.Image = global::HearMe.Properties.Resources.exit;
            this.exit.Location = new System.Drawing.Point(928, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(20, 20);
            this.exit.TabIndex = 26;
            this.exit.TabStop = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            this.exit.MouseEnter += new System.EventHandler(this.exit_MouseEnter);
            this.exit.MouseLeave += new System.EventHandler(this.exit_MouseLeave);
            // 
            // horizontalRuleMainForm
            // 
            this.horizontalRuleMainForm.Image = global::HearMe.Properties.Resources.verticalLine;
            this.horizontalRuleMainForm.Location = new System.Drawing.Point(471, 75);
            this.horizontalRuleMainForm.Name = "horizontalRuleMainForm";
            this.horizontalRuleMainForm.Size = new System.Drawing.Size(20, 499);
            this.horizontalRuleMainForm.TabIndex = 10;
            this.horizontalRuleMainForm.TabStop = false;
            // 
            // header
            // 
            this.header.Image = global::HearMe.Properties.Resources.header;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(960, 75);
            this.header.TabIndex = 0;
            this.header.TabStop = false;
            this.header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(960, 575);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.signUp);
            this.Controls.Add(this.femaleRegister);
            this.Controls.Add(this.maleRegister);
            this.Controls.Add(this.passwordRegister);
            this.Controls.Add(this.emailRegister);
            this.Controls.Add(this.lastNameRegister);
            this.Controls.Add(this.firstNameRegister);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.horizontalRuleMainForm);
            this.Controls.Add(this.logIn);
            this.Controls.Add(this.forgotPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordLogin);
            this.Controls.Add(this.emailLogin);
            this.Controls.Add(this.header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoginForm";
            this.Text = "HearMe";
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalRuleMainForm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.header)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox header;
        private System.Windows.Forms.TextBox emailLogin;
        private System.Windows.Forms.TextBox passwordLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel forgotPassword;
        private System.Windows.Forms.Button logIn;
        private System.Windows.Forms.PictureBox horizontalRuleMainForm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox firstNameRegister;
        private System.Windows.Forms.TextBox lastNameRegister;
        private System.Windows.Forms.TextBox emailRegister;
        private System.Windows.Forms.TextBox passwordRegister;
        private System.Windows.Forms.RadioButton maleRegister;
        private System.Windows.Forms.RadioButton femaleRegister;
        private System.Windows.Forms.Button signUp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox exit;
        private System.Windows.Forms.PictureBox minimize;
    }
}

