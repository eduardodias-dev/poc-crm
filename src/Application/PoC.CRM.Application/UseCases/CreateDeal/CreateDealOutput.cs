using System;

namespace PoC.CRM.Application.UseCases.CreateDeal;

public record CreateDealOutput(string DealCode, string DealStage, decimal ClosingProbability, DateTime ExpectedCloseDate);
