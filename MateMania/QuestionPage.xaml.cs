namespace MateMania;

public partial class QuestionPage : ContentPage
{
	Dictionary<string, string> otazky = new Dictionary<string, string>
	{ 
		{ "A?", "1" }, 
		{ "B?", "2" },
		{ "C?", "3" },
		{ "D?", "4" } 
	};
	Random generator = new Random();
	string otazka = "";
	string odpoved = "";

	public QuestionPage()
	{
		InitializeComponent();
		otazka = otazky.ElementAt(generator.Next(0, otazky.Count)).Key;
		odpoved = otazky[otazka];
        lbOtazka.Text = otazka;
    }

    private void btnOdpovedZahada_Clicked(object sender, EventArgs e)
    {
		if(entryOdpovedZahada.Text.Trim() == odpoved)
		{
			DisplayAlert("Z�hada vy�e�ena!", "Skv�l� pr�ce, od nyn� si m��e� ��kat �e�itel z�had!", "OK");
			DbData.ZmenitTitul(6);
			Navigation.PopAsync();
		}
    }
}