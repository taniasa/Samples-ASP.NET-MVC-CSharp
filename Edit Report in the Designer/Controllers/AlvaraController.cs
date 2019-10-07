using HTML_Samples.Classes;
using HTML_Samples.Properties;
using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTML_Samples.Controllers
{
    public class AlvaraController : Controller
    {
        static AlvaraController()
        {
            // How to Activate
            //Stimulsoft.Base.StiLicense.Key = "6vJhGtLLLz2GNviWmUTrhSqnO...";
            //Stimulsoft.Base.StiLicense.LoadFromFile("license.key");
            //Stimulsoft.Base.StiLicense.LoadFromStream(stream);
        }

        public ActionResult Index()
        {
            return View();
        }
        string reportName = "alvara2.mrt";
        public ActionResult GetReport()
        {
            StiReport report = new StiReport();
            report.Load(Server.MapPath($"~/Content/Reports/{reportName}"));

            RegisterReportData(report);

            return StiMvcDesigner.GetReportResult(report);
        }

        public ActionResult SaveReportDesigner()
        {
            StiReport report = StiMvcDesigner.GetReportObject();
            string packedReport = report.SavePackedReportToString();


            report.SavePackedReport(Server.MapPath("~/Content/Reports/" + reportName));
            return StiMvcDesigner.SaveReportResult();
        }

        public ActionResult PreviewReport()
        {
            //DataSet data = new DataSet("Demo");
            //data.ReadXml(Server.MapPath("~/Content/Data/Demo.xml"));

            StiReport report = StiMvcDesigner.GetActionReportObject();
            RegisterReportData(report);

            return StiMvcDesigner.PreviewReportResult(report);
        }

        private static void RegisterReportData(StiReport report)
        {
            //report.Dictionary.BusinessObjects.Clear();

            //report.RegBusinessObject("Principal", Utils.Get());
            //report.RegBusinessObject("Test", Utils.Get());
            //report.RegBusinessObject("TestMyObject3", Utils.Get());

            report.RegBusinessObject("Assinaturas", new List<ReponsavelModeloRelatorioDto>());
            report.RegBusinessObject("Cabecalho", new List<CabecalhoDto>());
            report.RegBusinessObject("CodigoAutenticidade", new { Data = string.Empty });


            report.RegBusinessObject("QrCode", new
            {
                Data = string.Empty
            });
            //var dados = System.IO.File.ReadAllText(Resources.alvara);
            var funcionamento = Newtonsoft.Json.JsonConvert.DeserializeObject<AlvaraFuncionamentoDto>(Resources.alvara);
            report.RegBusinessObject("Dados_Alvara_Funcionamento", funcionamento);

            report.Dictionary.SynchronizeBusinessObjects(2);
        }

        public ActionResult DesignerEvent()
        {
            return StiMvcDesigner.DesignerEventResult();
        }
    }
}