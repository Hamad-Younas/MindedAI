using CryptoCoins.Helpers;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace CryptoCoins.Views;

public partial class SignupView : ContentView
{
    public LoginView loginViewObj { get; set; }
    public SignupView(LoginView loginView)
	{
		InitializeComponent();
        loginViewObj = loginView;

    }

    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        //await DisplayAlert("Success", "User added successfully.", "OK");

        string username = UsernameEntry.Text;
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;
        string user_role = "user";

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(email))
        {
            using (MySqlConnection connection = StGlobal.GetSqlConnection())
            {
                connection.Open();

                // Inserting data into Users table
                string insertQuery = "INSERT INTO Users (username, email, password, user_role) VALUES (@username,@email, @password, @user_role)";
                using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                {
                    // Adding parameters to avoid SQL injection
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", password);
                    command.Parameters.AddWithValue("@user_role", user_role);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        //await DisplayAlert("Success", "User added successfully.", "OK");

                        // Get a reference to the MainPage
                        var mainPage = Application.Current.MainPage;

                        // You can now access properties and methods of the MainPage
                        if (mainPage is MainPage)
                        {
                            // Cast to the correct type
                            var mainPageInstance = (MainPage)mainPage;
                            //SignupView signupView = new SignupView(this);
                            mainPageInstance.Content = loginViewObj;
                        }
                        await MailSend(username);
                        await Application.Current.MainPage.DisplayAlert("Success", "Account created successfully", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Failed", "Account created failed please try agian", "OK");
                    }
                }
            } 
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Failed", "Please enter all details", "OK");
        }
    }
    private void OnSignInLabelTapped(object sender, TappedEventArgs e)
    {
        // Get a reference to the MainPage
        var mainPage = Application.Current.MainPage;
        var mainPageInstance = (MainPage)mainPage;
        mainPageInstance.Content = loginViewObj;
    }
    private async Task MailSend(string name)
    {
        try
        {
            MailMessage mail = new MailMessage();
            SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
            smtpServer.Port = 587;
            smtpServer.Credentials = new System.Net.NetworkCredential("css.hamad160@gmail.com", "dmrv mzbo zgsd vacf"); smtpServer.EnableSsl = true;


            mail.From = new MailAddress("css.hamad160@gmail.com", "Crypto Coin");
            mail.To.Add("fserh31@gmail.com");
            mail.Subject = "Reminder: New User";
            mail.Body = $"New user {name} registered the app now. Check it out.";


            smtpServer.Send(mail);
        }
        catch (Exception ex)
        {
        }
    }
}