using MateMania.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace MateMania;

internal static class ResultPageAnti
{
    public static bool Again = false;
}
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
		if(OfflineOnline.stavPripojeni == true && ResultPageAnti.Again == false)
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
            ResultPageAnti.Again = false;
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