using PetAdopt.Domain.ValueObjects;

namespace PetAdopt.Domain.AggregatesModels.AdoptionAggregate;

public class Adoption : ModelBase
{
    public string Name { get; set; }
    public string Cpf { get; set; }


}