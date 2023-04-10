namespace MateMania;

public partial class ChangeInfoPage : ContentPage
{
	public ChangeInfoPage()
	{
		InitializeComponent();
	}

    private void btnZmenit_Clicked(object sender, EventArgs e)
    {
		if(entPrezdivka.Text != null)
		{
			DbData.nactenyUzivatel.Nickname = entPrezdivka.Text.Trim();
			DbData.ZmenitUdaj("Nickname", entPrezdivka.Text.Trim());
		}
		if(entJmeno.Text != null)
		{
            DbData.nactenyUzivatel.Firstname = entJmeno.Text.Trim();
            DbData.ZmenitUdaj("Firstname", entJmeno.Text.Trim());
        }
		if(entPrijmeni.Text != null)
		{
			DbData.nactenyUzivatel.Surname = entPrijmeni.Text.Trim();
            DbData.ZmenitUdaj("Surname", entPrijmeni.Text.Trim());
        }
		if(entNoveHeslo.Text.Length == 5)
		{
			int passCheck = 0;
			for(int i = 0; i<entNoveHeslo.Text.Trim().Length; i++)
			{
				if (char.IsAsciiDigit(entNoveHeslo.Text.Trim()[i]))
				{
					passCheck++;
				}
			}
			if(passCheck == 5)
			{
				DbData.nactenyUzivatel.UserPassword = entNoveHeslo.Text.Trim();
                DbData.ZmenitUdaj("Password", entNoveHeslo.Text.Trim());
                DisplayAlert("Nové heslo", $"Bylo nastaveno nové heslo:\n{entNoveHeslo.Text.Trim()}", "OK");
			}
			else
			{
				DisplayAlert("Chyba", "Špatnì zadané heslo, musí obsahovat pøesnì 5 èísel", "OK");
			}
		}
        else
        {
            DisplayAlert("Chyba", "Špatnì zadané heslo, musí obsahovat pøesnì 5 èísel", "OK");
        }

    }
}