namespace MateMania;

public partial class OnlinePinPage : ContentPage
{
	public OnlinePinPage()
	{
		InitializeComponent();
	}
	int pocetZmacknuti = 0;
	string volenyPin = "";
	private void NastavImgBtn(string tagPressed)
	{
        volenyPin += tagPressed;
        ImageButton vkladanyPinButton = new();
		vkladanyPinButton.MaximumHeightRequest = 40;
		vkladanyPinButton.MaximumWidthRequest = 40;
        vkladanyPinButton.Clicked += VkladanyPinButton_Clicked;

        vkladanyPinButton.Source = $"pin{tagPressed}.png";
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
		var zmacknutyPinSmazani = sender as ImageButton;
		
		pocetZmacknuti--;
		grdPin.Remove(zmacknutyPinSmazani);
		int poc = 0;
		volenyPin = "";
		foreach(var prvekGridu in grdPin.Children)
		{
			if (prvekGridu is ImageButton)
			{
				grdPin.SetColumn(prvekGridu, poc);
				ImageButton prvekGriduImgBtn = (ImageButton)prvekGridu;
				volenyPin += prvekGriduImgBtn.ClassId;
				poc++;
			}
		}
    }

    private void btnVolba_Clicked(object sender, EventArgs e)
    {
		var zmacknutaVolba = sender as ImageButton;
		string tagZmacknuty = zmacknutaVolba.ClassId;
		NastavImgBtn(tagZmacknuty);
    }

    private void btnMenu_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}