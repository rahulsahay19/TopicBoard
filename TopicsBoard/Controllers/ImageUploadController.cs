using System;
using System.Web.Mvc;
using TopicsBoard.Data;

namespace TopicsBoard.Controllers
{
    public class ImageUploadController : Controller
    {
        private ITopicsBoardRepository _repo;

        public ImageUploadController(ITopicsBoardRepository repo)
        {
            _repo = repo;
        }

        //
        // GET: /ImageUpload/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadFiles(int id)
        {
            var length = Request.ContentLength;
            var bytes = new byte[length];
            Request.InputStream.Read(bytes, 0, length);

            var fileName = Request.Headers["X-File-Name"];
            var fileSize = Request.Headers["X-File-Size"];
            var fileType = Request.Headers["X-File-Type"];

            FileUpload fileUpload = new FileUpload()
            {
                FileData = bytes,
                FileName = fileName,
                FileType = fileType,
                FileSize = fileSize,
                Uploaded = DateTime.Now,
                TopicId = id,
                FileCreatedBy = User.Identity.Name,
                IsActive = true

                //TagId = 
                
            };
            _repo.UploadFile(fileUpload);
            _repo.Save();

        }
    }
}
