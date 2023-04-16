using MateMania.Models;
using System;

namespace MateMania;

public partial class TestPage : ContentPage
{
    Dictionary<string, int> slovnikPrikladuVysledku = new();
    ExamsModel onlineZadani;
    string onlinePin = "";
    public TestPage(string pin)
    {
        InitializeComponent();
        OnlinePriklady(pin);
        onlinePin = pin;
    }
    public TestPage(int rocnik)
    {
        InitializeComponent();
        slovnikPrikladuVysledku = MathProblem.GenerateMany(rocnik);
        ZaplnGrid();
    }
    private async void OnlinePriklady(string pin)
    {
        Task<ExamsModel> prikladyTask = DbData.NajitOtazky(pin);
        var priklady = await prikladyTask;
        if (priklady == null)
        {
            DisplayAlert("Špatný PIN", "Pin nenalezen nebo nepatří do tvé třídy", "OK");
            Navigation.PopAsync();
        }
        else
        {
            List<string> docasnySeznam = new();
            docasnySeznam.Add(priklady.Problem1);
            if (priklady.Problem2 != null) { docasnySeznam.Add(priklady.Problem2); }
            if (priklady.Problem3 != null) { docasnySeznam.Add(priklady.Problem3); }
            if (priklady.Problem4 != null) { docasnySeznam.Add(priklady.Problem4); }
            if (priklady.Problem5 != null) { docasnySeznam.Add(priklady.Problem5); }
            if (priklady.Problem6 != null) { docasnySeznam.Add(priklady.Problem6); }
            if (priklady.Problem7 != null) { docasnySeznam.Add(priklady.Problem7); }
            if (priklady.Problem8 != null) { docasnySeznam.Add(priklady.Problem8); }
            byte pocitadlo = 0;
            foreach (string priklad in docasnySeznam)
            {
                string aktPriklad = priklad;
                if (docasnySeznam[pocitadlo].Contains('+'))
                {
                    int indexPlus = aktPriklad.IndexOf('+');
                    int c1 = Convert.ToInt32(aktPriklad.Substring(0, indexPlus));
                    int c2 = Convert.ToInt32(aktPriklad.Substring(indexPlus+1, (docasnySeznam[pocitadlo].Length - indexPlus)-1));
                    slovnikPrikladuVysledku.Add(priklad, c1 + c2);
                    pocitadlo++;
                }
                else if (docasnySeznam[pocitadlo].Contains('-'))
                {
                    int indexMinus = aktPriklad.IndexOf('-');
                    int c1 = Convert.ToInt32(aktPriklad.Substring(0, indexMinus));
                    int c2 = Convert.ToInt32(aktPriklad.Substring(indexMinus+1, (docasnySeznam[pocitadlo].Length - indexMinus) - 1));
                    slovnikPrikladuVysledku.Add(priklad, c1 - c2);
                }
                else if (docasnySeznam[pocitadlo].Contains('*'))
                {
                    int indexKrat = aktPriklad.IndexOf('*');
                    int c1 = Convert.ToInt32(aktPriklad.Substring(0, indexKrat));
                    int c2 = Convert.ToInt32(aktPriklad.Substring(indexKrat+1, (docasnySeznam[pocitadlo].Length - indexKrat)-1));
                    slovnikPrikladuVysledku.Add(priklad, c1 * c2);
                }
                else if (docasnySeznam[pocitadlo].Contains(':'))
                {
                    int indexDeleno = aktPriklad.IndexOf(':');
                    int c1 = Convert.ToInt32(aktPriklad.Substring(0, indexDeleno));
                    int c2 = Convert.ToInt32(aktPriklad.Substring(indexDeleno+1, (docasnySeznam[pocitadlo].Length - indexDeleno) - 1));
                    slovnikPrikladuVysledku.Add(priklad, c1 / c2);
                }
            }
            ZaplnGrid();
            onlineZadani = priklady;
        }
    }
    private void ZaplnGrid()
    {
        int radek = 0;
        foreach (var item in slovnikPrikladuVysledku)
        {
            Label priklad = new Label();
            priklad.Text = item.Key;
            priklad.FontSize = 32;
            priklad.HorizontalOptions = LayoutOptions.CenterAndExpand;
            priklad.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label rovnaSe = new Label();
            rovnaSe.Text = "=";
            rovnaSe.Margin = new Thickness(0, 0, 5, 0);
            if (Device.RuntimePlatform == Device.Android)
            {
                rovnaSe.FontSize = 24;
            }
            else
            {
                rovnaSe.FontSize = 36;
            }
            rovnaSe.HorizontalOptions = LayoutOptions.CenterAndExpand;
            rovnaSe.VerticalOptions = LayoutOptions.CenterAndExpand;

            Entry vysledek = new Entry();
            vysledek.Placeholder = "0";
            if(Device.RuntimePlatform == Device.Android)
            {
                vysledek.FontSize= 24;
            }
            else
            {
                vysledek.FontSize = 36;
            }
            vysledek.WidthRequest = 100;
            vysledek.BackgroundColor = Colors.White;
            vysledek.TextColor = Colors.Black;
            vysledek.HorizontalOptions = LayoutOptions.CenterAndExpand;
            vysledek.VerticalOptions = LayoutOptions.CenterAndExpand;
            vysledek.Keyboard = Keyboard.Numeric;

            grdPriklady.Add(priklad);
            grdPriklady.Add(rovnaSe);
            grdPriklady.Add(vysledek);
            Grid.SetColumn(priklad, 0);
            Grid.SetColumn(rovnaSe, 1);
            Grid.SetColumn(vysledek, 2);
            Grid.SetRow(priklad, radek);
            Grid.SetRow(rovnaSe, radek);
            Grid.SetRow(vysledek, radek);
            radek++;
        }
    }
    
    bool zkontrolovano = false;
    int pocetSpravnych = 0;

    private void btnHotovo_Clicked(object sender, EventArgs e)
    {
        if (!zkontrolovano)
        {
            pocetSpravnych = 0;
            string radekPriklad = "";
            foreach (var item in grdPriklady.Children)
            {
                if (item is Label)
                {
                    Label label = (Label)item;
                    if (label.Text == "=")
                    {

                    }
                    else
                    {
                        radekPriklad = label.Text;
                    }
                }
                else if (item is Entry)
                {
                    Entry entry = (Entry)item;
                    entry.IsEnabled = false;
                    if (entry.Text == slovnikPrikladuVysledku[radekPriklad].ToString())
                    {
                        entry.Text += "✅";
                        pocetSpravnych++;
                    }
                    else
                    {
                        entry.Text += "❌";
                    }
                }
            }
            btnHotovo.Text = "HOTOVO ➡️";
        }
        else
        {
            if (onlineZadani == null)
            {
                ResultPage strankaVysledek = new ResultPage(pocetSpravnych);
                Navigation.PushAsync(strankaVysledek);
            }
            else
            {
                ExamAnswersModel vysledekZadani = new();
                vysledekZadani.MaxPossible = slovnikPrikladuVysledku.Count;
                vysledekZadani.UserID = DbData.nactenyUzivatel.Id;
                vysledekZadani.Result = pocetSpravnych;
                vysledekZadani.ExamID = onlineZadani.Id;
                DbData.PridatVysledek(vysledekZadani);
                DisplayAlert("Výsledek zapsán", $"Tvůj výsledek byl {vysledekZadani.Result}/{vysledekZadani.MaxPossible}", "OK");
                Navigation.PopAsync();
            }
        }
        zkontrolovano = true;
    }
}