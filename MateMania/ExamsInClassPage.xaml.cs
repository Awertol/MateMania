using MateMania.Models;

namespace MateMania;
public class ShowedExamData
{
	public string PIN { get; set; }
	public string? Name { get; set; }
	public DateTime Creation { get; set; }
}
public partial class ExamsInClassPage : ContentPage
{
	public List<string> seznamZadani = new List<string>();
	List<ExamsModel> nactenaZadaniTridy;

    public ExamsInClassPage()
	{
		InitializeComponent();
		NactiOtazkyVeTride();
    }
	private async void NactiOtazkyVeTride()
	{
        Task<List<ExamsModel>> taskOtazkyVeTride = DbData.NajitOtazkyTridy();
        List<ExamsModel> Exams = await taskOtazkyVeTride;
		foreach(var prvekListView in Exams)
		{
			string radek = prvekListView.ExamName + "\n" + prvekListView.PIN + "\t" + prvekListView.Creation;
			seznamZadani.Add(radek);
		}
        lvSeznamZadani.ItemsSource = seznamZadani;
		nactenaZadaniTridy = Exams;
    }

    private async void lvSeznamZadani_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		string vybraneZad = (lvSeznamZadani.SelectedItem as string);
		string pin = "";
		foreach(char c in vybraneZad)
		{
			if(char.IsAsciiDigit(c))
			{
				pin += c;
			}
		}
		pin = pin.Substring(0, 5);
		if(nactenaZadaniTridy != null)
		{
			ExamsModel zvoleneZadani = nactenaZadaniTridy.Find(x => x.PIN == pin);
			int idZvoleneZadani = zvoleneZadani.Id;
			Task<List<ExamAnswersModel>> taskOdpovedi = DbData.NajitOdpovedi(idZvoleneZadani);
			List<ExamAnswersModel> nacteneOdpovedi = await taskOdpovedi;
			ExamResultPage vysledkyZakuStranka = new(nacteneOdpovedi);
			Navigation.PushAsync(vysledkyZakuStranka);
		}
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}