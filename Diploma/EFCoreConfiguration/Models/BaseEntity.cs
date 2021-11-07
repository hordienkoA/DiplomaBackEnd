using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConfiguration.Models
{
    public class BaseEntity
    {

    }
    public class BaseEntity<T> : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; set; }
    }
}
