namespace alanchi.Examen.Views;

public partial class AcercaDePage : ContentPage
{
	public AcercaDePage() 
	{
		InitializeComponent();
		
    }

    private void OnVolver_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();

    }
}