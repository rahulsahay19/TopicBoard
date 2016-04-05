using System;
using System.Collections.Generic;
using TopicsBoard.Models;

namespace TopicsBoard.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagDescription { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
        public int TopicId { get; set; }

        public virtual Topic Topics { get; set; }

        public ICollection<FileUpload> FileUploads { get; set; }

        public bool IsActive { get; set; }
        public string TagCreatedBy { get; set; }
        public string TagModifiedBy { get; set; }
    }
}