using CryptoCoins.Helpers;
using CryptoCoins.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace CryptoCoins.Views;

public partial class LoginView : ContentView
{
    DashboardView dashboardView;
    VerifyCode verifycode;
    public LoginView()
    {
        InitializeComponent();
    }

    private async void btnSignIn_Clicked(object sender, EventArgs e)
    {

        try
        {
            string username = usernameEntry.Text;
            string password = passwordEntry.Text;
            User.username = username;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                (bool isAuthenticated, string userRole, int id , string email , string userpassword) = AuthenticateUser(username, password);

                if (isAuthenticated)
                {
                    if (userRole == "admin")
                    {
                        StGlobal.IsAdmin = true;
                        StGlobal.Id = id;
                        //userRole = userRole;
                    }
                    else
                    {
                        StGlobal.IsAdmin = false;
						StGlobal.Id = id;
                        userRole = "user";
                        StGlobal.EmailId = email;
                        StGlobal.password = userpassword;
                    }
                    // Get a reference to the MainPage
                    var mainPage = Application.Current.MainPage;

                    // You can now access properties and methods of the MainPage
                    if (mainPage is MainPage)
                    {
                        // Cast to the correct type
                        var mainPageInstance = (MainPage)mainPage;
                        dashboardView = new DashboardView();
                        mainPageInstance.Content = dashboardView;
                    }

                    // User is authenticated, navigate to the main page or perform other actions
                    //await Shell.Current.GoToAsync("//MainPage");
                }
                else
                {
                    // Display an error message or handle failed authentication
                    //await DisplayAlert("Login Failed", "Invalid username or password", "OK");

                    await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Please enter username and password", "OK");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Debug.WriteLine(ex.StackTrace);
        }
    }

    private (bool, string, int, string, string) AuthenticateUser(string username, string password)
    {
        bool isAuthenticated = false;
        string userRole = string.Empty;
        int id = 0;
        string email = string.Empty;
        string userPassword = string.Empty;

        using (MySqlConnection connection = StGlobal.GetSqlConnection())
        {
            connection.Open();

            string query = "SELECT * FROM Users WHERE username = @username AND password = @password";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            MySqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                isAuthenticated = true;
                id = reader.GetInt32(reader.GetOrdinal("Id"));
                email = reader.GetString(reader.GetOrdinal("email"));
                userPassword = reader.GetString(reader.GetOrdinal("password"));
                userRole = reader.GetString(reader.GetOrdinal("user_role"));
            }

            reader.Close();
        }

        return (isAuthenticated, userRole, id, email, userPassword);
    }

    private void OnSignUpLabelTapped(object sender, TappedEventArgs e)
    {
        // Get a reference to the MainPage
        var mainPage = Application.Current.MainPage;

        // You can now access properties and methods of the MainPage
        if (mainPage is MainPage)
        {
            // Cast to the correct type
            var mainPageInstance = (MainPage)mainPage;
            SignupView signupView = new SignupView(this);
            mainPageInstance.Content = signupView;
        }
    }
    private void OnForgotPasswordTapped(object sender, TappedEventArgs e)
    {
        // Get a reference to the MainPage
        var mainPage = Application.Current.MainPage;

        // You can now access properties and methods of the MainPage
        if (mainPage is MainPage)
        {
            // Cast to the correct type
            var mainPageInstance = (MainPage)mainPage;
            AddEmail signupView = new AddEmail(verifycode);
            mainPageInstance.Content = signupView;
        }
    }
}