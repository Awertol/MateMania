using MateMania.Models;
using System.Text.RegularExpressions;

namespace MateMania;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        InitializeComponent();
		if(accessType != NetworkAccess.Internet)
		{
			btnLogin.IsEnabled = false;
			btnRegistrovat.IsEnabled = false;
			entryUzJmeno.IsEnabled = false;
			entryUzHeslo.IsEnabled = false;
			OfflineOnline.stavPripojeni = false;
		}
		else
		{
			DbData.NastavClient();
            OfflineOnline.stavPripojeni = true;
        }
	}

    private async void btnLogin_Clicked(object sender, EventArgs e)
    {
		OfflineOnline.stavPripojeni = true;
		string uzJmeno = entryUzJmeno.Text.Trim();
		string uzHeslo = entryUzHeslo.Text.Trim();
        bool isOnlyDigits = Regex.IsMatch(uzHeslo, @"^\d+$");
		if (isOnlyDigits)
		{
			Task<UserModel> uzivatel = DbData.LoginUzivatele(uzJmeno, uzHeslo);
			UserModel uz = await uzivatel;
			if (uz != null)
			{
				MenuPage menuStranka = new MenuPage();
				Navigation.PushAsync(menuStranka);
			}
			else
			{
				DisplayAlert("Chyba", "Neplatné přihlašovací údaje", "OK");
			}
		}
    }

    private void btnRegistrovat_Clicked(object sender, EventArgs e)
    {
		RegisterPage registracniStranka = new RegisterPage();
		Navigation.PushAsync(registracniStranka);
    }
    private void btnOffline_Clicked(object sender, EventArgs e)
    {
        OfflineOnline.stavPripojeni = false;
        MenuPage menuStranka = new MenuPage();
		Navigation.PushAsync(menuStranka);
    }
}

