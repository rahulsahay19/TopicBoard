using System;

namespace TopicsBoard.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string TagDescription { get; set; }
        public DateTime Created { get; set; }
        public int TopicId { get; set; }
    }
}