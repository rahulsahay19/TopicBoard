using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TopicsBoard.Data;

namespace TopicsBoard.Controllers
{
    public class TagsController : ApiController
    {
        private ITopicsBoardRepository _repo;

        public TagsController(ITopicsBoardRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Tag> Get(int topicId)
        {
            return _repo.GetTagsWithTopics(topicId);
        }

        //This is for individual Tag with topic
        public HttpResponseMessage Post(int topicId, [FromBody] Tag newTag)
        {
            if (newTag.Created == default(DateTime))
            {
                newTag.Created = DateTime.Now;
                newTag.TagCreatedBy = User.Identity.Name;
                newTag.IsActive = true;
            }

            newTag.TopicId = topicId;

            if (_repo.AddTag(newTag) &&
                _repo.Save())
            {
                return Request.CreateResponse(HttpStatusCode.Created,
                  newTag);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

 
        public HttpResponseMessage Delete(int id)
        {
            //TODO:- soft delete creating Cyclomatic dependecy as it is referred both ways in table
            _repo.DeleteTag(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}

