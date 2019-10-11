using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class LoginWindow : Form
    {
        private UserClient UserClient;
        private InputUI InputUI;

        public LoginWindow(UserClient userClient)
        {
            InitializeComponent();
            this.UserClient = userClient;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.InputUI = new InputUI(this.UserClient);
            this.UserClient.SendUserName(this.txtBoxUsername.Text);
            this.InputUI.Show();
            this.Hide();
        }
    }
}
