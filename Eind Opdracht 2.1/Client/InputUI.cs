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
        UserClient userClient;
        public InputUI(UserClient userClient)
        {
            this.userClient = userClient;
            InitializeComponent();
        }

        private void InputUI_Load(object sender, EventArgs e)
        {

        }

        private void BtnRequestData_Click(object sender, EventArgs e)
        {
            //Show the ClientUI form and send the entered app ID
            int submittedAppId;
           
            bool succes = Int32.TryParse(this.txtAppID.Text, out submittedAppId);
            if (succes)
            {
                //Send the appID to the next form (ClientUI)
                this.userClient.SendSteamID(submittedAppId);
                this.Hide();
                ClientUI clientUI = new ClientUI(submittedAppId, this, this.userClient);
                clientUI.Show();
            }
            else
            {
                MessageBox.Show("This is not a valid input, please only enter numerical characters");
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.userClient.SendGoodbye();
            MessageBox.Show($"Goodbye! {this.userClient.UserName}");
            this.Close();
        }
    }
}