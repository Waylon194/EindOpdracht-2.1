using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientUI : Form
    {
        private int appID = 0;
        private string text;
        private InputUI InputUI;
        private UserClient UserClient;

        public ClientUI(int appID, InputUI input, UserClient userClient)
        {
            this.UserClient = userClient;
            this.InputUI = input;
            InitializeComponent();
            this.appID = appID;
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            // Load all the components on the UI
            MessageBox.Show(appID.ToString()); // tester
            LoadImage(this.UserClient.SteamDataJSON.data.header_image, this.pictureHeader);
            
        }

        private delegate void SetTextDelegate(string text);

        public void SetText(string text) 
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new SetTextDelegate(SetText), new object[] { text });
                this.text = text;
                return;
            }
            this.text = text;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.InputUI.Show();
            this.Close();
        }

        private void LoadImage(dynamic location, PictureBox box)
        {
            string x = (string) location;
            box.LoadAsync(x);
        }

    }
}