using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace binaryorm.Models
{
    public class Lecture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LectureId { get; set; } 
        public string Name { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
        public Category Category { get; set; } 
        public string Description { get; set; } 
    }
}