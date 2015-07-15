using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binaryorm.Models
{

    [Table("Tests")]
    public class Test
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TestId { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public int MaxPassTime { get; set; }
        public int PassMark { get; set; }


        public virtual ICollection<TestWork> TestWorks { get; set; }
    }
}
