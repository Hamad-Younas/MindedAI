using CryptoCoins.Helpers;
using CryptoCoins.Models;
using MySql.Data.MySqlClient;

namespace CryptoCoins.Views;

public partial class NewPassword : ContentView
{
	public NewPassword()
	{
		InitializeComponent();
	}
	private async void btnPass_Clicked(object sender, EventArgs e)
	{ 
        if (passwordEntry.Text == passwordEntry1.Text) 
        {
            bool check = UpdatePassword(VerifyUser.verifyEmail, passwordEntry.Text);
            if (check)
            {
                await Application.Current.MainPage.DisplayAlert("Password", "Your password is changed", "OK");
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
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please check your connection", "OK");
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Please enter same password", "OK");
        }
    }
    public static bool UpdatePassword(string userEmail, string newPassword)
    {
        try
        {
            using (MySqlConnection connection = StGlobal.GetSqlConnection())
            {
                connection.Open();

                string updateQuery = "UPDATE Users SET password = @password WHERE email = @user_email";

                using (MySqlCommand command = new MySqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@password", newPassword);
                    command.Parameters.AddWithValue("@user_email", userEmail);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error updating password: " + ex.Message);
            return false;
        }
    }
}