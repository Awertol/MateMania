using MateMania.Models;

namespace MateMania;

public partial class CreateClassPage : ContentPage
{
	string[] kraje = 
	{
		"Hlavní mìsto Praha", "Støedoèeský", "Jihoèeský", "Plzeòský", "Karlovarský", "Ústecký", "Liberecký", "Královehradecký", "Pardubický", "Vysoèina", "Jihomoravský", "Olomoucký", "Moravskoslezský", "Zlínský" 
	};
	string[] okresy;
    string[] tridy = { "1", "2", "3" };
	Dictionary<string, string[]> slovnikOkresy = new Dictionary<string, string[]>
	{
		{"Hlavní mìsto Praha", new string[] {"Praha"} },
        {"Støedoèeský",new string[] {"Benešov", "Beroun", "Kladno", "Kolín", "Kutná Hora", "Mìlník", "Mladá Boleslav", "Nymburk", "Praha-východ", "Praha-západ", "Pøíbram", "Rakovník"}},
        {"Jihoèeský", new string[] {"Èeské Budìjovice", "Èeský Krumlov", "Jindøichùv Hradec", "Písek", "Prachatice", "Strakonice", "Tábor"} },
        {"Plzeòský", new string[] {"Domažlice", "Klatovy", "Plzeò-jih", "Plzeò-mìsto", "Plzeò-sever", "Rokycany", "Tachov"} },
        {"Karlovarský", new string[] {"Cheb", "Karlovy Vary", "Sokolov"} },
        {"Ústecký",new string[] {"Dìèín", "Chomutov", "Litomìøice", "Louny", "Most", "Teplice", "Ústí nad Labem"} },
        {"Liberecký",new string[] {"Èeská Lípa", "Jablonec nad Nisou", "Liberec", "Semily"} },
        {"Královehradecký",new string[] {"Hradec Králové", "Jièín", "Náchod", "Rychnov nad Knìžnou", "Trutnov"} },
        {"Pardubický", new string[] {"Chrudim", "Pardubice", "Svitavy", "Ústí nad Orlicí"} },
        {"Vysoèina",new string[] {"Havlíèkùv Brod", "Jihlava", "Pelhøimov", "Tøebíè", "Žïár nad Sázavou"} },
        {"Jihomoravský",new string[] {"Blansko", "Brno-mìsto", "Brno-venkov", "Bøeclav", "Hodonín", "Vyškov", "Znojmo"} },
        {"Olomoucký",new string[] {"Jeseník", "Olomouc", "Prostìjov", "Pøerov", "Šumperk"} },
        {"Moravskoslezský",new string[] {"Bruntál", "Frýdek-Místek", "Karviná", "Nový Jièín", "Opava", "Ostrava-mìsto"} },
        {"Zlínský",new string[] {"Kromìøíž", "Uherské Hradištì", "Vsetín", "Zlín"} }
    };
	public CreateClassPage()
	{
		InitializeComponent();
		pckKraj.ItemsSource = kraje;
		pckKraj.SelectedIndex = 0;
		okresy = VratOkres();
		pckOkres.ItemsSource = okresy;
		pckOkres.SelectedIndex = 0;
		pckTrida.ItemsSource = tridy;
		pckTrida.SelectedIndex = 0;
	}
	private string[] VratOkres()
	{
		string kraj = pckKraj.SelectedItem as string;
		string[] okresyZeSlovniku = slovnikOkresy[kraj];
		return okresyZeSlovniku;
	}

    private void pckKraj_SelectedIndexChanged(object sender, EventArgs e)
    {
		okresy = VratOkres();
		pckOkres.ItemsSource = okresy;
    }

    private void btnPridat_Clicked(object sender, EventArgs e)
    {
		ClassModel vytvorenaTrida = new ClassModel();
		vytvorenaTrida.Grade = Convert.ToInt32(pckTrida.SelectedItem as string);
		vytvorenaTrida.SchoolName = entNazevSkoly.Text.Trim();
		vytvorenaTrida.City = entMesto.Text.Trim();
		vytvorenaTrida.Region = pckKraj.SelectedItem.ToString();
        vytvorenaTrida.District = pckOkres.SelectedItem.ToString();
		DbData.VytvoritTridu(vytvorenaTrida);
		DisplayAlert("Úspìch", "Tøída byla vytvoøena", "OK");
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}