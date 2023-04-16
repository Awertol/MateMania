using MateMania.Models;

namespace MateMania;

public partial class ExamResultPage : ContentPage
{
	public ExamResultPage(List<ExamAnswersModel> nacteneVysledky)
	{
		InitializeComponent();
        NactiVysledkyZaku(nacteneVysledky);
	}
	private async void NactiVysledkyZaku(List<ExamAnswersModel> nacteneVysledky)
	{
        Task<List<UserModel>> taskLideTridy = DbData.NajitClenyTridy((int)DbData.nactenyUzivatel.ChosenClassID);
        List<UserModel> lideTridy = await taskLideTridy;
        List<string> vysledkyZaku = new();
        if (lideTridy != null)
        {
            foreach (var vysledek in nacteneVysledky)
            {
                var uzivatelVysledku = lideTridy.FirstOrDefault(x => x.Id == vysledek.UserID);
                if (uzivatelVysledku != null)
                {
                    string vyslednyIdentifikator = "";
                    string? uzIdentifikator1 = uzivatelVysledku.Firstname;
                    string? uzIdentifikator2 = uzivatelVysledku.Surname;
                    if(uzIdentifikator1 == null || uzIdentifikator2 == null)
                    {
                        vyslednyIdentifikator = uzIdentifikator1 + " " + uzIdentifikator2;
                    }
                    else
                    {
                        vyslednyIdentifikator = uzivatelVysledku.Nickname;
                    }
                    string radek = vyslednyIdentifikator + " | (" + vysledek.Result + " / " + vysledek.MaxPossible + ")";
                    vysledkyZaku.Add(radek);
                }
            }
            lvVysledkyZaku.ItemsSource = vysledkyZaku;
        }
        if(vysledkyZaku.Count == 0)
        {
            lbVysledky.Text = "\U000026D4";
            frame.IsVisible = false;
            lvVysledkyZaku.IsVisible = false;
        }
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
        Navigation.PopAsync();
    }
}