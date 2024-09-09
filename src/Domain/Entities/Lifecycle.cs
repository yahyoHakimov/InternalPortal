using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lifecycle
    {
        public int Id { get; set; }
        public string CurrentState { get; set; }
        public string NextState { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
