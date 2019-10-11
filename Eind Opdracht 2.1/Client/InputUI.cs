using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class InputUI : Form
    {
        UserClient UserClient;
        public InputUI(UserClient userClient)
        {
            this.UserClient = userClient;
            InitializeComponent();
        }

        private void InputUI_Load(object sender, EventArgs e)
        {

        }

        private void BtnRequestData_Click(object sender, EventArgs e)
        {
            //Show the ClientUI form and send the entered app ID
            int submittedAppId = 0;
            Int32.TryParse(this.txtAppID.Text, out submittedAppId);
            this.UserClient.SendSteamID(submittedAppId);

            //Send the appID to the next form (ClientUI)


            this.Hide();
            ClientUI clientUI = new ClientUI(submittedAppId, this);
            clientUI.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.UserClient.SendGoodbye();
            MessageBox.Show($"Goodbye! {this.UserClient.UserName}");
            this.Close();
        }
    }
}