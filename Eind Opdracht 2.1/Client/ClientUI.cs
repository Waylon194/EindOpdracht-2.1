using System;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientUI : Form
    {
        private int appID = 0;
        private string text;
        private InputUI InputUI;

        public ClientUI(int appID, InputUI input)
        {
            this.InputUI = input;
            InitializeComponent();
            this.appID = appID;
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            //Show the appID as a test
            MessageBox.Show(appID.ToString());
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
    }
}