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
			DisplayAlert("Tvé heslo: ", heslo + " \n(mùžeš si ho kdykoli zmìnit :)", "OK");
			Navigation.PopAsync();
		}
		else
		{
			DisplayAlert("Nìco se nepovedlo", "Zkus to znovu", "OK");
		}
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}