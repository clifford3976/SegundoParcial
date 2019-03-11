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
    public partial class ReporteCuenta : System.Web.UI.Page
    {
        RepositorioBase<Cuenta> repositorio = new RepositorioBase<Cuenta>();
        Expression<Func<Cuenta, bool>> filtro = C => true;

        protected void Page_Load(object sender, EventArgs e)
        {
            CuentaReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            CuentaReportViewer.Reset();

            CuentaReportViewer.LocalReport.ReportPath = Server.MapPath(@"../Reportes/CuentaReporte.rdlc");

            CuentaReportViewer.LocalReport.DataSources.Clear();

            CuentaReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Cuentas", repositorio.GetList(filtro)));
            CuentaReportViewer.LocalReport.Refresh();
        }
    }
}