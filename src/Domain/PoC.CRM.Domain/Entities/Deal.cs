using System;

namespace PoC.CRM.Domain.Entities;

public class Deal : IActivityObject
{
    public int Id { get; }
    public string Title { get; }
    public decimal Amount { get; }
    public string CompanyName { get; }
    public DealStage Stage { get; private set; }
    public DateTime? ClosingDate { get; private set; }
    public string? LostReason { get; private set; }
    private DealCode Code { get; }
    public string Name => Title;

    public Deal(int id, string companyName, string title, decimal amount, DateTime date = new DateTime(), int sequenceNumber = 0)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative.");
        Id = id;
        CompanyName = companyName;
        Title = title;
        Amount = amount;
        Code = new DealCode(date, sequenceNumber);
        Stage = DealStage.NotInitiated;
    }

    public void SetClosingDate(DateTime date, DateTime today = default){
        today = today == default ? DateTime.Today : today;
        if (ClosingDate < today)
            throw new InvalidOperationException("Closing date cannot be in the past.");

        ClosingDate = date;
    }

    public void WinDeal()
    {
        if (Stage != DealStage.Prospect && Stage != DealStage.Proposal)
            throw new InvalidOperationException("Only deals in Prospect or Proposal stages can be marked as Won.");

        if (ClosingDate == null)
            throw new InvalidOperationException("Closing date must be set before marking the deal as Won.");
        
        if (Amount == 0)
            throw new InvalidOperationException("Amount must be greater than zero to mark the deal as Won.");

        Stage = DealStage.Won;
    }

    public void LoseDeal(string reason)
    {
        if (Stage != DealStage.Prospect && Stage != DealStage.Proposal)
            throw new InvalidOperationException("Only deals in Prospect stage can be marked as Lost.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason for losing the deal must be provided.", nameof(reason));

        LostReason = reason;
        Stage = DealStage.Lost;
    }

    public void MoveStage(DealStage newStage)
    {
        if (newStage == DealStage.Won || newStage == DealStage.Lost)
            throw new InvalidOperationException("Use WinDeal or LoseDeal methods to change the deal to Won or Lost.");

        if (newStage == Stage)
            return; // No change

        if (newStage == DealStage.NotInitiated)
            throw new InvalidOperationException("Cannot move back to NotInitiated stage.");

        if (newStage == DealStage.Prospect && Stage != DealStage.NotInitiated)
            throw new InvalidOperationException("Can only move to Prospect from NotInitiated stage.");

        Stage = newStage;
    }

    public decimal GetClosingProbability()
    {
        return Stage switch
        {
            DealStage.Prospect => 0.1m,
            DealStage.Proposal => 0.5m,
            DealStage.Lost => 0m,
            DealStage.Won => 1m,
            _ => 0m
        };
    }

    public string GetCode() => Code.Value;

    internal Deal(int id, string companyName, DealStage stage, string title, decimal amount, 
     DealCode code, DateTime? closingDate, string lostReason){
        Id = id;
        CompanyName = companyName;
        Stage = stage;
        Title = title;
        Amount = amount;
        Code = code;
        ClosingDate = closingDate;
        LostReason = lostReason;
     }
}

public class DealCode
{
    public string Value { get; }

    public DealCode(DateTime date, int sequenceNumber)
    {
        Value = Generate(date, sequenceNumber);
    }

    private string Generate(DateTime date, int sequenceNumber)
    {
        return string.Format("{0:yyyy}{1:D8}", date, sequenceNumber);
    }

    internal DealCode(string value){
        Value = value;
    }
}