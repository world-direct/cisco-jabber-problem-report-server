using System;
using System.Configuration;
using System.IO;
using System.Web.Mvc;
using NLog;

namespace CiscoJabberProblemReportServer.Controllers
{
    public class ProblemRPTController : Controller
    {
        private readonly string _destinationDirectory;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ProblemRPTController()
        {
            _destinationDirectory = ConfigurationManager.AppSettings.Get("ProblemUploadDestinationDirectory");
        }

        // GET: ProblemRPT
        public ActionResult Index()
        {
            _logger.Info("Returning Index() view");
            return View();
        }

        [HttpPost]
        public ActionResult UploadCiscoProblemRPT()
        {
            try
            {
                _logger.Info("New Cisco Jabber Problem Report uploaded");
                var file = System.Web.HttpContext.Current.Request.Files.Count > 0
                    ? System.Web.HttpContext.Current.Request.Files[0]
                    : null;

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    _logger.Debug($"Received incoming file `{fileName}`");

                    _logger.Trace($"Combining path in order to write file to output directory");
                    var path = Path.Combine(
                        _destinationDirectory,
                        fileName
                    );
                    _logger.Debug($"Trying to write file to `{path}`.");
                    file.SaveAs(path);
                }
                else
                {
                    _logger.Warn($"POST request does not contain a proper file, throwing exception...");
                    throw new ArgumentException("file");
                }

                return new HttpStatusCodeResult(200);
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unhandled exception while trying to process new problem report");
                throw;
            }
        }
    }
}