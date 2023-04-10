namespace MateMania;

public partial class TitlePage : ContentPage
{
	List<string> tituly = new List<string>();
	public TitlePage()
	{
		InitializeComponent();
        OverTituly();
		if(DbData.nactenyUzivatel.Title1 == 1)
		{
			tituly.Add("�len sme�ky");
		}
		if(DbData.nactenyUzivatel.Title2 == 1)
		{
            tituly.Add("No�n� sova");
        }
        if (DbData.nactenyUzivatel.Title3 == 1)
        {
            tituly.Add("Zlat� mistr");
        }
        if (DbData.nactenyUzivatel.Title4 == 1)
        {
            tituly.Add("Mistr matematiky");
        }
        if (DbData.nactenyUzivatel.Title5 == 1)
        {
            tituly.Add("Legenda");
        }
        if (DbData.nactenyUzivatel.Title6 == 1)
        {
            tituly.Add("�e�itel z�had");
            btnZahada.IsEnabled = false;
        }
        if(tituly.Count == 0)
        {
            tituly.Add("��dn� tituly");
        }
        pckTituly.ItemsSource = tituly;
    }
    private void OverTituly()
    {
        if (DbData.nactenyUzivatel.ChosenClassID != null)
        {
            DbData.ZmenitTitul(1);
        }
        if (DbData.nactenyUzivatel.GoldMedals >= 25)
        {
            DbData.ZmenitTitul(3);
        }
        if (DbData.nactenyUzivatel.BronzeMedals >= 100 && DbData.nactenyUzivatel.SilverMedals >= 100 && DbData.nactenyUzivatel.GoldMedals >= 100)
        {
            DbData.ZmenitTitul(4);
        }
    }

    private void btnZahada_Clicked(object sender, EventArgs e)
    {
        QuestionPage zahadaStranka = new QuestionPage();
        Navigation.PushAsync(zahadaStranka);
    }
}