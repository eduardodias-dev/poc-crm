using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Repositories;

public interface IDealRepository
{
    Task Add(Deal deal);
}
