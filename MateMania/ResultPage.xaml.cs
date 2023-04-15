using MateMania.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace MateMania;

public partial class ResultPage : ContentPage
{
	public ResultPage(int vysledek)
	{
		InitializeComponent();
        lbVysledek.Text = $"{vysledek}/8";
        if (vysledek >= 7)
		{
			imgVysledek.Source = "gold.png";
		}
		else if(vysledek >= 5)
		{
            imgVysledek.Source = "silver.png";
        }
        else if(vysledek >= 3)
		{
            imgVysledek.Source = "bronze.png";
        }
		else
		{
            imgVysledek.IsVisible = false;
            lbVysledek.Text += "\n" + "\U0001F340";
        }
		if(OfflineOnline.stavPripojeni == true)
		{
            if (vysledek >= 7)
            {
                DbData.ZmenitMedaili(3);
            }
            else if (vysledek >= 5)
            {
                DbData.ZmenitMedaili(2);
            }
            else if (vysledek >= 3)
            {
                DbData.ZmenitMedaili(1);
            }
            if(DateTime.Now.Hour >= 22 || DateTime.Now.Hour < 6)
            {
                DbData.ZmenitTitul(2);
            }
        }
	}

    private async void btnMenu_Clicked(object sender, EventArgs e)
    {
        DbData.RefreshUzivatele();
        Navigation.PopAsync();
        Navigation.PopAsync();
        Navigation.PopAsync();
    }
}