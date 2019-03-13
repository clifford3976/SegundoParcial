using BLL;
using Entities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SegundoParcial.Reportes
{
    public partial class ReportePrestamo : System.Web.UI.Page
    {

        RepositorioBase<Prestamo> repositorio = new RepositorioBase<Prestamo>();
        Expression<Func<Prestamo, bool>> filtro = C => true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PrestamoReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                PrestamoReportViewer.Reset();

                PrestamoReportViewer.LocalReport.ReportPath = Server.MapPath(@"../Reportes/PrestamoReporte.rdlc");

                PrestamoReportViewer.LocalReport.DataSources.Clear();

                PrestamoReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Prestamos", repositorio.GetList(c=>true)));
                PrestamoReportViewer.LocalReport.Refresh();

            }
         
        }
    }
}