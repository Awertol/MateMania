using MateMania.Models;

namespace MateMania;

public partial class CreateClassPage : ContentPage
{
	string[] kraje = 
	{
		"Hlavn� m�sto Praha", "St�edo�esk�", "Jiho�esk�", "Plze�sk�", "Karlovarsk�", "�steck�", "Libereck�", "Kr�lovehradeck�", "Pardubick�", "Vyso�ina", "Jihomoravsk�", "Olomouck�", "Moravskoslezsk�", "Zl�nsk�" 
	};
	string[] okresy;
    string[] tridy = { "1", "2", "3" };
	Dictionary<string, string[]> slovnikOkresy = new Dictionary<string, string[]>
	{
		{"Hlavn� m�sto Praha", new string[] {"Praha"} },
        {"St�edo�esk�",new string[] {"Bene�ov", "Beroun", "Kladno", "Kol�n", "Kutn� Hora", "M�ln�k", "Mlad� Boleslav", "Nymburk", "Praha-v�chod", "Praha-z�pad", "P��bram", "Rakovn�k"}},
        {"Jiho�esk�", new string[] {"�esk� Bud�jovice", "�esk� Krumlov", "Jind�ich�v Hradec", "P�sek", "Prachatice", "Strakonice", "T�bor"} },
        {"Plze�sk�", new string[] {"Doma�lice", "Klatovy", "Plze�-jih", "Plze�-m�sto", "Plze�-sever", "Rokycany", "Tachov"} },
        {"Karlovarsk�", new string[] {"Cheb", "Karlovy Vary", "Sokolov"} },
        {"�steck�",new string[] {"D���n", "Chomutov", "Litom��ice", "Louny", "Most", "Teplice", "�st� nad Labem"} },
        {"Libereck�",new string[] {"�esk� L�pa", "Jablonec nad Nisou", "Liberec", "Semily"} },
        {"Kr�lovehradeck�",new string[] {"Hradec Kr�lov�", "Ji��n", "N�chod", "Rychnov nad Kn�nou", "Trutnov"} },
        {"Pardubick�", new string[] {"Chrudim", "Pardubice", "Svitavy", "�st� nad Orlic�"} },
        {"Vyso�ina",new string[] {"Havl��k�v Brod", "Jihlava", "Pelh�imov", "T�eb��", "���r nad S�zavou"} },
        {"Jihomoravsk�",new string[] {"Blansko", "Brno-m�sto", "Brno-venkov", "B�eclav", "Hodon�n", "Vy�kov", "Znojmo"} },
        {"Olomouck�",new string[] {"Jesen�k", "Olomouc", "Prost�jov", "P�erov", "�umperk"} },
        {"Moravskoslezsk�",new string[] {"Brunt�l", "Fr�dek-M�stek", "Karvin�", "Nov� Ji��n", "Opava", "Ostrava-m�sto"} },
        {"Zl�nsk�",new string[] {"Krom���", "Uhersk� Hradi�t�", "Vset�n", "Zl�n"} }
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
		DisplayAlert("�sp�ch", "T��da byla vytvo�ena", "OK");
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}