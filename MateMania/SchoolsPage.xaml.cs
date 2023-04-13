using MateMania.Models;

namespace MateMania;

public partial class SchoolsPage : ContentPage
{
	public SchoolsPage()
	{
		InitializeComponent();
        NactiSkoly();
	}
    private async void NactiSkoly()
    {
        List<string> seznamSkol = new();
        Task<List<ClassModel>> taskTridy = DbData.NajitSkoly();
        var nacteneTridy = await taskTridy;
        if (nacteneTridy == null)
        {
            seznamSkol.Add("��dn� t��dy nebyly p�id�ny");
        }
        else
        {
            foreach (var item in nacteneTridy)
            {
                seznamSkol.Add(item.SchoolName + " | " + item.Grade + ". t�");
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
}