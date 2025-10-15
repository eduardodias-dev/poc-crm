using System;
using System.Data.Common;
using Dapper;
using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Data.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DbConnection _dbConnection = Connection.GetInstance();
    
    public async Task<Company> GetByName(string name)
    {
        var data = await _dbConnection.QueryAsync("SELECT * FROM company WHERE name = @Name", new { Name = name });
        if (!data.Any())
        {
            return null!;
        }

        var first = data.First();
        return new Company(first.name, first.domain);
    }
}
