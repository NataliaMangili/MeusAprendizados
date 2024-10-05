using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdopt.Domain.AggregatesModels;

public abstract class BaseModel
{
    protected BaseModel()
    {
        Id = Guid.NewGuid(); // Inicializa o Id com um novo Guid
    }

    public bool IsTransient()
    {
        return this.Id == default;
    }

    [Key, Column(TypeName = "UniqueIdentifier")]
    public Guid Id { get; set; }

    public bool Excluded { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? Alteration { get; set; } = DateTime.Now;

    [Column(TypeName = "datetime2")]
    public DateTime? Inclusion { get; set; }

    public virtual Nullable<Guid> UserAlteration { get; set; }
    public virtual Nullable<Guid> UserInclusion { get; set; }
}
