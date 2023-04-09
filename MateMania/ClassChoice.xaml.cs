namespace MateMania;

public partial class ClassChoice : ContentPage
{
	public ClassChoice()
	{
		InitializeComponent();
	}

    private void imgTrida_Clicked(object sender, EventArgs e)
    {
		var zmacknutaVolba = (sender as ImageButton);
		int rocnik = Convert.ToInt32(zmacknutaVolba.ClassId);
		TestPage test = new TestPage(rocnik);
		Navigation.PushAsync(test);
    }
}