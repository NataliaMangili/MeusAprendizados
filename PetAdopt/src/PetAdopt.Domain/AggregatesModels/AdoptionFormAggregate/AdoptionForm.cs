﻿namespace PetAdopt.Domain.AggregatesModels.AdoptionAggregate;

public class AdoptionForm : BaseModel
{
    public AdoptionForm(Guid petId, Guid adopterId, AddressVO addressVO, string feedback, string adopterContact, string reasonForAdoption, int adopterHouseholdSize, bool adopterHasOtherPets, StatusFormEnum formStatus)
    {
        PetId = petId;
        AdopterId = adopterId;
        this.addressVO = addressVO;
        Feedback = feedback;
        AdopterContact = adopterContact;
        ReasonForAdoption = reasonForAdoption;
        AdopterHouseholdSize = adopterHouseholdSize;
        AdopterHasOtherPets = adopterHasOtherPets;
        FormStatus = formStatus;
    }

    public Guid PetId { get; private set; }
    public Guid AdopterId { get; private set; }
    public AddressVO addressVO { get; private set; }
    public string Feedback { get; private set; }
    public string AdopterContact { get; private set; }
    public string ReasonForAdoption { get; private set; }
    public int AdopterHouseholdSize { get; private set; }
    public bool AdopterHasOtherPets { get; private set; }
    public StatusFormEnum FormStatus { get; private set; }

    //private readonly List<string> _adoptionFormLogs = new List<string>();
    //public IReadOnlyCollection<string> AdoptionFormLogs => _adoptionFormLogs.AsReadOnly();

    public void ApproveAdoption()
    {
        if (FormStatus != StatusFormEnum.Submitted)
            throw new InvalidOperationException("Adoption form must be submitted to approve.");

        FormStatus = StatusFormEnum.Approved;
        Alteration = DateTime.Now;
        //_adoptionFormLogs.Add("Adoption form approved.");
    }

    public void RejectAdoption(string reason)
    {
        if (FormStatus != StatusFormEnum.Submitted)
            throw new InvalidOperationException("Adoption form must be submitted to reject.");

        FormStatus = StatusFormEnum.Rejected;
        Feedback = reason;
        Alteration = DateTime.Now;
        //_adoptionFormLogs.Add($"Adoption form rejected. Reason: {reason}");
    }

    public void CancelForm()
    {
        if (FormStatus == StatusFormEnum.Approved || FormStatus == StatusFormEnum.Rejected)
            throw new InvalidOperationException("Cannot cancel a form that is already approved or rejected.");

        FormStatus = StatusFormEnum.Canceled;
        Alteration = DateTime.Now;
       //_adoptionFormLogs.Add("Adoption form canceled by adopter.");
    }

    public bool IsValid()
    {
        bool isValid = addressVO != null &&
                   !string.IsNullOrWhiteSpace(AdopterContact) &&
                   !string.IsNullOrWhiteSpace(ReasonForAdoption) &&
                   (AdopterHasOtherPets != null);

        if (!isValid)
            RejectAdoption("Form is automatic invalided due to missing or incorrect necessary information.");

        return isValid;
    }
}