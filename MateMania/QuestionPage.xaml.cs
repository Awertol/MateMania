namespace MateMania;

public partial class QuestionPage : ContentPage
{
	Dictionary<string, string> otazky = new Dictionary<string, string>
	{ 
		{ "Kino má 200 sedaèek. Postupnì pøišly 3 skupiny po 12 lidech, kolik nyní zbývá míst v kinì, když pøipoèteš sebe?", "163" }, 
		{ "Ve škole je 130 žákù a na školní výlet se chystáme jet všichni autobusy, který jeden pojme 40 osob. Kolik autobusù budeme potøebovat?", "4" },
		{ "Máme 30 sušenek a chceme je rozdìlit rovnomìrnì mezi 6 lidí. Kolik sušenek dostane každý?", "5" },
		{ "Mìli jsme na zahrádce 96 okurek. Z toho jsme 17 okurek dali zavaøovat a 13 okurek do salátu. Sousedùm jsme 3x dali krabici po 12 okurkách. Kolik okurek nám zbylo?", "30" } 
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
			DisplayAlert("Záhada vyøešena!", "Skvìlá práce, od nyní si mùžeš øíkat Øešitel záhad!", "OK");
			DbData.ZmenitTitul(6);
			Navigation.PopAsync();
		}
		else
		{
			DisplayAlert("Zkus to pøíštì!", "", "OK");
			Navigation.PopAsync();
		}
    }
}