using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Persons.API.Database.Entities.Common;

namespace Persons.API.Database.Entities
{
    [Table("persons")]
    public class PersonEntity : BaseEntity
    {
        //[Key]
        //[Column("id")]
        //public Guid Id { get; set; }

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }


        [Column("dni")]
        [Required]
        public string DNI { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        //Para crear un nuevo campo
        //[Column("created_by")]

        //public string CraetedBy { get; set; }

        //[Column("created_date")]
        //public string CraetedDate{ get; set; }

        //[Column("updated_by")]
        //public string UpdatedBy { get; set; }

        //[Column("updated_date")]
        //public string UpdatedDate { get; set; }

        //20 de febrero

        [Column("country_id")]
        public string CountryId { get; set; } //Para que se nula el signo de interrogacion 

        //Para crear la relacion de ambas en propiedad de navegacion 

        [ForeignKey(nameof(CountryId))]
        public virtual CountryEntity Country { get; set; }

        //5 de marzo
        public virtual IEnumerable<FamilyMemberEntity> FamilyGroup { get; set; }
    }
}
