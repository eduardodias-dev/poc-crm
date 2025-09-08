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

    public Deal(int id, string companyName, string title, decimal amount)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(companyName, nameof(companyName));
        ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be non-negative.");
        Id = id;
        CompanyName = companyName;
        Title = title;
        Amount = amount;

        Stage = DealStage.Prospect;
    }

    public void SetClosingDate(DateTime date, DateTime today = default){
        today = today == default ? DateTime.Today : today;
        if (ClosingDate < today)
            throw new InvalidOperationException("Closing date cannot be in the past.");

        ClosingDate = date;
    }

    public void WinDeal()
    {
        if (Stage != DealStage.Prospect)
            throw new InvalidOperationException("Only deals in Prospect stage can be marked as Won.");

        if (ClosingDate == null)
            throw new InvalidOperationException("Closing date must be set before marking the deal as Won.");
        
        if (Amount == 0)
            throw new InvalidOperationException("Amount must be greater than zero to mark the deal as Won.");

        Stage = DealStage.Won;
    }

    public void LoseDeal(string reason)
    {
        if (Stage != DealStage.Prospect)
            throw new InvalidOperationException("Only deals in Prospect stage can be marked as Lost.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Reason for losing the deal must be provided.", nameof(reason));

        LostReason = reason;
        Stage = DealStage.Lost;
    }

    public decimal GetClosingProbability()
    {
        return Stage switch
        {
            DealStage.Proposal => 0.1m,
            DealStage.Lost => 0m,
            DealStage.Won => 1m,
            _ => 0m
        };
    }
}
