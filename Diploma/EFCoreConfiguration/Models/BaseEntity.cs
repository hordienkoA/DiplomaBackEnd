using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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
