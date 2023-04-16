namespace MateMania;

public partial class QuestionPage : ContentPage
{
	Dictionary<string, string> otazky = new Dictionary<string, string>
	{ 
		{ "Kino m� 200 seda�ek. Postupn� p�i�ly 3 skupiny po 12 lidech, kolik nyn� zb�v� m�st v kin�, kdy� p�ipo�te� sebe?", "163" }, 
		{ "Ve �kole je 130 ��k� a na �koln� v�let se chyst�me jet v�ichni autobusy, kter� jeden pojme 40 osob. Kolik autobus� budeme pot�ebovat?", "4" },
		{ "M�me 30 su�enek a chceme je rozd�lit rovnom�rn� mezi 6 lid�. Kolik su�enek dostane ka�d�?", "5" },
		{ "M�li jsme na zahr�dce 96 okurek. Z toho jsme 17 okurek dali zava�ovat a 13 okurek do sal�tu. Soused�m jsme 3x dali krabici po 12 okurk�ch. Kolik okurek n�m zbylo?", "30" } 
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
		else
		{
			DisplayAlert("Zkus to p��t�!", "", "OK");
			Navigation.PopAsync();
		}
    }
}