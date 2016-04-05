using System.Web.Http;
using System.Web.Mvc;

namespace TopicsBoard.Controllers
{
    public class FileUploadController : ApiController
    {
        public JsonResult Post(string description)
        {
            string Message, Filename, ActualFileName;

            Message = Filename = string.Empty;
            bool flag = false;

            if(Request.Files!=null)
        }
    }
}
