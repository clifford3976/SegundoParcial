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
    public partial class ReporteDeposito : System.Web.UI.Page
    {
        RepositorioBase<Deposito> repositorio = new RepositorioBase<Deposito>();
        Expression<Func<Deposito, bool>> filtro = C => true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DepositoReportViewer.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                DepositoReportViewer.Reset();

                DepositoReportViewer.LocalReport.ReportPath = Server.MapPath(@"../Reportes/DepositoReporte.rdlc");

                DepositoReportViewer.LocalReport.DataSources.Clear();

                DepositoReportViewer.LocalReport.DataSources.Add(new ReportDataSource("Depositos", repositorio.GetList(c=>true)));
                DepositoReportViewer.LocalReport.Refresh();
            }
            
        }
    }
}