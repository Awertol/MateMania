using MateMania.Models;
using Microsoft.Maui.Controls;
using System.Net.NetworkInformation;
using System.Text;

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
			string pin = "";
			for(int i  = 0; i < prvekListView.PIN.Length; i++)
			{
				pin += OnlinePinPage.pins[Convert.ToInt32(prvekListView.PIN[i].ToString())-1];
			}
			string radek = prvekListView.ExamName + "|" + pin + "|";
			DateOnly datum = DateOnly.FromDateTime((DateTime)prvekListView.Creation);
			radek += datum;
			seznamZadani.Add(radek);
		}
        lvSeznamZadani.ItemsSource = seznamZadani;
		nactenaZadaniTridy = Exams;
    }
    private async void lvSeznamZadani_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		string vybraneZad = (lvSeznamZadani.SelectedItem as string);
		string pin = "";
		int indexCary = vybraneZad.IndexOf('|');
		pin = vybraneZad.Substring(indexCary+1);
        int indexCary2 = pin.IndexOf('|');
        pin = pin.Substring(0, indexCary2);
        string emojiString = "";
        for (int i = 0; i < pin.Length; i++)
        {
            char vec = pin[i];
            int hodnotaZnaku = vec;
            switch(hodnotaZnaku)
            {
                case 9727 or 9728:
                    pin += "1";
                    break;
                case 55357:
                    pin += "2";
                    break;
                case 56474:
                    pin += "3";
                    break;
                
            }
        }
        await DisplayAlert("OK", pin, "xd");
        if (nactenaZadaniTridy != null)
        {
            try
            {
                ExamsModel zvoleneZadani = nactenaZadaniTridy.Find(x => x.PIN == pin);
                int idZvoleneZadani = zvoleneZadani.Id;
                Task<List<ExamAnswersModel>> taskOdpovedi = DbData.NajitOdpovedi(idZvoleneZadani);
                List<ExamAnswersModel> nacteneOdpovedi = await taskOdpovedi;
                ExamResultPage vysledkyZakuStranka = new(nacteneOdpovedi);
                Navigation.PushAsync(vysledkyZakuStranka);
            }
            catch (Exception ex)
            { }
		}
    }

    private void btnZpet_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}