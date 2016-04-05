using System.Collections.Generic;
using System.Linq;

namespace TopicsBoard.Data
{
  public interface ITopicsBoardRepository
  {
      IQueryable<Topic> GetTopics();
      IQueryable<Topic> GetTopicsIncludingTags();
      IQueryable<Tag> GetTags();
      IQueryable<Tag> GetTagsWithTopics(int topicId);
      IQueryable<FileUpload> GetFiles(int topicId); 
      bool Save();
      bool AddTopic(Topic newTopic);
      bool AddTopicWithTag(Topic newTopic, Tag newTag);
      bool AddTag(Tag newTag);
      bool AddTags(IEnumerable<Tag> newTag);
      void DeleteTopic(int id);
      void DeleteTag(int id);
      void UpdateTopic(Topic topic);
      void UpdateTag(int id, Tag tag);
      void DeleteFile(int id);
      void UploadFile(FileUpload fileUpload);
      IEnumerable<FileUpload> DownloadFile(int fileId);
  }
}
