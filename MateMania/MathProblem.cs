using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateMania
{
    public static class MathProblem
    {
        public static Dictionary<string, int> GenerateMany(int grade)
        {
            Random rnd = new Random();
            Dictionary<string, int> slovnikPrikladuVysledku = new Dictionary<string, int>();
            if (grade == 1)
            {
                int num1 = rnd.Next(2, 5);
                int num2 = rnd.Next(1, 5 - num1);

                int vysledekC1 = (num1 *5) + (num2 *5);
                string prikladC1 = (num1 * 5).ToString() + " + " + (num2 *5).ToString();
                slovnikPrikladuVysledku.Add(prikladC1, vysledekC1);

                int osmyPriklad = rnd.Next(0, 2);
                int kladny = 0, zaporny = 0;
                if (osmyPriklad == 0) { kladny += 1; }
                else { zaporny += 1; }

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
            else if (grade == 2)
            {
                int pocetKladnych = rnd.Next(1, 3);
                int pocetOdectu = 4 - pocetKladnych;
                for (int i = 1; i <= pocetKladnych; i++)
                {
                    int cislo1, cislo2, vysledek;
                    do
                    {
                        cislo1 = rnd.Next(1, 50);
                        cislo2 = rnd.Next(1, 50 - cislo1);
                        vysledek = cislo1 + cislo2;
                    }
                    while (slovnikPrikladuVysledku.ContainsKey($"{cislo1}+{cislo2}"));
                    slovnikPrikladuVysledku.Add($"{cislo1} + {cislo2}", vysledek);
                }
                for (int i = 1; i <= pocetOdectu; i++)
                {
                    int cislo1, cislo2, vysledek;
                    do
                    {
                        cislo1 = rnd.Next(1, 50);
                        cislo2 = rnd.Next(1, cislo1);
                        vysledek = cislo1 - cislo2;
                    }
                    while (slovnikPrikladuVysledku.ContainsKey($"{cislo1} - {cislo2}"));
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
                        cislo1 = rnd.Next(cislo2, cislo2 *10 + 1);
                        vysledek = cislo1 / cislo2;
                    } while (cislo1 % cislo2 != 0 || vysledek > 50 || slovnikPrikladuVysledku.ContainsKey($"{cislo1} : {cislo2}"));
                    slovnikPrikladuVysledku.Add($"{cislo1} : {cislo2}", vysledek);
                }
            }
            else if (grade == 3)
            {
                int cisloNasobky5a10 = rnd.Next(1, 20) * 5;
                int cislo2 = rnd.Next(1, 10);
                slovnikPrikladuVysledku.Add($"{cisloNasobky5a10} * {cislo2}", cisloNasobky5a10 * cislo2);

                int kladne = 0;
                int odecet = 0;
                int nasobeni = 0;
                int deleni = 0;

                int zvyseni = 3;/*rnd.Next(0, 4);*/
                if (zvyseni == 0) { kladne = 2; }
                else if (zvyseni == 1) { odecet = 2; }
                else if (zvyseni == 2) { nasobeni = 2; }
                else if (zvyseni == 3) { deleni = 2; }

                for (int i = 0; i < 2 + nasobeni; i++)
                {
                    int cislo = 0;
                    while (true)
                    {
                        cislo = rnd.Next(2, 20);
                        if (!slovnikPrikladuVysledku.ContainsKey($"{cislo} * {cislo}"))
                        {
                            break;
                        }
                    }
                    slovnikPrikladuVysledku.Add($"{cislo} * {cislo}", cislo * cislo);
                }

                int delitel = rnd.Next(1, 21); // dělitel v rozmezí 1-20
                int delenec = rnd.Next(1, delitel * 25); // dělenec v rozmezí 1-25, musí být maximálně 25násobkem dělitele
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
                        cisloA = rnd.Next(2, 10) * 100;
                    }
                    while (cisloA % cisloB != 0 || slovnikPrikladuVysledku.ContainsKey($"{cisloA} : {cisloB}"));
                    slovnikPrikladuVysledku.Add($"{cisloA} : {cisloB}", cisloA / cisloB);
                }
                for (int i = 0; i < 1 + kladne; i++)
                {
                    int cislo1 = rnd.Next(1, 95) * 10;
                    int rozdilRnd = rnd.Next(0, 1000 - cislo1);
                    int vysledek = cislo1 + rozdilRnd;
                    slovnikPrikladuVysledku.Add($"{cislo1} + {rozdilRnd}", vysledek);
                }
                for (int i = 0; i < 1 + odecet; i++)
                {
                    int cislo1, cisloB, nasobky5X10, vysledek;
                    do
                    {
                        cislo1 = rnd.Next(50, 101) * 10;
                        nasobky5X10 = rnd.Next(0, 2);
                        cisloB = 0;
                        if (nasobky5X10 == 0)
                        {
                            cisloB = rnd.Next(1, 11) * 5;
                        }
                        else
                        {
                            cisloB = rnd.Next(1, 11) * 10;
                        }
                        vysledek = cislo1 - cisloB;
                    }
                    while (slovnikPrikladuVysledku.ContainsKey($"{cislo1} - {cisloB}"));
                    slovnikPrikladuVysledku.Add($"{cislo1} - {cisloB}", vysledek);
                }
            }
            return slovnikPrikladuVysledku;
        }
    }
}
