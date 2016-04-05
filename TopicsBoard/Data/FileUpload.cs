using System;
using TopicsBoard.Models;

namespace TopicsBoard.Data
{
    public class FileUpload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileSize { get; set; }
        public byte[] FileData { get; set; }

        //TODO: Since, File Upload has the association with Individual Topic and Tag, hence key association is required
        public virtual Topic Topics { get; set; }
      //  public virtual Tag Tags { get; set; }
        public int TopicId { get; set; }
        //public int TagId { get; set; }
        public DateTime Uploaded { get; set; }

        public bool IsActive { get; set; }
        public string FileCreatedBy { get; set; }
        public string FileModifiedBy { get; set; }
    }
}