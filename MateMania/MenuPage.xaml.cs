namespace MateMania;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
        if(OfflineOnline.stavPripojeni == false)
        {
            imgPostava.Source = "amogusred.png";
            imgPostava.Clicked -= imgPostava_Clicked;
            lbUzivPrezdivka.Text = "Offline";
            lbUzivTitul.IsVisible = false;
            pbUzivExp.IsVisible = false;
            btnOnline.IsVisible = false;
            btnVysledky.IsVisible = false;
            btnTituly.IsVisible = false;
        }
        else
        {
            lbUzivPrezdivka.Text = DbData.nactenyUzivatel.Nickname;
            lbUzivTitul.Text = "X";
            imgPostava.Source = "amogus.png";
            //imgPostava.Source = $"postava{DbData.nactenyUzivatel.Avatar}.png";
        }
	}

    private void btnTituly_Clicked(object sender, EventArgs e)
    {
		TitlePage titulyStranka = new TitlePage();
		Navigation.PushAsync(titulyStranka);
    }

    private void btnProcvicovani_Clicked(object sender, EventArgs e)
    {
		ClassChoice vyberTridy = new ClassChoice();
		Navigation.PushAsync(vyberTridy);
    }

    private void imgPostava_Clicked(object sender, EventArgs e)
    {
        ProfilePage profilStranka = new ProfilePage();
        Navigation.PushAsync(profilStranka);
    }

    private void btnVysledky_Clicked(object sender, EventArgs e)
    {
        MedalsPage medaileStranka = new MedalsPage();
        Navigation.PushAsync(medaileStranka);
    }

    private void btnOnline_Clicked(object sender, EventArgs e)
    {
        OnlinePinPage onlinePinPage = new OnlinePinPage();
        Navigation.PushAsync(onlinePinPage);
    }
}