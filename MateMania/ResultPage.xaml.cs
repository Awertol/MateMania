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
			imgVysledek.Source = "amogus.png";
		}
	}
}