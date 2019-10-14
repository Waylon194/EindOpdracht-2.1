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

        private async void BtnRequestData_Click(object sender, EventArgs e)
        {
            //Show the ClientUI form and send the entered app ID
            int submittedAppId;
           
            bool succes = Int32.TryParse(this.txtAppID.Text, out submittedAppId);
            if (succes)
            {
                //Send the appID to the next form (ClientUI)
                this.userClient.SendSteamID(submittedAppId);
                this.Hide();
                ClientUI ui = await WaitForClientUIAsync(submittedAppId);
                ui.Show();
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
        
        private async Task<ClientUI> WaitForClientUIAsync(int id) 
        {
            await Task.Delay(1200);
            ClientUI clientUI = new ClientUI(id, this, this.userClient);
            return clientUI;
        }
    }
}