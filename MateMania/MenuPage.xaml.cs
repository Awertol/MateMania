namespace MateMania;

public partial class MenuPage : ContentPage
{
	public MenuPage(string uzivJmeno)
	{
		InitializeComponent();
		lbUzivPrezdivka.Text = uzivJmeno;
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
}