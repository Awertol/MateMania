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
                if (docasnySeznam[pocitadlo].Contains('+'))
                {
                    int indexPlus = priklad.IndexOf('+');
                    int c1 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(0, indexPlus));
                    int c2 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(indexPlus, docasnySeznam[pocitadlo].Length - indexPlus));
                    slovnikPrikladuVysledku.Add(priklad, c1 + c2);
                    pocitadlo++;
                }
                else if (docasnySeznam[pocitadlo].Contains('-'))
                {
                    int c1 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(0, priklad.IndexOf('-')));
                    int c2 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(priklad.IndexOf('-'), docasnySeznam[pocitadlo].Length));
                    slovnikPrikladuVysledku.Add(priklad, c1 - c2);
                }
                else if (docasnySeznam[pocitadlo].Contains('*'))
                {
                    int c1 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(0, priklad.IndexOf('*')));
                    int c2 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(priklad.IndexOf('*'), docasnySeznam[pocitadlo].Length));
                    slovnikPrikladuVysledku.Add(priklad, c1 * c2);
                }
                else if (docasnySeznam[pocitadlo].Contains(':'))
                {
                    int c1 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(0, priklad.IndexOf(':')));
                    int c2 = Convert.ToInt32(docasnySeznam[pocitadlo].Substring(priklad.IndexOf(':'), docasnySeznam[pocitadlo].Length));
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
            priklad.FontSize = 36;
            priklad.HorizontalOptions = LayoutOptions.CenterAndExpand;
            priklad.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label rovnaSe = new Label();
            rovnaSe.Text = "=";
            rovnaSe.Margin = new Thickness(0, 0, 5, 0);
            rovnaSe.FontSize = 36;
            rovnaSe.HorizontalOptions = LayoutOptions.CenterAndExpand;
            rovnaSe.VerticalOptions = LayoutOptions.CenterAndExpand;

            Entry vysledek = new Entry();
            vysledek.Placeholder = "0";
            vysledek.FontSize = 32;
            vysledek.WidthRequest = 100;
            vysledek.TextColor = Colors.White;
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
                DisplayAlert("Výsledek zapsán", $"Tvůj výsledek byl {vysledekZadani.MaxPossible}/{vysledekZadani.Result}", "OK");
                Navigation.PopAsync();
            }
        }
        zkontrolovano = true;
    }
}