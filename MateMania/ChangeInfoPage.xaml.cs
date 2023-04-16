using MateMania.Models;
using Microsoft.Maui;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MateMania;

public partial class ChangeInfoPage : ContentPage
{
    List<ClassModel> seznam = new();
	public ChangeInfoPage()
	{
		InitializeComponent();
		NajdiSkoly();
    }
	private async void NajdiSkoly()
	{
        Task<List<ClassModel>> taskNajitVsechnyTridy = DbData.NajitSkoly();
		seznam = await taskNajitVsechnyTridy;
    }
    private void btnZmenit_Clicked(object sender, EventArgs e)
    {
		if(entPrezdivka.Text != null && entPrezdivka.Text != "")
		{
			DbData.nactenyUzivatel.Nickname = entPrezdivka.Text.Trim();
			DbData.ZmenitUdaj("Nickname", entPrezdivka.Text.Trim());
		}
		if(entJmeno.Text != null && entJmeno.Text != "")
		{
            DbData.nactenyUzivatel.Firstname = entJmeno.Text.Trim();
            DbData.ZmenitUdaj("Firstname", entJmeno.Text.Trim());
        }
		if(entPrijmeni.Text != null && entPrijmeni.Text != "")
		{
			DbData.nactenyUzivatel.Surname = entPrijmeni.Text.Trim();
            DbData.ZmenitUdaj("Surname", entPrijmeni.Text.Trim());
        }
		if((entNoveHeslo.Text != "" && entNoveHeslo != null)&&entNoveHeslo.Text != DbData.nactenyUzivatel.UserPassword)
		{
			if (entNoveHeslo.Text.Length == 5)
			{
				int passCheck = 0;
				for (int i = 0; i < entNoveHeslo.Text.Trim().Length; i++)
				{
                    string kontrola = entNoveHeslo.ToString();
                    bool isOnlyDigits = Regex.IsMatch(kontrola, @"^\d+$");
                    if (isOnlyDigits)
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
			"Praha", "St�edo�esk�", "Jiho�esk�", "Plze�sk�", "Karlovarsk�", "�steck�", "Libereck�", "Kr�lovehradeck�", "Pardubick�", "Vyso�ina", "Jihomoravsk�", "Olomouck�", "Moravskoslezsk�", "Zl�nsk�"
		};
        Dictionary<string, string[]> slovnikOkresy = new Dictionary<string, string[]>
		{
			{"Praha", new string[] {"Praha"} },
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
                    var unikatniMestaVOkresu = seznam.Where(x => x.District == volbaKraje && x.Region == volbaOkresu).Select(x => x.City).Distinct().ToList();
					if (unikatniMestaVOkresu != null)
					{
						string volbaMesta = await DisplayActionSheet("M�STA: ", "Zru�it", null, unikatniMestaVOkresu.ToArray());
						if (volbaMesta != null && volbaMesta != "Zru�it")
						{
							List<string> seznamTridVMeste = seznam.Where(x => x.District == volbaKraje && x.Region == volbaOkresu && x.City == volbaMesta).Select(x => x.SchoolName + " | " + x.Grade + ". t�").Distinct().ToList();
							if (seznamTridVMeste != null)
							{
								string volbaTridy = await DisplayActionSheet("T��DY: ", "Zru�it", null, seznamTridVMeste.ToArray());
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
        }
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
        Navigation.PopAsync();
    }
}