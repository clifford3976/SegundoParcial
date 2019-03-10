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
    public partial class Prestamos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                DesdeTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                HastaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        RepositorioBase<Prestamo> repositorio = new RepositorioBase<Prestamo>();
        bool paso = false;
        Expression<Func<Prestamo, bool>> filtrar = x => true;


        protected void ButtonBuscar_Click1(object sender, EventArgs e)
        {
            var DesdeDateTime = Convert.ToDateTime(DesdeTextBox.Text);
            var HastaDateTime = Convert.ToDateTime(HastaTextBox.Text);
            int id = 0;
            if (TextCriterio.Text == string.Empty && FechaCheckBox.Checked == true)
            {
                filtrar = t => true && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
            }
            else
            {
                filtrar = t => true;
            }
            switch (TipodeFiltro.SelectedIndex)
            {

                //Lista todo
                case 0:

                    if (FechaCheckBox.Checked == true)
                    {
                        filtrar = t => true && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
                    }
                    else
                    {
                        filtrar = t => true;
                    }

                    break;

                case 1:
                    if (paso)
                        return;
                    id = int.Parse(TextCriterio.Text);
                    if (FechaCheckBox.Checked == true)
                    {
                        filtrar = t => t.PrestamoId == id && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
                    }
                    else
                    {
                        filtrar = t => t.PrestamoId == id;
                    }

                    break;

                case 2:
                    if (paso)
                        return;
                    if (FechaCheckBox.Checked == true)
                    {
                        filtrar = t => t.CuentaId.Equals(TextCriterio.Text) && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
                    }
                    else
                    {
                        filtrar = t => t.CuentaId.Equals(TextCriterio.Text);

                    }
                    break;

                case 3:

                    if (FechaCheckBox.Checked == true)
                    {
                        filtrar = t => t.Interes.Equals(TextCriterio.Text) && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
                    }
                    else
                    {
                        filtrar = t => t.Interes.Equals(TextCriterio.Text);
                    }

                    break;

                case 4:
                    if (paso)
                        return;
                    if (FechaCheckBox.Checked == true)
                    {
                        filtrar = t => t.Capital.Equals(TextCriterio.Text) && (t.Fecha >= DesdeDateTime.Date) && (t.Fecha <= HastaDateTime.Date);
                    }
                    {
                        filtrar = t => t.Capital.Equals(TextCriterio.Text);
                    }

                    break;

            }

            PrestamoGridView.DataSource = repositorio.GetList(filtrar);
            PrestamoGridView.DataBind();


        }
    }
    
}