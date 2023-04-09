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
		}
	}

    private void btnLogin_Clicked(object sender, EventArgs e)
    {
		//if()
		//{

		//}
		MenuPage menuStranka = new MenuPage(entryUzJmeno.Text);
		Navigation.PushAsync(menuStranka);
    }

    private void btnRegistrovat_Clicked(object sender, EventArgs e)
    {
		RegisterPage registracniStranka = new RegisterPage();
		Navigation.PushAsync(registracniStranka);
    }
    private void btnOffline_Clicked(object sender, EventArgs e)
    {

    }
}

