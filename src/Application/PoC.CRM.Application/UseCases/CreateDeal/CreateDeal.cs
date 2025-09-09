using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Application.UseCases.CreateDeal;

public class CreateDeal(ICompanyRepository companyRepository, IDealRepository dealRepository)
{
    public async Task<CreateDealOutput> Execute(CreateDealInput input)
    {
        var company = await companyRepository.GetByName(input.CompanyName);
        if (company == null)
        {
            throw new ApplicationException("Company not found");
        }

        var deal = new Deal(0, input.CompanyName, input.Title, input.Amount);
        deal.SetClosingDate(input.CloseDate);
        deal.MoveStage((DealStage)input.stageCode);

        await dealRepository.Add(deal);
        return new CreateDealOutput(deal.Stage.ToString(), deal.GetClosingProbability(), deal.ClosingDate.GetValueOrDefault());
    } 
}