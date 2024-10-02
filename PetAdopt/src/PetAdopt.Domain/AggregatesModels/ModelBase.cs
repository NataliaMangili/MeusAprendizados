using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdopt.Domain.AggregatesModels;

public abstract class ModelBase
{
    protected ModelBase()
    {
        Id = Guid.NewGuid(); // Inicializa o Id com um novo Guid
    }

    public bool IsTransient()
    {
        return this.Id == default;
    }

    [Key, Column(TypeName = "UniqueIdentifier")]
    public Guid Id { get; set; }

    public bool Excluido { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime? Alteracao { get; set; } = DateTime.Now;

    [Column(TypeName = "datetime2")]
    public DateTime? Inclusao { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string? CodigoExterno { get; set; }

    public virtual Nullable<Guid> UsuarioAlteracao { get; set; }
    public virtual Nullable<Guid> UsuarioInclusao { get; set; }
}
