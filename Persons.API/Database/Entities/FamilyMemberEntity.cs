using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Persons.API.Database.Entities.Common;

namespace Persons.API.Database.Entities
    //5 de marzo
{
    [Table("familyGroup")]
    public class FamilyMemberEntity 
        //No vamos a usar el baseentity ya que sera un agrego que se usara al crear 
    {
      
        [Column("id")]
        [Key]
        public string Id { get; set; }

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }


        [Column("relation_ship")]
        [Required]
        public string RelationShip { get; set; } //Seria lo bueno tener otra tabla donde genere lo que necesitamos

        //Relacion con la tabla personas 
        [Column("person_id")]
        [Required]
        public string PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual PersonEntity Person { get; set; }
    }
}
