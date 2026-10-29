namespace alanchi.Examen.Views;

public partial class LoginPage : ContentPage
{

    readonly string[,] usuarios = {
        { "recepcion", "gym2026" },
        { "entrenador", "fit_01" },
        { "uisrael", "2026" },
        { "gerente", "admin123" }
    };
    public LoginPage()
	{
		InitializeComponent();
	}

    private void OnLogin_Clicked(object sender, EventArgs e)
    {
        string u = entUsuario.Text?.Trim() ?? "", c = entClave.Text ?? "";
        for (int i = 0; i < usuarios.GetLength(0); i++)
        {
            if (usuarios[i, 0] == u && usuarios[i, 1] == c)
            {
                entClave.Text = "";
                Navigation.PushAsync(new RegistroPage(u));
                return;
            }
        }
        DisplayAlertAsync("Error", "Usuario o contraseña incorrectos", "OK");
        
    }

    private void OnAcercaDe_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AcercaDePage());
    }

    
}