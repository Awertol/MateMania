using MateMania.Models;

namespace MateMania;

public partial class ExamResultPage : ContentPage
{
	string[] poleJmenVysledku;
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
                    string radek = uzivatelVysledku.Firstname + " " + uzivatelVysledku.Surname + " | (" + vysledek.MaxPossible + " / " + vysledek.Result + ")";
                    vysledkyZaku.Add(radek);
                }
            }
            lvVysledkyZaku.ItemsSource = vysledkyZaku;
        }
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}