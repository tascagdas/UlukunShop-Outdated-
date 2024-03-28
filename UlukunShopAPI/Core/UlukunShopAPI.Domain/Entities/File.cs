using System.ComponentModel.DataAnnotations.Schema;
using UlukunShopAPI.Domain.Entities.Common;

namespace UlukunShopAPI.Domain.Entities;

public class File:BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string Storage { get; set; }
    [NotMapped]
    public override DateTime UpdatedDate { get; set; }
    
    
}