using System;
using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Repositories;

public interface ICompanyRepository
{
    Task<Company> GetByName(string name);
}
