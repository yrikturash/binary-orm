using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace binaryorm.Models
{
    public class TestWork
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TestWorkId { get; set; } 
        public User User { get; set; } 
        public int Result { get; set; } 
        public int PassTime { get; set; }


        public virtual Test Test { get; set; }
    }
}