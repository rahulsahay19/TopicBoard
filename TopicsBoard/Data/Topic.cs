using System;
using System.Collections.Generic;
using TopicsBoard.Models;

namespace TopicsBoard.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<FileUpload> FileUploads { get; set; }

        //Need to add few more fields whoCreated and whomodified and also for soft delete
        public bool IsActive { get; set; }
        public string TopicCreatedBy { get; set; }
        public string TopicModifiedBy { get; set; }
       
    }
}