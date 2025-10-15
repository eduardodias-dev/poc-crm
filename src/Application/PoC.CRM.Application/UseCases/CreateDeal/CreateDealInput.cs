using System;

namespace PoC.CRM.Application.UseCases.CreateDeal;

public record CreateDealInput(int stageCode, string CompanyName, string Title, decimal Amount, DateTime CloseDate, DateTime CreatedAt);
