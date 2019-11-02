using System;

namespace BookLibrary.DAL.Models.Entities.Abstraction
{
    public interface IAuditableEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}