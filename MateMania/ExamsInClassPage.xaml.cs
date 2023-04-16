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
        if (Exams != null)
        {
            foreach (var prvekListView in Exams.Where(x => x.TeacherID == DbData.nactenyUzivatel.Id).ToList())
            {
                string pin = "";
                for (int i = 0; i < prvekListView.PIN.Length; i++)
                {
                    pin += OnlinePinPage.pins[Convert.ToInt32(prvekListView.PIN[i].ToString()) - 1];
                }
                string radek = prvekListView.ExamName + "|" + pin + "|";
                DateOnly datum = DateOnly.FromDateTime((DateTime)prvekListView.Creation);
                radek += datum;
                seznamZadani.Add(radek);
            }
            lvSeznamZadani.ItemsSource = seznamZadani;
            nactenaZadaniTridy = Exams;
        }
    }
    private async void lvSeznamZadani_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		string vybraneZad = (lvSeznamZadani.SelectedItem as string);
		string emojiString = "";
		int indexCary = vybraneZad.IndexOf('|');
        emojiString = vybraneZad.Substring(indexCary+1);
        int indexCary2 = emojiString.IndexOf('|');
        emojiString = emojiString.Substring(0, indexCary2);
        string pin = "";
        for (int i = 0; i < emojiString.Length; i++)
        {
            char vec = emojiString[i];
            int hodnotaZnaku = vec;
            switch(hodnotaZnaku)
            {
                case 57152:
                    pin += "1";
                    break;
                case 56474:
                    pin += "2";
                    break;
                case 57141:
                    pin += "3";
                    break;
                case 10084:
                    pin += "4";
                    break;
                case 56359:
                    pin += "5";
                    break;
                case 56473:
                    pin += "6";
                    break;
                case 9727 or 9728:
                    pin += "7";
                    break;
                case 56647:
                    pin += "8";
                    break;
                case 57113:
                    pin += "9";
                    break;

            }
        }
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