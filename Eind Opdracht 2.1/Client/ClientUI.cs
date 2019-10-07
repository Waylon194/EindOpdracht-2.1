using System;
using System.Windows.Forms;

namespace Client
{
    public partial class ClientUI : Form
    {
        private int appID = 0;
        public ClientUI(int appID)
        {
            InitializeComponent();
            this.appID = appID;
        }

        private void ClientUI_Load(object sender, EventArgs e)
        {
            //Show the appID as a test
            MessageBox.Show(appID.ToString());
        }
    }
}