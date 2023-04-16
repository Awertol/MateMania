using MateMania.Models;

namespace MateMania;

public partial class SchoolsPage : ContentPage
{
    List<ClassModel> nacteneTridy;
    public SchoolsPage()
	{
		InitializeComponent();
        NactiSkoly();
	}
    private async void NactiSkoly()
    {
        List<string> seznamSkol = new();
        Task<List<ClassModel>> taskTridy = DbData.NajitSkoly();
        nacteneTridy = await taskTridy;
        if (nacteneTridy == null)
        {
            seznamSkol.Add("Žádné tøídy nebyly pøidány");
        }
        else
        {
            foreach (var item in nacteneTridy)
            {
                seznamSkol.Add(item.SchoolName + " | " + item.Grade + ". tø");
            }
        }
        lvSeznamSkol.ItemsSource = seznamSkol;
    }

    private void btnVytvoritTridu_Clicked(object sender, EventArgs e)
    {
        CreateClassPage vytvoreniTridyStranka = new();
        Navigation.PushAsync(vytvoreniTridyStranka);
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
    string[] obsahKliknuteTridy;
    private void lvSeznamSkol_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        btnSmazatTridu.IsVisible = false;
        obsahKliknuteTridy = lvSeznamSkol.SelectedItem.ToString().Split(" | ");
        string nazevSkoly = obsahKliknuteTridy[0].Trim();
        string[] rocnikTx = obsahKliknuteTridy[1].Split('.');
        int rocnik = Convert.ToInt32(rocnikTx[0]);
        foreach (var trida in nacteneTridy.Where(x => x.SchoolName == nazevSkoly && x.Grade == rocnik && x.TeacherId == DbData.nactenyUzivatel.Id))
        {
            if (trida != null)
            {
                btnSmazatTridu.IsVisible = true;
                break;
            }
            else
            {
                btnSmazatTridu.IsVisible = false;
            }
        }
    }

    private void btnSmazatTridu_Clicked(object sender, EventArgs e)
    {
        try
        {
            DbData.VymazatTridu(obsahKliknuteTridy[0], Convert.ToInt32(obsahKliknuteTridy[1][0].ToString()));
            DisplayAlert("Tøída smazána", $"Tøída '{obsahKliknuteTridy[0]}' byla úspìšnì smazána", "OK");
            Navigation.PopAsync();
        }
        catch(Exception ex)
        {
            DisplayAlert("Nastala chyba", "Zkuste to znovu", "OK");
        }
        
    }
}