using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TopicsBoard.Models;

namespace TopicsBoard.Data
{
    public class TopicsBoardRepository : ITopicsBoardRepository
    {
        private TopicsBoardContext _context;

        public TopicsBoardRepository(TopicsBoardContext context)
        {
            _context = context;
        }
        public IQueryable<Topic> GetTopics()
        {
            return _context.Topics;
        }

        public IQueryable<Topic> GetTopicsIncludingTags()
        {
            return _context.Topics.Include("Tags");
        }

        public IQueryable<Tag> GetTags()
        {
            return _context.Tags;
        }

        public IQueryable<Tag> GetTagsWithTopics(int topicId)
        {
            return _context.Tags.Where(r => r.TopicId == topicId && r.IsActive);
        }

        public bool Save()
        {
            try
            {
                //> 0 means some change has been made to db
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                //TODO
                return false;
            }
        }

        public bool AddTopic(Topic newTopic)
        {
            try
            {
                _context.Topics.Add(newTopic);
                return true;
            }
            catch (Exception)
            {
                //TODO
                return false;

            }
        }

        public bool AddTopicWithTag(Topic newTopic, Tag newTag)
        {
            try
            {
                _context.Topics.Add(newTopic);
                _context.Tags.Add(newTag);
                return true;
            }
            catch (Exception)
            {
                //TODO
                return false;

            }
        }

        public bool AddTag(Tag newTag)
        {
            try
            {
                _context.Tags.Add(newTag);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }

        public bool AddTags(IEnumerable<Tag> newTag)
        {
            try
            {
                foreach (var item in newTag)
                {
                    //Adding all items individually to context
                    _context.Tags.Add(item);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }

        public void DeleteTopic(int id)
        {
            Topic topic = _context.Topics.Find(id);
            if (topic != null)
            {
                //This is to make sure that topic will live in db
                //Just to mark it invisible via soft delete
                topic.IsActive = false;
                topic.Modified=DateTime.Now;
                //TO Do: Need to think to update the Modified by flag here
             //   topic.TopicModifiedBy=UserProfile.
               // _context.Topics.Remove(topic);
                _context.SaveChanges();
            }
        }
        public void DeleteTag(int id)
        {
            Tag tag = _context.Tags.Find(id);
            if (tag != null)
            {
               // tag.IsActive = false;
               // tag.Modified=DateTime.Now;
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
        }

        public void UpdateTopic(Topic topic)
        {
            _context.Entry(topic).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //TODO:- Below implementation needs to be changed
        public void UpdateTag(int id, Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteFile(int id)
        {
            FileUpload fileUpload = _context.FileUploads.Find(id);
            if (fileUpload != null)
            {
                fileUpload.IsActive = false;
               // _context.FileUploads.Remove(fileUpload);
                _context.SaveChanges();
            }
            
        }

        public void UploadFile(FileUpload fileUpload)
        {
            try
            {
                _context.FileUploads.Add(fileUpload);
            }
            catch (Exception ex)
            {
                // TODO log this error

            }
        }

        public IEnumerable<FileUpload> DownloadFile(int fileId)
        {
            return _context.FileUploads.Where(t => t.Id == fileId);
        }

        //TODO:- This call needs to be changed for file download
        //AS for file download this will be file file id
        public IQueryable<FileUpload> GetFiles(int topicId)
        {
            return _context.FileUploads.Where(t => t.TopicId == topicId && t.IsActive);
        }
    }
}