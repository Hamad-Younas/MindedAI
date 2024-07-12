using CryptoCoins.Views;

namespace CryptoCoins
{
    public partial class MainPage : ContentPage
    {
        LoginView loginView;
        public MainPage()
        {
            InitializeComponent();
            loginView = new LoginView();
            this.Content = loginView;
        }
    }
}
