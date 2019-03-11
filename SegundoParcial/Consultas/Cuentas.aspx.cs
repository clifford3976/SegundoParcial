using BLL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SegundoParcial.Consultas
{
    public partial class Cuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DesdeTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                HastaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void ButtonBuscar_Click1(object sender, EventArgs e)
        {
            CuentaGridView.DataBind();
            Expression<Func<Cuenta, bool>> filtro = x => true;
            RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();

            int id;

            DateTime desde = Convert.ToDateTime(DesdeTextBox.Text);
            DateTime hasta = Convert.ToDateTime(HastaTextBox.Text);

            switch (TipodeFiltro.SelectedIndex)
            {
                case 0://ID

                    id = Utilities.Utils.ToInt(TextCriterio.Text);
                    if (FechaCheckBox.Checked == true)
                    {
                        filtro = x => x.CuentaID == id && (x.Fecha >= desde && x.Fecha <= hasta);
                    }
                    else
                    {
                        filtro = c => c.CuentaID == id;
                    }

                    if (repositorio.GetList(filtro).Count() == 0)
                    {
                        Utilities.Utils.ShowToastr(this, "Cuenta No Existe", "Fallido", "success");
                        return;
                    }

                    break;

                case 1:// Nombre

                    if (FechaCheckBox.Checked == true)
                    {
                        filtro = x => x.Nombre.Contains(TextCriterio.Text) && (x.Fecha >= desde && x.Fecha <= hasta);
                    }
                    else
                    {
                        filtro = c => c.Nombre.Contains(TextCriterio.Text);
                    }

                    if (repositorio.GetList(filtro).Count() == 0)
                    {
                        Utilities.Utils.ShowToastr(this, "Nombre no existe", "Fallido", "success");
                        return;
                    }

                    break;



                case 2:// Balance

                    decimal balance = Utilities.Utils.ToDecimal(TextCriterio.Text);
                    if (FechaCheckBox.Checked == true)
                    {
                        filtro = x => x.Balance == balance && (x.Fecha >= desde && x.Fecha <= hasta);
                    }
                    else
                    {
                        filtro = c => c.Balance == balance;
                    }
                    if (repositorio.GetList(filtro).Count() == 0)
                    {
                        Utilities.Utils.ShowToastr(this, "Balance no encontrado", "Fallido", "success");
                        return;
                    }
                    break;

                case 3://Todos

                    if (FechaCheckBox.Checked == true)
                    {
                        filtro = x => true && (x.Fecha >= desde && x.Fecha <= hasta);
                    }
                    else
                    {
                        filtro = x => true;
                    }

                    if (repositorio.GetList(filtro).Count() == 0)
                    {
                        Utilities.Utils.ShowToastr(this, "No existen dichas cuentas", "Fallido", "success");
                        return;
                    }
                    break;

            }

            CuentaGridView.DataSource = repositorio.GetList(filtro);
            CuentaGridView.DataBind();


        }

        protected void ReporteButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Reportes/ReporteCuenta.aspx");
        }
    }
    
}