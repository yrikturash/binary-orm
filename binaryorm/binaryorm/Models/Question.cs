using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace binaryorm.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId { get; set; } 
        public Category Category { get; set; } 
        public string Text { get; set; }
        public virtual ICollection<Test> Tests { get; set; }

    }
}