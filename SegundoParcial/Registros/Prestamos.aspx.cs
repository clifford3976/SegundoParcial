using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SegundoParcial.Registros
{
    public partial class Prestamos : System.Web.UI.Page
    {

        RepositorioBase<Prestamo> repositorioBase = new RepositorioBase<Prestamo>();
        RepositorioPrestamo repositorioPrestamo = new RepositorioPrestamo();
        List<CuotaDetalle> Detalle = new List<CuotaDetalle>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LlenaComboCuentaID();
                FechadateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                PrestamoIDTextbox.Text = "0";

                
            }
        }

        private Prestamo LlenaClase()
        {
            Prestamo prestamo = new Prestamo();
            int id;
            bool result = int.TryParse(PrestamoIDTextbox.Text, out id);
            if (result == true)
            {
                prestamo.PrestamoId = id;
            }
            else
            {
                prestamo.PrestamoId = 0;
            }

            prestamo.CuentaId = Utilities.Utils.ToInt(CuentaDropDownList.SelectedValue);
            prestamo.Capital = Utilities.Utils.ToDecimal(CapitalTexbox.Text);
            prestamo.Interes = Utilities.Utils.ToDecimal(InteresesTextBox.Text);
            prestamo.Tiempo = Utilities.Utils.ToInt(TiempoTextBox.Text);
            prestamo.Detalle = (List<CuotaDetalle>)ViewState["Cuota"];
            
            return prestamo;
        }

        private void LlenaCampos(Prestamo prestamo)
        {
            Limpiar();
            PrestamoIDTextbox.Text = prestamo.PrestamoId.ToString();
            LlenaComboCuentaID();
            CapitalTexbox.Text = prestamo.Capital.ToString();
            InteresesTextBox.Text = prestamo.Interes.ToString();
            TiempoTextBox.Text = prestamo.Tiempo.ToString();
            CuotasGridView.DataSource = prestamo.Detalle;
            CuotasGridView.DataBind();
        }

        private void Limpiar()
        {
            PrestamoIDTextbox.Text = "0";
            FechadateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            CapitalTexbox.Text = string.Empty;
            InteresesTextBox.Text = string.Empty;
            TiempoTextBox.Text = string.Empty;
            CuotasGridView.DataSource = null;
            CuotasGridView.DataBind();
            LlenaComboCuentaID();
            ViewState["Cuota"] = null;
        }

        private void LlenaComboCuentaID()
        {
            RepositorioBase<Cuenta> cuentas = new RepositorioBase<Cuenta>();
            CuentaDropDownList.Items.Clear();
            CuentaDropDownList.DataSource = cuentas.GetList(x => true);
            CuentaDropDownList.DataValueField = "CuentaID";
            CuentaDropDownList.DataTextField = "Nombre";
            CuentaDropDownList.DataBind();
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(CuentaDropDownList.SelectedValue) == 0)
                return;


            RepositorioPrestamo repositorio = new RepositorioPrestamo();
            Prestamo prestamo = LlenaClase();

            RepositorioBase<Prestamo> cuentas = new RepositorioBase<Prestamo>();

            var validar = cuentas.Buscar(Utilities.Utils.ToInt(CuentaDropDownList.SelectedValue));

            bool paso = false;


            if (validar != null)
            {

                if (Page.IsValid)
                {
                    if (PrestamoIDTextbox.Text == "0")
                    {
                        paso = repositorio.Guardar(prestamo);

                    }

                    else
                    {
                        var verificar = repositorio.Buscar(Utilities.Utils.ToInt(PrestamoIDTextbox.Text));
                        if (verificar != null)
                        {
                            paso = repositorio.Modificar(prestamo);
                        }
                        else
                        {
                            Utilities.Utils.ShowToastr(this, "No se encuentra el ID", "Fallo", "success");
                            return;
                        }
                    }

                    if (paso)

                    {
                        Utilities.Utils.ShowToastr(this, "Registro Con Exito", "Exito", "success");

                    }

                    else

                    {
                        Utilities.Utils.ShowToastr(this, "No se pudo Guardar", "Fallo", "success");
                    }
                    Limpiar();

                    return;
                }


            }
            else
            {
                Utilities.Utils.ShowToastr(this, "El numero de cuenta no existe", "Fallo", "success");
                return;


            }

        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioPrestamo repositorio = new RepositorioPrestamo();
            RepositorioBase<Prestamo> dep = new RepositorioBase<Prestamo>();


            int id = Utilities.Utils.ToInt(PrestamoIDTextbox.Text);
            var depositos = repositorio.Buscar(id);


            if (depositos == null)
            {
                Utilities.Utils.ShowToastr(this, "El deposito no existe", "Fallo", "success");
            }

            else
            {
                repositorio.Eliminar(id);



                Utilities.Utils.ShowToastr(this, "Elimino Correctamente", "Exito", "success");
                Limpiar();
            }

        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Prestamo> repositorio = new RepositorioBase<Prestamo>();


            Prestamo prestamo = repositorio.Buscar(Utilities.Utils.ToInt(PrestamoIDTextbox.Text));

            Limpiar();
            if (prestamo != null)
            {
                LlenaCampos(prestamo);

                Utilities.Utils.ShowToastr(this, "Se ha Encontrado su deposito", "Exito", "success");
            }
            else
            {
                Utilities.Utils.ShowToastr(this, "el ID registrado no existe", "Fallido", "success");
            }
        }

        protected void CalcularButton_Click(object sender, EventArgs e)
        {
            int acu = 1;
            double Capital = double.Parse(CapitalTexbox.Text);
            double Interes = double.Parse(InteresesTextBox.Text) / 1200;
            double Tiempo = double.Parse(TiempoTextBox.Text);

            double Cuota = Capital * (Interes / (double)(1 - Math.Pow(1 + (double)Interes, -Tiempo)));
            double InteresMensual = 0, AmTotal = 0, Am = 0;
            Expression<Func<Prestamo, bool>> filtro = x => true;

            for (int i = 0; i < Tiempo; ++i)
            {
                CuotaDetalle Detalle1 = new CuotaDetalle();
                InteresMensual = Math.Round((Interes * Capital), 2);
                Capital = Math.Round(Capital - Cuota + InteresMensual, 2);

                AmTotal += Math.Round(Cuota - InteresMensual, 2);
                Am = Cuota - InteresMensual;
                Detalle1.PrestamoId = repositorioPrestamo.GetList(filtro).Count + 1;
                Detalle1.Valor = Math.Round((decimal)Cuota, 2);
                Detalle1.Capital = Math.Round((decimal)Am, 2);
                Detalle1.Interes = Math.Round((decimal)InteresMensual, 2);
                if (i == Tiempo - 1)
                {
                    decimal Aux = Math.Round((decimal)Capital, MidpointRounding.AwayFromZero);
                    if (Aux == 0)
                        Detalle1.Balance = (decimal)0.00;
                }
                else
                    Detalle1.Balance = Math.Round((decimal)Capital, 2);
                Detalle.Add(Detalle1);
                //repositorioPrestamo.Guardar(prestamo);
                ++acu;
            }
            CuotasGridView.DataSource = Detalle;
            CuotasGridView.DataBind();
        }
    }
    }
