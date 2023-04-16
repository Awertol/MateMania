using MateMania.Models;
using System.Text.RegularExpressions;

namespace MateMania;

public partial class CreateExamPage : ContentPage
{
    int pocetRadku = 0;
    string nazevZadani = "";
	public CreateExamPage(int pocetRadku)
	{
		InitializeComponent();
        this.pocetRadku = pocetRadku;
        string[] operace = {"+", "-", "*",":"};
        for (int i = 0; i < pocetRadku; i++)
        {
            Entry zadavane1 = new Entry();
            if (Device.RuntimePlatform == Device.Android)
            {
                zadavane1.FontSize = 26;
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                zadavane1.FontSize = 34;
            }
            zadavane1.BackgroundColor = Colors.White;
            zadavane1.TextColor = Colors.Black;
            zadavane1.WidthRequest = 110;
            zadavane1.HorizontalOptions = LayoutOptions.CenterAndExpand;
            zadavane1.VerticalOptions = LayoutOptions.Start;
            zadavane1.ClassId = "1";
            zadavane1.Keyboard = Keyboard.Numeric;

            Picker znamenko = new Picker();
            znamenko.FontSize = 36;
            znamenko.ItemsSource = operace;
            znamenko.SelectedIndex = 0;
            znamenko.HorizontalOptions = LayoutOptions.CenterAndExpand;
            znamenko.VerticalOptions = LayoutOptions.Start;

            Entry zadavane2 = new Entry();
            if (Device.RuntimePlatform == Device.Android)
            {
                zadavane2.FontSize = 26;
            }
            else if (Device.RuntimePlatform == Device.UWP)
            {
                zadavane2.FontSize = 34;
            }
            zadavane2.BackgroundColor = Colors.White;
            zadavane2.TextColor = Colors.Black;
            zadavane2.WidthRequest = 110;
            zadavane2.ClassId = "2";
            zadavane2.HorizontalOptions = LayoutOptions.CenterAndExpand;
            zadavane2.VerticalOptions = LayoutOptions.Start;
            zadavane2.Keyboard = Keyboard.Numeric;

            grdTvorenePriklady.Add(zadavane1);
            grdTvorenePriklady.Add(znamenko);
            grdTvorenePriklady.Add(zadavane2);
            Grid.SetColumn(zadavane1, 0);
            Grid.SetColumn(znamenko, 1);
            Grid.SetColumn(zadavane2, 2);
            Grid.SetRow(zadavane1, i);
            Grid.SetRow(znamenko, i);
            Grid.SetRow(zadavane2, i);
        }
    }

    private void btnHotovo_Clicked(object sender, EventArgs e)
    {
        bool error = false;
        foreach(var item in grdTvorenePriklady.Children)
        {
            if(item is Entry)
            {
                Entry entry = (Entry)item;
                string txt = entry.Text == null ? "" : entry.Text;
                bool isNumeric = Regex.IsMatch(txt, @"^\d+$");
                if(isNumeric)
                {
                    continue;
                }
                else
                {
                    DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                    error = true;
                    break;
                }
            }
        }
        if(!error)
        {
            Random rnd = new Random();
            string vygPin = "";
            for (int i = 0; i < 5; i++)
            {
                vygPin += Convert.ToString(rnd.Next(1, 10));
            }
            ExamsModel vytvorenePriklady = new();
            vytvorenePriklady.ClassID = (int)DbData.nactenyUzivatel.ChosenClassID;
            vytvorenePriklady.PIN = vygPin;
            vytvorenePriklady.TeacherID = DbData.nactenyUzivatel.Id;
            vytvorenePriklady.ExamName = (nazevZadani == "" || nazevZadani == "Zrušit / Vymazat název") ? null : nazevZadani;
            List<string> zadanePriklady = LoadProblemsFromGrid();
            bool chybaVZadani = false;
            foreach(string priklad in zadanePriklady)
            {
                char operace;
                string[] rozdelenyPriklad = new string[3];
                if(priklad.Contains('+'))
                {
                    rozdelenyPriklad = priklad.Split('+');
                    if (Convert.ToInt32(rozdelenyPriklad[0]) < 1 || Convert.ToInt32(rozdelenyPriklad[1]) < 1)
                    {
                        DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                        chybaVZadani = true;
                        break;
                    }
                }
                else if(priklad.Contains('-'))
                {
                    rozdelenyPriklad = priklad.Split('-');
                    if(rozdelenyPriklad.Length > 2)
                    {
                        DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                        chybaVZadani = true;
                        break;
                    }
                    if (Convert.ToInt32(rozdelenyPriklad[0]) < 1 || Convert.ToInt32(rozdelenyPriklad[1]) < 1)
                    {
                        DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                        chybaVZadani = true;
                        break;
                    }
                }
                else if(priklad.Contains('*'))
                {
                    rozdelenyPriklad = priklad.Split("*");
                    if (Convert.ToInt32(rozdelenyPriklad[0]) < 1 || Convert.ToInt32(rozdelenyPriklad[1]) < 1)
                    {
                        DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                        chybaVZadani = true;
                        break;
                    }
                }
                else if (priklad.Contains(':'))
                {
                    rozdelenyPriklad = priklad.Split(":");
                    if (Convert.ToInt32(rozdelenyPriklad[0]) < 1 || Convert.ToInt32(rozdelenyPriklad[1]) < 1)
                    {
                        DisplayAlert("Špatnì zadaná hodnota", "Zkontrolujte prosím znovu", "OK");
                        chybaVZadani = true;
                        break;
                    }
                }
                if (chybaVZadani == false)
                {
                    chybaVZadani = Regex.IsMatch(priklad, @"^\d+$");
                }
            }
            if (chybaVZadani == false)
            {
                switch (pocetRadku)
                {
                    case 1:
                        vytvorenePriklady.Problem1 = zadanePriklady[0] != null ? zadanePriklady[0] : "1+1";
                        break;
                    case 2:
                        vytvorenePriklady.Problem2 = zadanePriklady[1];
                        goto case 1;
                    case 3:
                        vytvorenePriklady.Problem3 = zadanePriklady[2];
                        goto case 2;
                    case 4:
                        vytvorenePriklady.Problem4 = zadanePriklady[3];
                        goto case 3;
                    case 5:
                        vytvorenePriklady.Problem5 = zadanePriklady[4];
                        goto case 4;
                    case 6:
                        vytvorenePriklady.Problem6 = zadanePriklady[5];
                        goto case 5;
                    case 7:
                        vytvorenePriklady.Problem7 = zadanePriklady[6];
                        goto case 6;
                    case 8:
                        vytvorenePriklady.Problem8 = zadanePriklady[7];
                        goto case 7;
                }
                DbData.VytvoritZadani(vytvorenePriklady);
                string pin = "";
                for (int i = 0; i < vygPin.Length; i++)
                {
                    pin += OnlinePinPage.pins[Convert.ToInt32(vygPin[i].ToString()) - 1];
                }
                DisplayAlert("Vytvoøeno nové zadání", $"PIN: {pin}\nZadání jsou po 14 dnech mazána", "OK");
                Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Zkontrolujte pøíklady", "Aplikace neumožòuje zadání desetinných, záporných èísel a speciálních znakù", "OK");
            }
        }
    }
    private List<string> LoadProblemsFromGrid()
    {
        List<string> seznam = new();
        bool radekGridu = false;
        string priklad = "";
        foreach (var item in grdTvorenePriklady.Children)
        {
            if(item is Entry)
            {
                Entry entryGrid = item as Entry;
                if(entryGrid.ClassId == "1")
                {
                    radekGridu = true;
                    priklad += entryGrid.Text;
                }
                else if(entryGrid.ClassId == "2")
                {
                    radekGridu = false;
                    priklad += entryGrid.Text;
                }
                if(radekGridu == false)
                {
                    seznam.Add(priklad);
                    priklad = "";
                }
            }
            else if(item is Picker)
            {
                Picker pickerGrid = item as Picker;
                if(pickerGrid.SelectedItem != null)
                {
                    priklad += pickerGrid.SelectedItem;
                }
            }
        }
        return seznam;
    }

    private async void btnPridatJmenoZadani_Clicked(object sender, EventArgs e)
    {
        string zadanyNazev = await DisplayPromptAsync("Zadání názvu", "Název", "OK", "Zrušit / Vymazat název");
        if(zadanyNazev != "" || zadanyNazev != "Cancel")
        {
            nazevZadani = zadanyNazev;
        }
    }
}