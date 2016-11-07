using System.ComponentModel.DataAnnotations.Schema;

namespace Giffy.Entities.Models
{
    public abstract class Entity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
