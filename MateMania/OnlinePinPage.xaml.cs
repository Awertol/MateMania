using System.Text;

namespace MateMania;

public partial class OnlinePinPage : ContentPage
{
	readonly string[] pins = { "\0x1F340", "\0x1F427", "\0x1F335", "\0x2764", "\0x1F49A", "\01F499", "\0x2600", "\0x2694", "\0x1F319" };
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
		vkladanyPinButton.MaximumHeightRequest = 40;
		vkladanyPinButton.MaximumWidthRequest = 40;
        vkladanyPinButton.Clicked += VkladanyPinButton_Clicked;
		vkladanyPinButton.BackgroundColor = Colors.Black;
		vkladanyPinButton.FontSize = 16;
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