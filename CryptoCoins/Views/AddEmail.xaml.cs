using CryptoCoins.Helpers;
using CryptoCoins.Models;
using MySql.Data.MySqlClient;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CryptoCoins.Views
{
    public partial class AddEmail : ContentView
    {
        private VerifyCode loginViewObj;
        private LoginView LoginView;
        private static Random random = new Random();

        public AddEmail(VerifyCode loginView)
        {
            InitializeComponent();
            loginViewObj = loginView;
        }

        private async void btn_Clicked(object sender, EventArgs e)
        {
            int code = GenerateRandomCode(6);
            VerifyUser.verifyEmail = usernameEntry.Text;
            bool check = add_data(usernameEntry.Text, code);

            if (check)
            {
                await MailSend("fezanyounas191706@gmail.com", code);
                await Application.Current.MainPage.DisplayAlert("Email Sent", "Please check for 6 digit to the email", "OK");
                // Get a reference to the MainPage
                var mainPage = Application.Current.MainPage;

                // You can now access properties and methods of the MainPage
                if (mainPage is MainPage)
                {
                    // Cast to the correct type
                    var mainPageInstance = (MainPage)mainPage;
                    VerifyCode signupView = new VerifyCode(LoginView);
                    mainPageInstance.Content = signupView;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add data to database", "OK");
            }
        }

        private void OnbackTapped(object sender, EventArgs e)
        {
            var mainPage = Application.Current.MainPage;

            // You can now access properties and methods of the MainPage
            if (mainPage is MainPage)
            {
                // Cast to the correct type
                var mainPageInstance = (MainPage)mainPage;
                LoginView signupView = new LoginView();
                mainPageInstance.Content = signupView;
            }
        }

        private bool add_data(string userEmailAddress, int code)
        {
            try
            {
                using (MySqlConnection connection = StGlobal.GetSqlConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO VerificationCodes (user_email, code) VALUES (@user_email, @code)";

                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@user_email", userEmailAddress);
                        cmd.Parameters.AddWithValue("@code", code);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding data to database: " + ex.Message);
                return false;
            }
        }

        private async Task MailSend(string email, int code)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                smtpServer.Port = 587;
                smtpServer.Credentials = new System.Net.NetworkCredential("css.hamad160@gmail.com", "dmrv mzbo zgsd vacf");
                smtpServer.EnableSsl = true;

                mail.From = new MailAddress("css.hamad160@gmail.com", "Crypto Coin");
                mail.To.Add(email);
                mail.Subject = "Recovery: 6 digit code";
                mail.Body = $"Verify your account with this code: {code}";

                await smtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                throw; // Rethrow the exception to propagate it up the call stack
            }
        }

        private static int GenerateRandomCode(int length)
        {
            const string chars = "0123456789";
            int code = 0;

            for (int i = 0; i < length; i++)
            {
                int digit = random.Next(chars.Length);
                code = code * 10 + digit; // Append the new digit to the code
            }

            return code;
        }
    }
}
