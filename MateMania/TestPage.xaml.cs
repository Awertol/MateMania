using System;

namespace MateMania;

public partial class TestPage : ContentPage
{
    Dictionary<string, int> slovnikPrikladuVysledku;
    public TestPage(int rocnik)
    {
        InitializeComponent();
        slovnikPrikladuVysledku = GenerujPriklady(rocnik);
        int radek = 0;
        int pocitadlo = 0;
        foreach (var item in slovnikPrikladuVysledku)
        {
            Label priklad = new Label();
            priklad.Text = item.Key;
            priklad.FontSize = 36;
            priklad.HorizontalOptions = LayoutOptions.CenterAndExpand;
            priklad.VerticalOptions = LayoutOptions.CenterAndExpand;

            Label rovnaSe = new Label();
            rovnaSe.Text = "=";
            rovnaSe.FontSize = 36;
            rovnaSe.HorizontalOptions = LayoutOptions.CenterAndExpand;
            rovnaSe.VerticalOptions = LayoutOptions.CenterAndExpand;

            Entry vysledek = new Entry();
            vysledek.Placeholder = "0";
            vysledek.FontSize = 32;
            vysledek.WidthRequest = 100;
            vysledek.HorizontalOptions = LayoutOptions.CenterAndExpand;
            vysledek.VerticalOptions = LayoutOptions.CenterAndExpand;
            vysledek.Keyboard=Keyboard.Numeric;

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

    private Dictionary<string, int> GenerujPriklady(int rocnik)
    {
        Dictionary<string, int> slovnikPrikladuVysledku = new Dictionary<string, int>();
        Random rnd = new Random();
        if (rocnik == 1)
        {
            //vytvoření příkladu, který bude mít násobky 5 - 20
            int num1 = rnd.Next(6, 20); //19
            int num2 = rnd.Next(1, 21 - num1);

            int vysledekC1 = num1 + num2;
            string prikladC1 = num1.ToString() + " + " + num2.ToString();
            slovnikPrikladuVysledku.Add(prikladC1, vysledekC1);

            int osmyPriklad = rnd.Next(0, 2);
            int kladny = 0;
            int zaporny = 0;
            if (osmyPriklad == 0)
            {
                kladny += 1;
            }
            else
            {
                zaporny += 1;
            }

            for (int i = 0; i < 3 + kladny; i++)
            {
                int result = 0;
                do
                {
                    num1 = rnd.Next(1, 11);
                    num2 = rnd.Next(1, 11);
                    result = num1 + num2;
                } while (result > 20 || slovnikPrikladuVysledku.Contains(new KeyValuePair<string, int>($"{num1} + {num2}", result)));

                slovnikPrikladuVysledku.Add($"{num1} + {num2}", result);
            }
            for (int i = 0; i < 3 + zaporny; i++)
            {
                int result = 0;
                do
                {
                    num1 = rnd.Next(1, 21);
                    num2 = rnd.Next(1, num1);
                    result = num1 - num2;
                } while (result > 20 || slovnikPrikladuVysledku.Contains(new KeyValuePair<string, int>($"{num1} - {num2}", result)));

                slovnikPrikladuVysledku.Add($"{num1} - {num2}", result);
            }
        }
        else if (rocnik == 2)
        {
            int pocetKladnych = rnd.Next(1, 3);
            int pocetOdectu = 4 - pocetKladnych;
            for (int i = 1; i <= pocetKladnych; i++)
            {
                int cislo1 = rnd.Next(1, 50);
                int cislo2 = rnd.Next(1, 50 - cislo1);
                int vysledek = cislo1 + cislo2;
                slovnikPrikladuVysledku.Add($"{cislo1} + {cislo2}", vysledek);
            }
            for (int i = 1; i <= pocetOdectu; i++)
            {
                int cislo1 = rnd.Next(1, 50);
                int cislo2 = rnd.Next(1, cislo1);
                int vysledek = cislo1 - cislo2;
                slovnikPrikladuVysledku.Add($"{cislo1} - {cislo2}", vysledek);
            }
            for (int i = 1; i <= 2; i++)
            {
                int cislo1 = rnd.Next(2, 6);
                int cislo2 = rnd.Next(2, (cislo1 * 4) + 1);
                int vysledek = cislo1 * cislo2;
                slovnikPrikladuVysledku.Add($"{cislo1} * {cislo2}", vysledek);
            }
            for (int i = 1; i <= 2; i++)
            {
                int vysledek = 0;
                int cislo1;
                int cislo2 = rnd.Next(2, 6);
                do
                {
                    cislo1 = rnd.Next(2, 51);
                    vysledek = cislo1 / cislo2;
                } while (cislo1 % cislo2 != 0 || vysledek > 50);
                slovnikPrikladuVysledku.Add($"{cislo1} : {cislo2}", vysledek);
            }
        }
        else if (rocnik == 3)
        {
            int cisloNasobky5a10 = rnd.Next(1, 20) * 5;
            int cislo2 = rnd.Next(1, 10);
            slovnikPrikladuVysledku.Add($"{cisloNasobky5a10} * {cislo2}", cisloNasobky5a10 * cislo2);

            int kladne = 0;
            int odecet = 0;
            int nasobeni = 0;
            int deleni = 0;

            int zvyseni = rnd.Next(0, 4);
            if (zvyseni == 0) { kladne = 2; }
            else if (zvyseni == 1) { odecet = 2; }
            else if (zvyseni == 2) { nasobeni = 2; }
            else if (zvyseni == 3) { deleni = 2; }

            for (int i = 0; i < 2 + nasobeni; i++)
            {
                int cislo = 0;
                while(true)
                {
                    cislo = rnd.Next(2, 20);
                    if(!slovnikPrikladuVysledku.ContainsKey($"{cislo} * {cislo}"))
                    {
                        break;
                    }
                }
                slovnikPrikladuVysledku.Add($"{cislo} * {cislo}", cislo * cislo);      
            }

            int delitel = rnd.Next(1, 21); // dělitel v rozmezí 1-20
            int delenec = rnd.Next(1, delitel*25); // dělenec v rozmezí 1-25, musí být maximálně 25násobkem dělitele
            int mezivypocet = delitel * delenec; // výpočet dělení
            while (mezivypocet > 1000 || (delenec % delitel) != 0) // opakování, dokud nevyhovuje pravidlům
            {
                delitel = rnd.Next(1, 21);
                delenec = rnd.Next(1, 26);
                mezivypocet = delitel * delenec;
            }
            slovnikPrikladuVysledku.Add($"{delenec} : {delitel}", delenec / delitel);

            for (int i = 0; i < 0 + deleni; i++)
            {
                int cisloA, cisloB;
                do
                {
                    cisloB = rnd.Next(2, 11);
                    cisloA = rnd.Next(2, cisloB * 10 + 1);
                }
                while (cisloB % cisloA != 0);
                slovnikPrikladuVysledku.Add($"{cisloA} : {cisloB}", cisloB / cisloA);
            }
            for(int i = 0; i < 1 + kladne; i++)
            {
                int cislo1 = rnd.Next(1, 95) * 10; 
                int rozdilRnd = rnd.Next(0, 1000 - cislo1); 
                int vysledek = cislo1 + rozdilRnd;
                slovnikPrikladuVysledku.Add($"{cislo1} + {rozdilRnd}", vysledek);
            }
            for (int i = 0; i < 1 + odecet; i++)
            {
                int cislo1 = rnd.Next(50, 101) * 10;
                int nasobky5X10 = rnd.Next(0, 2);
                int cisloB = 0;
                if(nasobky5X10 == 0)
                {
                    cisloB = rnd.Next(1,11) * 5;
                }
                else
                {
                    cisloB = rnd.Next(1,11) * 10;
                }
                int vysledek = cislo1 - cisloB;
                slovnikPrikladuVysledku.Add($"{cislo1} - {cisloB}", vysledek);
            }
        }
        return slovnikPrikladuVysledku;
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
            ResultPage strankaVysledek = new ResultPage(pocetSpravnych);
            Navigation.PushAsync(strankaVysledek);
        }
        zkontrolovano = true;
    }
}