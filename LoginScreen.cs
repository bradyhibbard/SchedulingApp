using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace Scheduling_App
{
    public partial class LoginScreen : Form
    {
        private List<User> users;
        private string country;

        public LoginScreen()
        {
            InitializeComponent();
        }

        private void changeLoginToBrazilianPortuguese()
        {
            LoginScreenLabel.Text = "Tela de Login";
            UnameLabel.Text = "Nome de Usuário";
            PwdLabel.Text = "Senha de Acesso";
            LoginBtn.Text = "Entrar";
            ExitBtn.Text = "Sair";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = UnameTextBox.Text;
            string password = PwdTextBox.Text;

            try
            {
                if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                {
                    throw new LoginException("You need to provide both a User Name and Password to login.");
                }

                List<User> signedInUser = users.Where(user => user.UserName == userName).ToList();

                if (signedInUser.Count < 1)
                {
                    throw new LoginException("The User Name you entered does not exist.");
                }

                if (signedInUser[0].Password != password)
                {
                    throw new LoginException("The password is incorrect.");
                }

                Logging.logActivity(signedInUser[0]);

                var mainScreen = new MainScreen(signedInUser[0]);
                mainScreen.Show();
                Hide();
            }
            catch (LoginException error)
            {
                string errorMessage = "An error occurred during login. Please try again.";

                // Display the error message in a MessageBox
                MessageBox.Show(errorMessage, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            //country = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            users = Database.getAllUsers();
            ErrorLabel.Text = "";
            if (country == "pt")
            {
                changeLoginToBrazilianPortuguese();
            }
        }

        private void LoginScreen_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

