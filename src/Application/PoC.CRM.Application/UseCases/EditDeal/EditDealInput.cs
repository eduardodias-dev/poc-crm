namespace PoC.CRM.Application.UseCases.EditDeal;
public record EditDealInput(int Id, string? Title, decimal? Amount, int? StageCode, DateTime? CloseDate, string? LostReason);

