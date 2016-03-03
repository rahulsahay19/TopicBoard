using System;
using System.Collections.Generic;

namespace TopicsBoard.Data
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}