using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Common.Database;

[Index("Date", Name = "IX_Sales_Date")]
public partial class Sale
{
    [Key]
    [Column("ID")]
    public long Id { get; set; }

    [Column("CarID")]
    public Guid CarId { get; set; }

    public DateOnly Date { get; set; }

    [ForeignKey("CarId")]
    [InverseProperty("Sales")]
    public virtual Car Car { get; set; } = null!;
}
