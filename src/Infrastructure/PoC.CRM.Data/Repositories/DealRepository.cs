using System.Data.Common;
using Dapper;
using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Mappers;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Data.Repositories;

public class DealRepository : IDealRepository
{
    private DbConnection _dbConnection;
    public DealRepository()
    {
        _dbConnection = Connection.GetInstance();
    }

    public async Task Add(Deal deal)
    {
        var affectedRows = await _dbConnection.ExecuteAsync(@"INSERT INTO deal (code, title, amount, company_name, stage) 
        VALUES (@Code, @Title, @Amount, @CompanyName, @Stage)", new {
            Code = deal.GetCode(),
            deal.Title,
            deal.Amount,
            deal.CompanyName,
            deal.Stage
        });

        if (affectedRows == 0)
        {
            throw new Exception("Failed to insert deal");
        }
    }

    public async Task<Deal> GetById(int id)
    {
        var dealDB = await _dbConnection.QueryAsync("select * from deal where id = @id", new{
            id
        });
        if(!dealDB.Any()) return null!;
        return DealMapper.MapFromDB(dealDB.FirstOrDefault());
    }

    public async Task<int> GetNextSequenceNumber()
    {
        return await _dbConnection.ExecuteScalarAsync<int>("SELECT count(0) + 1 FROM deal");
    }

    public async Task Update(Deal deal)
    {
        var affectedRows = await _dbConnection.ExecuteAsync(@"
            UPDATE deal set 
                title = @title, 
                amount = @amount, 
                stage = @stage,
                closing_date = @closing_date,
                lost_reason = @lost_reason
            where id = @id", 
            new {
                id = deal.Id,
                title = deal.Title,
                amount = deal.Amount,
                stage = deal.Stage,
                closing_date = deal.ClosingDate,
                lost_reason = deal.LostReason
                });

        if (affectedRows == 0)
        {
            throw new Exception("Failed to update deal");
        }
    }
}
