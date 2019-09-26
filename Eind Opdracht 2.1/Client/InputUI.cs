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
    public partial class InputUI : Form
    {
        public InputUI()
        {
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

            //Send the appID to the next form (ClientUI)

            this.Hide();
            new ClientUI(submittedAppId).Show();
        }
    }
}