using System;

namespace PoC.CRM.Domain.Entities;

public interface IContactValidator
{
    public Task<bool> EmailExists(string email);
}
