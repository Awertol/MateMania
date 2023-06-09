﻿using MateMania.Models;

namespace MateMania;

public partial class ProfilePage : ContentPage
{
    List<string> seznamTrid = new List<string>();
    Dictionary<string, string> slovnikOtazek = new Dictionary<string, string>()
    {
        {"Kolik je  XVII na druhou?", "289"},
        {"Pořadí π (pí) v řecké abecedě?", "16"},
        {"octo + septem * tres = ","29" },
        {"6!","720" },
        {"Vyřeš: √400 + √100","30" },
        {"Vyřeš následující rovnici za x: 5x – 3 + 220 = 55 * 4 + 2x","1" },
        {"Vyřeš následující rovnici za y: 20 + 10 + 3y = √36 * y","10" },
        {"Vyřeš následující rovnici za z: 200z + (√16*5z)  = 1100","5" },
        {"Doplň vzorec: S = 2ab + 2bc + ","2ac" },
        {"Doplň vzorec: D = b2 - ","4ac" }
    };
    bool pageStart = true;
	public ProfilePage()
	{
		InitializeComponent();
        lbUzivPrezdivka.Text = DbData.nactenyUzivatel.Nickname;
        lbUzivJmeno.Text = DbData.nactenyUzivatel.Firstname != null ? DbData.nactenyUzivatel.Firstname + " " + DbData.nactenyUzivatel.Surname : "---";
        lbUzivZS.Text = DbData.nactenyUzivatel.ChosenClassID != null ? DbData.nactenyUzivatel.ChosenClassID.ToString() : "---";
        chbUcitel.IsChecked = DbData.nactenyUzivatel.IsTeacher != 0 ? true : false;
        if(DbData.nactenyUzivatel.IsTeacher == 1) { btnRezimUcitele.IsVisible = true; }
        lbUzivLevel.Text = "Level " + ExpCounter.Level.ToString();
        int min = 0, max = 0;
        if (ExpCounter.Level == 1)
        {
            min = 0;
            max = ExpCounter.LevelCaps[ExpCounter.Level - 1];
        }
        else if (ExpCounter.Level >= 1 && ExpCounter.Level <= 6)
        {
            min = ExpCounter.LevelCaps[ExpCounter.Level - 2];
            max = ExpCounter.LevelCaps[ExpCounter.Level - 1];
        }
        else if(ExpCounter.Level == 6)
        {
            pbUzivExp.Progress = 1.0;
        }
        int cap = max - min; 
        double progress = ((double)ExpCounter.Experience - min) / cap;
        pbUzivExp.Progress = progress;
        imgPostavaProfil.Source = $"plusak_{DbData.nactenyUzivatel.Avatar}.png";
        NactiSkolu();
    }
    Random rnd = new Random();
    private async void chbUcitel_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (pageStart)
        {
            pageStart = false;
        }
        else
        {
            if (chbUcitel.IsChecked)
            {
                string nahodnaOtazka = slovnikOtazek.ElementAt(rnd.Next(0, slovnikOtazek.Count)).Key;
                string result = await DisplayPromptAsync("Otázka", nahodnaOtazka);
                if (result.Trim() == slovnikOtazek[nahodnaOtazka])
                {
                    await DisplayAlert("Odpověď", "Správně! Můžeš vstoupit do režimu učitele", "OK");
                    DbData.ZmenitStavUc(true);
                    btnRezimUcitele.IsVisible = true;
                }
                else
                {
                    await DisplayAlert("Odpověď", "Špatně!", "OK");
                    chbUcitel.IsChecked = false;
                }
            }
            else if (!chbUcitel.IsChecked)
            {
                DbData.ZmenitStavUc(false);
                btnRezimUcitele.IsVisible = false;
            }
        }
    }
    private async void NactiSkolu()
    {
        Task<ClassModel> taskSkola = DbData.NajitUzivatelovuSkolu();
        var skola =await taskSkola;
        if (skola == null)
        { lbUzivZS.Text = "Nevybráno"; }
        else
        {
            lbUzivZS.Text = skola.SchoolName + " " + skola.Grade + ". třída";
        }
    }

    private void btnZmenaUdaju_Clicked(object sender, EventArgs e)
    {
        ChangeInfoPage zmenaUdajuStranka = new ChangeInfoPage();
        Navigation.PushAsync(zmenaUdajuStranka);
    }

    private void imgPostavaProfil_Clicked(object sender, EventArgs e)
    {
        AvatarPage postavyStranka = new AvatarPage();
        Navigation.PushAsync(postavyStranka);
    }

    private void btnRezimUcitele_Clicked(object sender, EventArgs e)
    {
        TeacherMenuPage menuUcitele = new();
        Navigation.PushAsync(menuUcitele);
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void btnOdhlasit_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
        Navigation.PopAsync();
        DbData.nactenyUzivatel = null;
        OfflineOnline.stavPripojeni = false;
    }
    
}