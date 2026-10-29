using alanchi.Examen.Models;
using System.Globalization;

namespace alanchi.Examen.Views;

public partial class RegistroPage : ContentPage
{

    const double Costo = 1200;
    readonly string usuario;
    double pagoMensual;
    bool calculado;

    public RegistroPage(string usuario)
	{
        InitializeComponent();
        this.usuario = usuario;
        lblUsuario.Text = $"Usuario conectado: {usuario}";
        dpFecha.MinimumDate = DateTime.Today;
        dpFecha.Date = DateTime.Today;
    }

    void OnEdadChanged(object? s, TextChangedEventArgs e)
    {
        string solo = new string((e.NewTextValue ?? "").Where(char.IsDigit).ToArray());
        if (solo != e.NewTextValue) entEdad.Text = solo;
    }

    bool TryMonto(out double monto) =>
        double.TryParse(entMonto.Text?.Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out monto)
        && monto >= 120 && monto <= 600;


    private void OnCalcular_Clicked(object sender, EventArgs e)
    {
        if (!TryMonto(out double monto))
        {
            DisplayAlertAsync("Error", "El monto inicial debe estar entre 120 y 600", "OK");
            return;
        }
        pagoMensual = (Costo - monto) / 5 + Costo * 0.02 + (swCasillero.IsToggled ? 10 : 0);
        entPago.Text = pagoMensual.ToString("F2", CultureInfo.InvariantCulture);
        calculado = true;
    }

    private void OnResumen_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(entNombre.Text) || string.IsNullOrWhiteSpace(entApellido.Text))
        { DisplayAlertAsync("Error", "Ingrese nombre y apellido", "OK"); return; }
        if (!int.TryParse(entEdad.Text, out int edad) || edad < 16)
        {  DisplayAlertAsync("Error", "La edad debe ser un número mayor o igual a 16", "OK"); return; }
        if (dpFecha.Date < DateTime.Today)
        {  DisplayAlertAsync("Error", "No se permiten fechas pasadas", "OK"); return; }
        if (pkPlan.SelectedIndex < 0)
        {  DisplayAlertAsync("Error", "Seleccione un plan", "OK"); return; }
        if (!TryMonto(out double monto))
        {  DisplayAlertAsync("Error", "El monto inicial debe estar entre 120 y 600", "OK"); return; }
        if (!calculado)
        {  DisplayAlertAsync("Error", "Primero calcule el pago mensual", "OK"); return; }

        var socio = new Socio
        {
            Nombre = entNombre.Text.Trim(),
            Apellido = entApellido.Text.Trim(),
            Edad = edad,
            FechaInicio = (DateTime)dpFecha.Date,
            Plan = pkPlan.SelectedItem.ToString()!,
            Casillero = swCasillero.IsToggled,
            MontoInicial = monto,
            PagoMensual = pagoMensual
        };
         Navigation.PushAsync(new ResumenPage(usuario, socio));
    }

}
