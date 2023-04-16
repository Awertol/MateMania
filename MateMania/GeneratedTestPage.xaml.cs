using MateMania.Models;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace MateMania;

public partial class GeneratedTestPage : ContentPage
{
	Dictionary<string, int> slovnikPrikladuVysledku = new Dictionary<string, int>();
    int tridaPrikladu;
    int pocetPrikladu;
	public GeneratedTestPage(int urcenaTrida)
	{
		InitializeComponent();
		slovnikPrikladuVysledku = MathProblem.GenerateMany(urcenaTrida);
        tridaPrikladu = urcenaTrida;
        ZaplnGrid();
	}

    private void btnGenerujZnova_Clicked(object sender, EventArgs e)
    {
        grdPriklady.Children.Clear();
        slovnikPrikladuVysledku.Clear();
        slovnikPrikladuVysledku = MathProblem.GenerateMany(tridaPrikladu);
        ZaplnGrid();
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    private void ZaplnGrid()
    {
        int radek = 0;
        foreach (var item in slovnikPrikladuVysledku)
        {
            string znak = "";
            foreach(var item2 in item.Key.Trim())
            {
                string znak2 = item2.ToString();
                bool isOnlyDigits = Regex.IsMatch(znak, @"^\d+$");
                if(isOnlyDigits)
                {
                    znak += znak2;
                }
            }
            Label cislo1 = new Label();
            cislo1.Text = item.Key.Substring(0,item.Key.IndexOf(znak));
            cislo1.FontSize = 32;
            cislo1.WidthRequest = 100;
            cislo1.HorizontalOptions = LayoutOptions.CenterAndExpand;
            cislo1.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label znamenko = new Label();
            znamenko.Text = znak;
            znamenko.FontSize = 36;
            znamenko.HorizontalOptions = LayoutOptions.CenterAndExpand;
            znamenko.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label cislo2 = new Label();
            cislo2.Text = item.Key.Substring(item.Key.IndexOf(znak)+2, item.Key.Length-2 - item.Key.IndexOf(znak));           
            cislo2.FontSize = 32;
            cislo2.WidthRequest = 100;
            cislo2.HorizontalOptions = LayoutOptions.CenterAndExpand;
            cislo2.VerticalOptions = LayoutOptions.CenterAndExpand;

            grdPriklady.Add(cislo1);
            grdPriklady.Add(znamenko);
            grdPriklady.Add(cislo2);
            Grid.SetColumn(cislo1, 0);
            Grid.SetColumn(znamenko, 1);
            Grid.SetColumn(cislo2, 2);
            Grid.SetRow(cislo1, radek);
            Grid.SetRow(znamenko, radek);
            Grid.SetRow(cislo2, radek);
            radek++;
        }
        pocetPrikladu = radek;
    }

    private void btnVytvorit_Clicked(object sender, EventArgs e)
    {
        ExamsModel vytvoreneZadani = new();
        string vygPin = "";
        vytvoreneZadani.ClassID = (int)DbData.nactenyUzivatel.ChosenClassID;
        vytvoreneZadani.TeacherID = DbData.nactenyUzivatel.Id;
        Random rnd = new Random();
        for (int i = 0; i < 5; i++)
        {
            vygPin += Convert.ToString(rnd.Next(1, 10));
        }
        vytvoreneZadani.PIN = vygPin;
        int pocitadlo = 1;
        foreach (var item in slovnikPrikladuVysledku)
        {
            switch(pocitadlo)
            {
                case 1:
                    vytvoreneZadani.Problem1 = item.Key; break;
                case 2:
                    vytvoreneZadani.Problem2 = item.Key; break;
                case 3:
                    vytvoreneZadani.Problem3 = item.Key; break;
                case 4:
                    vytvoreneZadani.Problem4 = item.Key; break;
                case 5:
                    vytvoreneZadani.Problem5 = item.Key; break;
                case 6:
                    vytvoreneZadani.Problem6 = item.Key; break;
                case 7:
                    vytvoreneZadani.Problem7 = item.Key; break;
                case 8:
                    vytvoreneZadani.Problem8 = item.Key; break;
            }
            pocitadlo++;
        }
        DbData.VytvoritZadani(vytvoreneZadani);
        string pin = "";
        for (int i = 0; i < vygPin.Length; i++)
        {
            pin += OnlinePinPage.pins[Convert.ToInt32(vygPin[i].ToString())-1];
        }
        DisplayAlert("Zadání vytvoøeno", $"PIN: {pin}", "OK");
        Navigation.PopAsync();
    }
}