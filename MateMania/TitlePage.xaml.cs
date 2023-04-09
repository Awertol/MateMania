namespace MateMania;

public partial class TitlePage : ContentPage
{
	string[] tituly = { "Mistr matematiky", "Èlen smeèky", "Noèní sova", "Zlatý mistr", "Legenda", "Øešitel záhad" };
	public TitlePage()
	{
		InitializeComponent();
		pckTituly.ItemsSource = tituly;
	}
}