using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Repositories;

public interface IDealRepository
{
    Task Add(Deal deal);
    Task<int> GetNextSequenceNumber();
    Task<Deal> GetById(int id);
    Task Update(Deal deal);
}
