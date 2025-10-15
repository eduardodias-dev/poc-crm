using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Application.UseCases.CreateDeal;

public class CreateDeal(ICompanyRepository companyRepository, IDealRepository dealRepository)
{
    public async Task<CreateDealOutput> Execute(CreateDealInput input)
    {
        var sequenceNumber = await dealRepository.GetNextSequenceNumber();
        var company = await companyRepository.GetByName(input.CompanyName);
        if (company == null)
        {
            throw new ApplicationException("Company not found");
        }

        var deal = new Deal(default, input.CompanyName, input.Title, input.Amount, input.CreatedAt, sequenceNumber);
        deal.SetClosingDate(input.CloseDate);
        deal.MoveStage((DealStage)input.stageCode);

        await dealRepository.Add(deal);
        return new CreateDealOutput(deal.GetCode(), deal.Stage.ToString(), deal.GetClosingProbability(), deal.ClosingDate.GetValueOrDefault());
    } 
}