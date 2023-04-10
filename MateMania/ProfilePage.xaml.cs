namespace MateMania;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
		InitializeComponent();
        lbUzivPrezdivka.Text = DbData.nactenyUzivatel.Nickname;
        lbUzivJmeno.Text = DbData.nactenyUzivatel.Firstname != null ? DbData.nactenyUzivatel.Firstname + " " + DbData.nactenyUzivatel.Surname : "---";
        lbUzivZS.Text = DbData.nactenyUzivatel.ChosenClassID != null ? DbData.nactenyUzivatel.ChosenClassID.ToString() : "---";
        chbUcitel.IsChecked = DbData.nactenyUzivatel.IsTeacher != 0 ? true : false;
        lbUzivLevel.Text = "Level " + ExpCounter.Level.ToString();
        int min = 0, max = 0;
        if (ExpCounter.Level == 1)
        {
            min = 0;
            max = ExpCounter.LevelCaps[ExpCounter.Level - 1];
        }
        else if (ExpCounter.Level >= 1 && ExpCounter.Level <= 6)
        {
            min = ExpCounter.LevelCaps[ExpCounter.Level - 2];
            max = ExpCounter.LevelCaps[ExpCounter.Level - 1];
        }
        else if(ExpCounter.Level == 6)
        {
            pbUzivExp.Progress = 1.0;
        }
        double progress = (double)ExpCounter.Experience / max;
        pbUzivExp.Progress = progress;
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

    private void imgPostavaProfil_Clicked(object sender, EventArgs e)
    {
        AvatarPage postavyStranka = new AvatarPage();
        Navigation.PushAsync(postavyStranka);
    }
}