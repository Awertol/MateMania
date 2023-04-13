namespace MateMania;

public partial class MedalsPage : ContentPage
{
	public MedalsPage()
	{
		InitializeComponent();
		lbBronze.Text = DbData.nactenyUzivatel.BronzeMedals.ToString();
        lbSilver.Text = DbData.nactenyUzivatel.SilverMedals.ToString();
        lbGold.Text = DbData.nactenyUzivatel.GoldMedals.ToString();
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}