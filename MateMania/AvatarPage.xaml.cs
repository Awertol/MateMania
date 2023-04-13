namespace MateMania;

public partial class AvatarPage : ContentPage
{
	public AvatarPage()
	{
		InitializeComponent();
        imgPostava.Source = $"plusak_{DbData.nactenyUzivatel.Avatar}.png";
        if (ExpCounter.Experience > 2000)
		{
            imgVolba2.IsEnabled = true;
			lbVolba2Odemknuti.Text = "✅";
		}
		if(ExpCounter.Experience > 4500)
		{
            imgVolba3.IsEnabled = true;
            lbVolba3Odemknuti.Text = "✅";
        }
        if (ExpCounter.Experience > 7000)
        {
            imgVolba4.IsEnabled = true;
            lbVolba4Odemknuti.Text = "✅";
        }
        if (ExpCounter.Experience > 10000)
        {
            imgVolba5.IsEnabled = true;
            lbVolba5Odemknuti.Text = "✅";
        }
        if (ExpCounter.Experience > 15000)
        {
            imgVolba6.IsEnabled = true;
            lbVolba6Odemknuti.Text = "✅";
        }
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void imgVolba_Clicked(object sender, EventArgs e)
    {
        var imgVolba = sender as ImageButton;
        int idAvatarTlacitka = Convert.ToInt32(imgVolba.ClassId);
        imgPostava.Source = $"plusak_{idAvatarTlacitka}.png";
        DbData.ZmenitUdaj("Avatar", imgVolba.ClassId);
    }
}