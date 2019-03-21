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
        RepositorioBase<Cuentas> repositorio = new RepositorioBase<Cuentas>();
        Expression<Func<Cuentas, bool>> filtro = C => true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CuentaReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                CuentaReportViewer.Reset();

                CuentaReportViewer.LocalReport.ReportPath = Server.MapPath(@"../Reportes/CuentaReporte.rdlc");

                CuentaReportViewer.LocalReport.DataSources.Clear();

                CuentaReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Cuentas", repositorio.GetList(c=>true)));
                CuentaReportViewer.LocalReport.Refresh();
            }

            
        }
    }
}