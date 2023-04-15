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
                    string radek = uzivatelVysledku.Firstname + " " + uzivatelVysledku.Surname + " | (" + vysledek.Result + " / " + vysledek.MaxPossible + ")";
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
    }
}