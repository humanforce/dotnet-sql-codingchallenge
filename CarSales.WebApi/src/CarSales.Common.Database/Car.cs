using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Common.Database;

[Table("Car")]
public partial class Car
{
    [Key]
    [Column("ID")]
    public Guid Id { get; set; }

    [StringLength(128)]
    public string Name { get; set; } = null!;

    [StringLength(64)]
    public string Colour { get; set; } = null!;

    [Column(TypeName = "decimal(14, 2)")]
    public decimal Price { get; set; }

    [InverseProperty("Car")]
    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
