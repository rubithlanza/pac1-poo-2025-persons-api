using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persons.API.Database.Entities.Common
{
    public class BaseEntity 
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }
        [Column("created_by")]

        public string CraetedBy { get; set; }

        [Column("created_date")]
        public string CraetedDate { get; set; }

        [Column("updated_by")]
        public string UpdatedBy { get; set; }

        [Column("updated_date")]
        public string UpdatedDate { get; set; }
    }
}
