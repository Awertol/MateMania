using System.Text;

namespace MateMania;

public partial class OnlinePinPage : ContentPage
{
	public static readonly string[] pins = 
		{ "\U0001F340", 
		"\U0001F49A", 
		"\U0001F335", 
		"\U00002764", 
		"\U0001F427", 
		"\U0001F499", 
		"\U00002600", 
		"\U0001F947", 
		"\U0001F319" };
    
	public OnlinePinPage()
	{
		InitializeComponent();
	}
	int pocetZmacknuti = 0;
	string volenyPin = "";
	private void NastavImgBtn(string tagPressed)
	{
        volenyPin += tagPressed;
        Button vkladanyPinButton = new();
		vkladanyPinButton.MaximumHeightRequest = 50;
		vkladanyPinButton.MaximumWidthRequest = 30;
        vkladanyPinButton.Clicked += VkladanyPinButton_Clicked;
		vkladanyPinButton.BackgroundColor = Colors.White;
		vkladanyPinButton.FontSize = 24;
		vkladanyPinButton.Scale = 0.8;
		vkladanyPinButton.Text += pins[Convert.ToInt32(tagPressed) - 1];
		vkladanyPinButton.ClassId = tagPressed;
		grdPin.Add(vkladanyPinButton);
		Grid.SetColumn(vkladanyPinButton, pocetZmacknuti);
		Grid.SetRow(vkladanyPinButton, 0);
        pocetZmacknuti++;
        if (pocetZmacknuti >= 5)
		{
			TestPage testPage = new TestPage(volenyPin);
			Navigation.PushAsync(testPage);
		}
    }

    private void VkladanyPinButton_Clicked(object sender, EventArgs e)
    {
		var zmacknutyPinSmazani = sender as Button;
		
		pocetZmacknuti--;
		grdPin.Remove(zmacknutyPinSmazani);
		int poc = 0;
		volenyPin = "";
		foreach(var prvekGridu in grdPin.Children)
		{
			if (prvekGridu is Button)
			{
				grdPin.SetColumn(prvekGridu, poc);
				Button prvekGriduImgBtn = (Button)prvekGridu;
				volenyPin += prvekGriduImgBtn.ClassId;
				poc++;
			}
		}
    }

    private void btnVolba_Clicked(object sender, EventArgs e)
    {
		var zmacknutaVolba = sender as Button;
		string tagZmacknuty = zmacknutaVolba.ClassId;
		NastavImgBtn(tagZmacknuty);
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}