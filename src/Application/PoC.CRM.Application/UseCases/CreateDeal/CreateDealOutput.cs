using System;

namespace PoC.CRM.Application.UseCases.CreateDeal;

public record CreateDealOutput(string DealStage, decimal ClosingProbability, DateTime ExpectedCloseDate);
