using System;

namespace PoC.CRM.Domain.Entities;

public class Activity
{
    public IActivityObject ReferenceObject { get; }
    public DateTime DueDate { get; }
    public ActivityStatus Status { get; private set; }
    public string? CancelReason { get; private set; }
    public string? Result { get; private set; }
    public Activity(IActivityObject referenceObject, DateTime dueDate)
    {
        ReferenceObject = referenceObject ?? throw new ArgumentNullException(nameof(referenceObject));
        DueDate = dueDate;

        Status = ActivityStatus.Pending;
    }

    public void Cancel(string reason)
    {
        if (Status != ActivityStatus.Pending)
            throw new InvalidOperationException("Only pending activities can be canceled.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Cancellation reason must be provided.", nameof(reason));
        
        Status = ActivityStatus.Canceled;
        CancelReason = reason;
    }

    public void Complete(string result)
    {
        if (string.IsNullOrWhiteSpace(result))
            throw new ArgumentException("Activity completion result must be provided.", nameof(result));

        Result = result;
        Status = ActivityStatus.Completed;
    }
}
