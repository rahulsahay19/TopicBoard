using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.ModelBinding;
using TopicsBoard.Data;
using TopicsBoard.Models;

namespace TopicsBoard.Controllers
{
    public class TopicsController : ApiController
    {
        private ITopicsBoardRepository _repo;

        public TopicsController(ITopicsBoardRepository repo)
        {
            _repo = repo;
        }
        public IEnumerable<Topic> Get()
        {
           
            //TODO: Need to discuss delete tag inactivation
            var topics = _repo.GetTopicsIncludingTags().
                 OrderByDescending(t => t.Created)
                 .Where(t => t.IsActive)
                 .ToList();

            var tags = _repo.GetTags().
                OrderByDescending(t => t.Created)
                .Where(t => t.IsActive)
                .ToList();

            //Inner Join
             var innerJoin = from t in topics
                join ta in tags
                    on t.Id equals ta.TopicId
                where ta.IsActive && t.IsActive
                select t;

            //Left Outer Join

           var outerjoin = from t in topics
                            join ta in tags
                                on  t.Id equals ta.TopicId into gj
                            from tb in gj.DefaultIfEmpty()
           //                 where tb.IsActive
                        select t;

            //Right outer join
         /*   var outerjoin = from ta in tags
                            join t in topics
                                on ta.TopicId equals t.Id into gj
                            from t in gj.DefaultIfEmpty()
                            //                 where tb.IsActive
                            select t;*/
           
            
            //Now filter the redundant rows
            var distinctItems = outerjoin.Distinct();
          //  var result = innerJoin.GroupBy(x => x.Id).Select(y => y.FirstOrDefault());

          
            return distinctItems;
        }

 
        public HttpResponseMessage Post([FromBody] Topic newTopic)
        {
            if (newTopic.Created == default(DateTime))
            {
                newTopic.Created = DateTime.Now;
                newTopic.TopicCreatedBy = User.Identity.Name;
                newTopic.IsActive = true;
            }

            if (_repo.AddTopic(newTopic) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  newTopic);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }


        public HttpResponseMessage Delete(int id)
        {
            //TO Do Soft Delete
            _repo.DeleteTopic(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }

        public HttpResponseMessage Put([FromBody] Topic topic)
        {
            topic.Modified = DateTime.Now;
            topic.TopicModifiedBy = User.Identity.Name;
            topic.IsActive = true;
            _repo.UpdateTopic(topic);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }

}
