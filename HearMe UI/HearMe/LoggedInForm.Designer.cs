namespace HearMe
{
    partial class LoggedinForm
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
            this.friendSearchCriteria = new System.Windows.Forms.TextBox();
            this.addFriend = new System.Windows.Forms.Button();
            this.avatar = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.exit = new System.Windows.Forms.PictureBox();
            this.header = new System.Windows.Forms.PictureBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.friendPanel = new System.Windows.Forms.Panel();
            this.changeAvatar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.header)).BeginInit();
            this.SuspendLayout();
            // 
            // friendSearchCriteria
            // 
            this.friendSearchCriteria.Location = new System.Drawing.Point(714, 81);
            this.friendSearchCriteria.Name = "friendSearchCriteria";
            this.friendSearchCriteria.Size = new System.Drawing.Size(234, 20);
            this.friendSearchCriteria.TabIndex = 30;
            // 
            // addFriend
            // 
            this.addFriend.Location = new System.Drawing.Point(688, 540);
            this.addFriend.Name = "addFriend";
            this.addFriend.Size = new System.Drawing.Size(260, 23);
            this.addFriend.TabIndex = 33;
            this.addFriend.Text = "ADD FRIEND";
            this.addFriend.UseVisualStyleBackColor = true;
            this.addFriend.Click += new System.EventHandler(this.addFriend_Click);
            // 
            // avatar
            // 
            this.avatar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.avatar.Image = global::HearMe.Properties.Resources.avatar0;
            this.avatar.Location = new System.Drawing.Point(18, 18);
            this.avatar.Name = "avatar";
            this.avatar.Size = new System.Drawing.Size(40, 40);
            this.avatar.TabIndex = 34;
            this.avatar.TabStop = false;
            this.avatar.Click += new System.EventHandler(this.avatar_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::HearMe.Properties.Resources.search;
            this.pictureBox2.Location = new System.Drawing.Point(688, 81);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HearMe.Properties.Resources.verticalLine;
            this.pictureBox1.Location = new System.Drawing.Point(662, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 499);
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // minimize
            // 
            this.minimize.Image = global::HearMe.Properties.Resources.minimize;
            this.minimize.Location = new System.Drawing.Point(902, 12);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(20, 20);
            this.minimize.TabIndex = 28;
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
            this.exit.TabIndex = 27;
            this.exit.TabStop = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            this.exit.MouseEnter += new System.EventHandler(this.exit_MouseEnter);
            this.exit.MouseLeave += new System.EventHandler(this.exit_MouseLeave);
            // 
            // header
            // 
            this.header.Image = global::HearMe.Properties.Resources.header;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(960, 75);
            this.header.TabIndex = 1;
            this.header.TabStop = false;
            this.header.MouseDown += new System.Windows.Forms.MouseEventHandler(this.header_MouseDown);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(64, 34);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(61, 24);
            this.nameLabel.TabIndex = 35;
            this.nameLabel.Text = "Name";
            // 
            // friendPanel
            // 
            this.friendPanel.AutoScroll = true;
            this.friendPanel.Location = new System.Drawing.Point(688, 112);
            this.friendPanel.Name = "friendPanel";
            this.friendPanel.Size = new System.Drawing.Size(272, 417);
            this.friendPanel.TabIndex = 29;
            // 
            // changeAvatar
            // 
            this.changeAvatar.AutoSize = true;
            this.changeAvatar.Location = new System.Drawing.Point(65, 18);
            this.changeAvatar.Name = "changeAvatar";
            this.changeAvatar.Size = new System.Drawing.Size(98, 13);
            this.changeAvatar.TabIndex = 36;
            this.changeAvatar.Text = "CHANGE AVATAR";
            this.changeAvatar.Click += new System.EventHandler(this.changeAvatar_Click);
            // 
            // LoggedinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(960, 575);
            this.Controls.Add(this.changeAvatar);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.avatar);
            this.Controls.Add(this.addFriend);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.friendSearchCriteria);
            this.Controls.Add(this.friendPanel);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoggedinForm";
            this.Text = "LoggedInForm";
            ((System.ComponentModel.ISupportInitialize)(this.avatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.header)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox header;
        private System.Windows.Forms.PictureBox exit;
        private System.Windows.Forms.PictureBox minimize;
        private System.Windows.Forms.TextBox friendSearchCriteria;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button addFriend;
        private System.Windows.Forms.PictureBox avatar;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Panel friendPanel;
        private System.Windows.Forms.Label changeAvatar;
    }
}