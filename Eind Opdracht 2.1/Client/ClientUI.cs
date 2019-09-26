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