using System.ComponentModel.DataAnnotations.Schema;
using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class File:BaseEntity
{
    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
    
    
}