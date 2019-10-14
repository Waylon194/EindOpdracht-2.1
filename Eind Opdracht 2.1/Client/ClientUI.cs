using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;

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
            try
            {
                // Load all the components on the UI
                // MessageBox.Show(appID.ToString()); // tester
                LoadImage(this.UserClient.SteamDataJSON.data.header_image, this.pictureHeader);
                this.lblName.Text = this.UserClient.SteamDataJSON.data.name;
                this.lblReleaseDate.Text = this.UserClient.SteamDataJSON.data.release_date.date; /* + " (" + (this.UserClient.SteamDataJSON.data.release_data.date-DateTime.Now) + ") years old";*/
                this.lblDeveloper.Text += this.UserClient.SteamDataJSON.data.developers[0];
                this.lblPublisher.Text += this.UserClient.SteamDataJSON.data.publishers[0];

                if ((bool)this.UserClient.SteamDataJSON.data.is_free)
                {
                    this.lblFreeToPlay.Text = "Free To Play";
                }
                else
                {
                    this.lblFreeToPlay.Text = this.UserClient.SteamDataJSON.data.price_overview.final_formatted;
                }

                if (this.UserClient.SteamDataJSON.data.about_the_game == null)
                {
                    this.lblNotes.Text = "About: No note has been set for this game";
                }
                else
                {
                    string x = Regex.Replace((string) this.UserClient.SteamDataJSON.data.about_the_game, "<br />", " ");
                    x = Regex.Replace(x, @"\n\r", "");
                    x = Regex.Replace(x, "quot;", "");
                    this.lblNotes.Text = "About the game:\n" + x;
                }
            }
            catch (RuntimeBinderException)
            {
                MessageBox.Show("Wrong ID or something went wrong, try again");
                this.InputUI.Show();
                this.Close();
            }
           
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.InputUI.Show();
            this.Close();
        }


        private void LoadImage(dynamic location, PictureBox box)
        {
            string x =  (string) location;
            box.LoadAsync(x);
        }
    }
}