using System;
using System.ComponentModel.DataAnnotations;

namespace BlogSystem.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public byte IsRemoved { get; set; } = 0x0;
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
    }
}