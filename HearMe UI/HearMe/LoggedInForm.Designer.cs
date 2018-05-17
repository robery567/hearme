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
            this.header = new System.Windows.Forms.PictureBox();
            this.exit = new System.Windows.Forms.PictureBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.header)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            this.SuspendLayout();
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
            // LoggedinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(960, 575);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoggedinForm";
            this.Text = "LoggedInForm";
            ((System.ComponentModel.ISupportInitialize)(this.header)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox header;
        private System.Windows.Forms.PictureBox exit;
        private System.Windows.Forms.PictureBox minimize;
    }
}