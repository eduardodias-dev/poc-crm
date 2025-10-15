using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using PoC.CRM.Domain.Mappers;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Application.UseCases.EditDeal
{
    public class EditDeal
    {
        private readonly IDealRepository _dealRepository;

        public EditDeal(IDealRepository dealRepository)
        {
            _dealRepository = dealRepository;
        }

        public async Task<EditDealOutput> Execute(EditDealInput input)
        {
            var existingDeal = await _dealRepository.GetById(input.Id);
            if(existingDeal == null) 
                throw new InvalidOperationException("Deal not found");

            dynamic ob = new ExpandoObject();
            ob.id = input.Id;
            ob.company_name = existingDeal.CompanyName;
            ob.stage = input.StageCode ?? (int)existingDeal.Stage;
            ob.title = input.Title ?? existingDeal.Title;
            ob.amount = input.Amount ?? existingDeal.Amount;
            ob.code = existingDeal.GetCode();
            ob.closing_date = input.CloseDate ?? existingDeal.ClosingDate;
            ob.lost_reason = input.LostReason ?? existingDeal.LostReason;
            
            var updateDeal = DealMapper.MapToDB(ob);
            await _dealRepository.Update(updateDeal);

            return new (Success: true);
        }
    }
}