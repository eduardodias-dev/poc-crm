using System;

namespace PoC.CRM.Domain.Entities;

public class Company : IActivityObject
{
    public string Name { get; }
    public string DomainName { get; }
    public IReadOnlyList<Contact> Contacts => _contacts.AsReadOnly();
    private readonly List<Contact> _contacts = new();
    public IReadOnlyList<Deal> Deals => _deals.AsReadOnly();
    private readonly List<Deal> _deals = new();
    public Company(string name, string domainName = "")
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name, nameof(name));
        Name = name;
        DomainName = domainName;
    }

    public void AddContact(Contact contact){
        if (contact != null)
            _contacts.Add(contact);
    }

    public void RemoveContact(int contactId){
        var contact = _contacts.FirstOrDefault(c => c.Id == contactId);
        if (contact != null)
            _contacts.Remove(contact);
    }

    public void AddDeal(Deal deal){
        if (deal != null)
            _deals.Add(deal);
    }
}
