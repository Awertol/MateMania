namespace MateMania;

public partial class TitlePage : ContentPage
{
	string[] tituly = { "Mistr matematiky", "�len sme�ky", "No�n� sova", "Zlat� mistr", "Legenda", "�e�itel z�had" };
	public TitlePage()
	{
		InitializeComponent();
		pckTituly.ItemsSource = tituly;
	}
}