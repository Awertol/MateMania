namespace MateMania;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
	}

    private async void chbUcitel_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		if(chbUcitel.IsChecked)
		{
            string result = await DisplayPromptAsync("Otázka", "Kolik je XVII na druhou?");
			if(result == "289")
			{
                await DisplayAlert("Odpovìï", "Správnì! Mùžeš vstoupit do režimu uèitele", "OK");
                btnRezimUcitele.IsVisible = true;
            }
            else
			{
                await DisplayAlert("Odpovìï", "Špatnì!", "OK");
            }
        }
		else if(!chbUcitel.IsChecked)
		{
			btnRezimUcitele.IsVisible=false;
		}
    }

    private void btnZmenaUdaju_Clicked(object sender, EventArgs e)
    {
        ChangeInfoPage zmenaUdajuStranka = new ChangeInfoPage();
        Navigation.PushAsync(zmenaUdajuStranka);
    }
}