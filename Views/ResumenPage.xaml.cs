using alanchi.Examen.Models;

namespace alanchi.Examen.Views;

public partial class ResumenPage : ContentPage
{
    readonly string usuario;
    readonly Socio socio;
    public ResumenPage(string usuario, Socio s)
	{
		InitializeComponent();
        this.usuario = usuario;
        this.socio = s;
        lblUsuario.Text = $"Usuario conectado: {usuario}";
        lblNombre.Text = s.Nombre;
        lblApellido.Text = s.Apellido;
        lblEdad.Text = s.Edad.ToString();
        lblFecha.Text = s.FechaInicio.ToString("dd/MM/yyyy");
        lblPlan.Text = s.Plan;
        lblCasillero.Text = s.Casillero ? "Sí" : "No";
        lblMonto.Text = s.MontoInicial.ToString("F2");
        lblPago.Text = s.PagoMensual.ToString("F2");
        lblTotal.Text = s.PagoTotal.ToString("F2");

    }

    private void OnNuevo_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new NavigationPage(new RegistroPage(usuario));
    }

    private void OnCerrar_Clicked(object sender, EventArgs e)
    {
        Application.Current!.MainPage = new NavigationPage(new LoginPage());
    }

    private void OnGuardar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string contenido =
                $"Usuario conectado: {usuario}\n" +
                $"Nombre: {socio.Nombre}\n" +
                $"Apellido: {socio.Apellido}\n" +
                $"Edad: {socio.Edad}\n" +
                $"Fecha de inicio: {socio.FechaInicio:dd/MM/yyyy}\n" +
                $"Plan: {socio.Plan}\n" +
                $"Casillero: {(socio.Casillero ? "Sí" : "No")}\n" +
                $"Monto inicial: {socio.MontoInicial:F2}\n" +
                $"Pago mensual: {socio.PagoMensual:F2}\n" +
                $"Pago total: {socio.PagoTotal:F2}\n";

            string nombreArchivo = $"Resumen_{socio.Apellido}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
            string ruta = Path.Combine(FileSystem.CacheDirectory, nombreArchivo);
            File.WriteAllTextAsync(ruta, contenido);

            Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Guardar resumen",
                File = new ShareFile(ruta)
            });
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Error", $"No se pudo guardar: {ex.Message}", "OK");
        }
    }
}