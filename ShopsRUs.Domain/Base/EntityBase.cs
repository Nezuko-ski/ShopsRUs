using System.ComponentModel.DataAnnotations.Schema;

namespace ShopsRUs.Domain.Interface
{
    public class EntityBase
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
