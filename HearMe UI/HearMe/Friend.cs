using HearMe.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HearMe
{
    public class Friend
    {
        public PictureBox avatar { get; set; }
        public Label name { get; set; }
        public Label email { get; set; }

        public Friend()
        {
            avatar = new PictureBox();
            name = new Label();
            email = new Label();

            avatar.Image = (Image)Resources.ResourceManager.GetObject("avatar0");
            avatar.Size = new System.Drawing.Size(40, 40);
            name.Font = new Font("Microsoft Sans Serif", 12);
            name.Width = email.Width = 200;
        }
    }
}
