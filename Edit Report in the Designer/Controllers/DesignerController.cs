using Stimulsoft.Report;
using Stimulsoft.Report.Mvc;
using System.Data;
using System.Web.Mvc;

namespace HTML_Samples.Controllers
{
    public class DesignerController : Controller
    {
        static DesignerController()
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
        string reportName = "MyReport.mrt";
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
            DataSet data = new DataSet("Demo");
            data.ReadXml(Server.MapPath("~/Content/Data/Demo.xml"));

            StiReport report = StiMvcDesigner.GetActionReportObject();
            RegisterReportData(report);

            return StiMvcDesigner.PreviewReportResult(report);
        }

        private static void RegisterReportData(StiReport report)
        {
            //report.Dictionary.BusinessObjects.Clear();

            report.RegBusinessObject("Principal", Utils.Get());
            report.RegBusinessObject("Test", Utils.Get());
            report.RegBusinessObject("TestMyObject3", Utils.Get());

            report.Dictionary.SynchronizeBusinessObjects(4);
        }

        public ActionResult DesignerEvent()
        {
            return StiMvcDesigner.DesignerEventResult();
        }
    }
}