using CryptoCoins.Helpers;
using CryptoCoins.Models;
using MySql.Data.MySqlClient;

namespace CryptoCoins.Views;

public partial class VerifyCode : ContentView
{
    private LoginView loginViewObj;
    public VerifyCode(LoginView loginView)
	{
		InitializeComponent();
        loginViewObj = loginView;
    }
	private async void btn_Clicked(object sender, EventArgs e)
	{
        string email = VerifyUser.verifyEmail;
        int code = GetLatestVerificationCode(email);
        if (code == int.Parse(usernameEntry.Text))
        {
            var mainPage = Application.Current.MainPage;

            // You can now access properties and methods of the MainPage
            if (mainPage is MainPage)
            {
                // Cast to the correct type
                var mainPageInstance = (MainPage)mainPage;
                NewPassword signupView = new NewPassword();
                mainPageInstance.Content = signupView;
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Failed to get data from database", "OK");
        }
	}
    public static int GetLatestVerificationCode(string userEmailAddress)
    {
        int latestCode = 0;

        try
        {
            using (MySqlConnection connection = StGlobal.GetSqlConnection())
            {
                connection.Open();

                string query = "SELECT code FROM VerificationCodes WHERE user_email = @user_email ORDER BY Id DESC LIMIT 1";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@user_email", userEmailAddress);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            latestCode = Convert.ToInt32(reader["code"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error retrieving latest verification code: " + ex.Message);
        }

        return latestCode;
    }
    private void OnbackTapped(object sender, TappedEventArgs e)
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
}