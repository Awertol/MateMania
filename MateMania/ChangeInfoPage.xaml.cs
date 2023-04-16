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
					DisplayAlert("Nové heslo", $"Bylo nastaveno nové heslo:\n{entNoveHeslo.Text.Trim()}", "OK");
				}
				else
				{
					DisplayAlert("Chyba", "Špatnì zadané heslo, musí obsahovat pøesnì 5 èísel", "OK");
				}
			}
		}
		DbData.RefreshUzivatele();
    }

    private async void btnZmenaTridy_Clicked(object sender, EventArgs e)
    {
        string[] kraje =
		{
			"Praha", "Støedoèeský", "Jihoèeský", "Plzeòský", "Karlovarský", "Ústecký", "Liberecký", "Královehradecký", "Pardubický", "Vysoèina", "Jihomoravský", "Olomoucký", "Moravskoslezský", "Zlínský"
		};
        Dictionary<string, string[]> slovnikOkresy = new Dictionary<string, string[]>
		{
			{"Praha", new string[] {"Praha"} },
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
		bool odpovedZmena = await DisplayAlert("", "Opravdu chcete zmìnít svou tøídu?", "ANO", "NE");
		if(odpovedZmena)
		{
            string volbaKraje = await DisplayActionSheet("KRAJ: ", "Zrušit", null, kraje);
			if(volbaKraje != null && volbaKraje != "Zrušit")
			{
				string[] mozneOkresy = slovnikOkresy[volbaKraje];
                string volbaOkresu = await DisplayActionSheet("OKRESY: ", "Zrušit", null, mozneOkresy);
				if(volbaOkresu != null && volbaOkresu != "Zrušit")
				{
                    var unikatniMestaVOkresu = seznam.Where(x => x.District == volbaKraje && x.Region == volbaOkresu).Select(x => x.City).Distinct().ToList();
					if (unikatniMestaVOkresu != null)
					{
						string volbaMesta = await DisplayActionSheet("MÌSTA: ", "Zrušit", null, unikatniMestaVOkresu.ToArray());
						if (volbaMesta != null && volbaMesta != "Zrušit")
						{
							List<string> seznamTridVMeste = seznam.Where(x => x.District == volbaKraje && x.Region == volbaOkresu && x.City == volbaMesta).Select(x => x.SchoolName + " | " + x.Grade + ". tø").Distinct().ToList();
							if (seznamTridVMeste != null)
							{
								string volbaTridy = await DisplayActionSheet("TØÍDY: ", "Zrušit", null, seznamTridVMeste.ToArray());
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