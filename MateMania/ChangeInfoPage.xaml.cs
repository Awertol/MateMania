using MateMania.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MateMania;

public partial class ChangeInfoPage : ContentPage
{
    List<string> seznam = new List<string>();
	public ChangeInfoPage()
	{
		InitializeComponent();
    }
    private async Task<List<ClassModel>> Nacti(string kraj, string okres, string mesto)
    {
        seznam.Clear();
        Task<List<ClassModel>> taskTridy = DbData.NajitSkoly(kraj, okres, mesto);
        var nacteneTridy = await taskTridy;
        if (nacteneTridy == null)
        {
            seznam.Add("��dn� t��dy nebyly p�id�ny");
        }
		return nacteneTridy;
    }
    private async Task<List<ClassModel>> Nacti(string kraj, string okres)
	{
		seznam.Clear();
        Task<List<ClassModel>> taskMesta = DbData.NajitMesta(kraj, okres);
		var nactenaMesta = await taskMesta;
		if (nactenaMesta == null)
		{
			seznam.Add("��dn� t��dy nebyly p�id�ny");
		}
		return nactenaMesta;
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
		if((entNoveHeslo.Text != "" && entNoveHeslo != null)&&entNoveHeslo.Text == DbData.nactenyUzivatel.UserPassword)
		{
			if (entNoveHeslo.Text.Length == 5)
			{
				int passCheck = 0;
				for (int i = 0; i < entNoveHeslo.Text.Trim().Length; i++)
				{
					if (char.IsAsciiDigit(entNoveHeslo.Text.Trim()[i]))
					{
						passCheck++;
					}
				}
				if (passCheck == 5)
				{
					DbData.nactenyUzivatel.UserPassword = entNoveHeslo.Text.Trim();
					DbData.ZmenitUdaj("Password", entNoveHeslo.Text.Trim());
					DisplayAlert("Nov� heslo", $"Bylo nastaveno nov� heslo:\n{entNoveHeslo.Text.Trim()}", "OK");
				}
				else
				{
					DisplayAlert("Chyba", "�patn� zadan� heslo, mus� obsahovat p�esn� 5 ��sel", "OK");
				}
			}
		}
		DbData.RefreshUzivatele();
    }

    private async void btnZmenaTridy_Clicked(object sender, EventArgs e)
    {
        string[] kraje =
		{
			"Hlavn� m�sto Praha", "St�edo�esk�", "Jiho�esk�", "Plze�sk�", "Karlovarsk�", "�steck�", "Libereck�", "Kr�lovehradeck�", "Pardubick�", "Vyso�ina", "Jihomoravsk�", "Olomouck�", "Moravskoslezsk�", "Zl�nsk�"
		};
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
		bool odpovedZmena = await DisplayAlert("", "Opravdu chcete zm�n�t svou t��du?", "ANO", "NE");
		if(odpovedZmena)
		{
            string volbaKraje = await DisplayActionSheet("KRAJ: ", "Zru�it", null, kraje);
			if(volbaKraje != null && volbaKraje != "Zru�it")
			{
				string[] mozneOkresy = slovnikOkresy[volbaKraje];
                string volbaOkresu = await DisplayActionSheet("OKRESY: ", "Zru�it", null, mozneOkresy);
				if(volbaOkresu != null && volbaOkresu != "Zru�it")
				{
                    Task<List<ClassModel>> taskMesta = Nacti(volbaKraje, volbaOkresu);
					List<ClassModel> seznamMest = await taskMesta;
					foreach(var mesto in seznamMest)
					{
						seznam.Add(mesto.City);
					}
                    string volbaMesta = await DisplayActionSheet("M�STA: ", "Zru�it", null, seznam.ToArray());
					if(volbaMesta != null && volbaMesta != "Zru�it")
					{
                        Task<List<ClassModel>> taskTridyMesta = Nacti(volbaKraje, volbaOkresu, volbaMesta);
                        List<ClassModel> seznamTridVMeste = await taskTridyMesta;
                        foreach(var tridavemeste in seznamTridVMeste)
						{
							seznam.Add(tridavemeste.SchoolName + " | " + tridavemeste.Grade + ". t�");
						}
                        string volbaTridy = await DisplayActionSheet("T��DY: ", "Zru�it", null, seznam.ToArray());
						string skola = volbaTridy.Substring(0, volbaTridy.IndexOf("|")).Trim();
                        string trida = volbaTridy.Substring(volbaTridy.IndexOf("|") + 2, 1).Trim();
						DbData.ZmenitSkolu(skola, Convert.ToInt32(trida));
						DbData.ZmenitStavUc(false);
                        DbData.RefreshUzivatele();
                    }
                }
            }
        }
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
        Navigation.PopAsync();
    }
}