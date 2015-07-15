using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace binaryorm.Models
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; } 
        public string Name { get; set; } 
        public virtual ICollection<Lecture> Lectures { get; set; }

        public Teacher()
        {
            this.Lectures = new List<Lecture>();
        }
    }
}