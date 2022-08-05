﻿namespace MyBoards.entities
{
    public class Comment
    {
        public int Id { get; set; } 
        public string Message { get; set; }
        public virtual User Author { get; set; }
        public Guid AuthorId { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }
        
    }
}
