using System.ComponentModel.DataAnnotations.Schema;
using Udemy_RestWithASP_NET5.Model.Base;

namespace Udemy_RestWithASP_NET5.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {        
        [Column("id")]
        public long Id { get; set; }
        [Column("first_name")]
        public string FirstName { get; set; }
        [Column("last_name")]
        public string LastName { get; set; }
        [Column("address")]
        public string Address { get; set; }
        [Column("gender")]
        public string Gender { get; set; }

    }

}
