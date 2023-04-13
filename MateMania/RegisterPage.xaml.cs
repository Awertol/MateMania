namespace MateMania;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    private void btnRegistrovat_Clicked(object sender, EventArgs e)
    {
		string jmeno = entryPrezdivka.Text;
		jmeno = jmeno.Trim();
		string heslo = "";
		if(jmeno != "")
		{
			for (int i = 0; i < 5; i++)
			{
				int nahodne = new Random().Next(0, 10);
				heslo += nahodne.ToString();
			}
			DbData.RegistrovatUzivatele(jmeno, heslo);
			DisplayAlert("Tv� heslo: ", heslo + " \n(m��e� si ho kdykoli zm�nit :)", "OK");
			Navigation.PopAsync();
		}
		else
		{
			DisplayAlert("N�co se nepovedlo", "Zkus to znovu", "OK");
		}
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}