using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SegundoParcial.Registros
{
    public partial class Cuentas : System.Web.UI.Page
    {
        RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FechadateTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                BalanceTexbox.Text = "0";
                CuentaIDTextbox.Text = "0";
            }
        }

        public Cuenta LlenaClase()
        {
            Cuenta cuentas = new Cuenta();
            int id;
            bool result = int.TryParse(CuentaIDTextbox.Text, out id);
            if (result == true)
            {
                cuentas.CuentaID = id;
            }
            else
            {
                cuentas.CuentaID = 0;
            }

            cuentas.Nombre = nombreTextbox.Text;
            cuentas.Balance = Convert.ToDecimal(BalanceTexbox.Text.ToString());

            return cuentas;
        }

        private void LlenaCampos(Cuenta cuentas)
        {
            CuentaIDTextbox.Text = cuentas.CuentaID.ToString();
            nombreTextbox.Text = cuentas.Nombre;
            BalanceTexbox.Text = cuentas.Balance.ToString();


        }

        private void Limpiar()
        {
            CuentaIDTextbox.Text = "";
            nombreTextbox.Text = "";
            BalanceTexbox.Text = "";

        }


        void MostrarMensaje(TiposMensajes tipo, string mensaje)

        {

            ErrorLabel.Text = mensaje;

            if (tipo == TiposMensajes.Success)

                ErrorLabel.CssClass = "alert-success";

            else

                ErrorLabel.CssClass = "alert-danger";

        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuardarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();

            Cuenta cuenta = LlenaClase();

            bool paso = false;

            if (Page.IsValid)
            {
                if (CuentaIDTextbox.Text == "0")
                {
                    paso = repositorio.Guardar(cuenta);

                }


                else
                {
                    var verificar = repositorio.Buscar(Utilities.Utils.ToInt(CuentaIDTextbox.Text));

                    if (verificar != null)
                    {
                        paso = repositorio.Modificar(cuenta);
                    }
                    else
                    {
                        Utilities.Utils.ShowToastr(this, "Cuenta No Existo", "Fallido", "success");
                        return;
                    }
                }

                if (paso)

                {
                    Utilities.Utils.ShowToastr(this, "Cuenta Registrada", "Exito", "success");
                }

                else

                {
                    Utilities.Utils.ShowToastr(this, "No pudo Guardarse la cuenta", "Exito", "success");
                }
                Limpiar();
                return;
            }
        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();



            int id = Utilities.Utils.ToInt(CuentaIDTextbox.Text);
            var cuenta = repositorio.Buscar(id);


            if (cuenta == null)
            {
                Utilities.Utils.ShowToastr(this, "No se puede Eliminar", "Fallido", "success");
            }

            //Si tiene algun prestamo o deposito enlazado no elimina
            RepositorioBase<Deposito> repositorios = new RepositorioBase<Deposito>();

            if (repositorios.GetList(x => x.CuentaID == id).Count() > 0)
            {
                Utilities.Utils.ShowToastr(this, "No se puede Eliminar, La cuenta contiene depositos", "contiene Depositos", "success");

            }

            else
            {
                repositorio.Eliminar(id);

                Utilities.Utils.ShowToastr(this, "Cuenta a sido Eliminada", "Exito", "success");
                Limpiar();
            }
        }

        protected void BuscarButton_Click(object sender, EventArgs e)
        {
            RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();


            Cuenta cuentas = repositorio.Buscar(Convert.ToInt32(CuentaIDTextbox.Text));
            if (cuentas != null)
            {
                LlenaCampos(cuentas);
            }
            else
            {
                Utilities.Utils.ShowToastr(this, "Usuario no encontrado", "Fallido", "success");

            }
        }
    }
}