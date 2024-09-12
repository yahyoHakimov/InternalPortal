using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Lifecycle
    {
        [Key]
        public int Id { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Add foreign key if this Lifecycle is related to another entity (example with Request)
        public int? RelatedEntityId { get; set; } // Optional foreign key to another entity
        public string RelatedEntityType { get; set; } // To track which entity this lifecycle belongs to
    }
}
