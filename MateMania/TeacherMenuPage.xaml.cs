namespace MateMania;

public partial class TeacherMenuPage : ContentPage
{
	public TeacherMenuPage()
	{
		InitializeComponent();
	}

    private async void btnVytvoritZadani_Clicked(object sender, EventArgs e)
    {
        string[] poleCisel = new string[8];
        for (int i = 1; i <= 8; i++)
        {
            poleCisel[i-1] = i.ToString();
        }
        string? vybranyPocetPrikladu = await DisplayActionSheet("Po�et p��klad�", "Zru�it", null, poleCisel);
        if(vybranyPocetPrikladu != "Zru�it")
        {
            CreateExamPage tvorbaPrikladuStranka = new(Convert.ToInt32(vybranyPocetPrikladu));
            Navigation.PushAsync(tvorbaPrikladuStranka);
        }
    }

    private async void btnVygenerovatZadani_Clicked(object sender, EventArgs e)
    {
        string urcenaTrida = await DisplayActionSheet("T��DA P��KLAD�: ", "Zru�it", null, "1", "2", "3");
        if(urcenaTrida != null && urcenaTrida != "Zru�it")
        {
            GeneratedTestPage generovanyTestStranka = new(Convert.ToInt32(urcenaTrida));
            Navigation.PushAsync(generovanyTestStranka);
        }
    }

    private void btnVysledkyZaku_Clicked(object sender, EventArgs e)
    {
        ExamsInClassPage zadaniTridyStranka = new();
        Navigation.PushAsync(zadaniTridyStranka);
    }

    private void btnPrehledyTridy_Clicked(object sender, EventArgs e)
    {
        SchoolsPage skolyStranka = new SchoolsPage();
        Navigation.PushAsync(skolyStranka);
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
        Navigation.PopAsync();
    }
}