using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.DynamicData;
using System.Web.Mvc;
using TopicsBoard.Data;
using TopicsBoard.Models;
using TopicsBoard.Services;

namespace TopicsBoard.Controllers
{
    public class HomeController : Controller
    {
        private IMailService _mail;
        private ITopicsBoardRepository _repo;

        public HomeController(IMailService mail, ITopicsBoardRepository repo)
        {
            _mail = mail;
            _repo = repo;
        }

        //TODO:- Need to think on that web.security initialize message
    /*     [Authorize]*/
        public ActionResult Index(string message)
        {
            ViewBag.Message = message;

         var topics = _repo.GetTopicsIncludingTags().
                 OrderByDescending(t => t.Created)
                 .Where(t => t.IsActive)
                 .ToList();

            var tags = _repo.GetTags().
                OrderByDescending(t => t.Created)
                .Where(t => t.IsActive)
                .ToList();

            //Inner Join
           /* var innerJoin = from t in topics
                            join ta in tags
                                on t.Id equals ta.TopicId
                            where ta.IsActive && t.IsActive
                            select t;*/

            //Left outer join
            var outerjoin = from t in topics
                            join ta in tags
                                on t.Id equals ta.TopicId into gj
                            from ta in gj.DefaultIfEmpty()
                       //     where tb.IsActive
                    select t;

            /*var outerjoin = from ta in tags
                            join t in topics
                                on ta.TopicId equals t.Id into gj
                            from t in gj.DefaultIfEmpty()
                            //                 where tb.IsActive
                            select t;*/

            var distinctItems = outerjoin.Distinct();

            //var result = innerJoin.GroupBy(x => x.Id).Select(y => y.FirstOrDefault());
          return View(distinctItems);
        }

    
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel model)
        {
            var msg = string.Format("Comment From: {1}{0}Email:{2}{0}Website: {3}{0}Comment:{4}",
              Environment.NewLine,
              model.Name,
              model.Email,
              model.Website,
              model.Comment);

            if (_mail.SendMail("noreply@yourdomain.com",
              "foo@yourdomain.com",
              "Website Contact",
              msg))
            {
                ViewBag.MailSent = true;
            }

            return View();
        }

        [Authorize]
        public ActionResult MyMessages()
        {
            return View();
        }
    }
}
