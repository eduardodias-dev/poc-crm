using System;

namespace PoC.CRM.Domain.Entities;

public class Contact : IActivityObject
{
    public int Id { get; }
    public string Email { get; }
    public string CompanyName { get; }
    public string Name => Email;
    
    public Contact(int id, string email, string companyName, IContactValidator contactValidator)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email, nameof(email));
        if (string.IsNullOrWhiteSpace(companyName))
            throw new ArgumentException("Contact must be linked to a company.");
        if (contactValidator != null && contactValidator.EmailExists(email).Result)
            throw new ArgumentException("Email already exists.", nameof(email));
        CompanyName = companyName;
        Id = id;
        Email = email;
    }
}