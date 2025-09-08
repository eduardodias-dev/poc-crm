using NSubstitute;
using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Tests.Entities
{
    public class CompanyTests
    {
        private IContactValidator _contactValidator;
        
        [SetUp]
        public void Setup()
        {
            _contactValidator = Substitute.For<IContactValidator>();
            _contactValidator.EmailExists(Arg.Any<string>()).Returns(false);
        }

        [TestCase(" ")]
        [TestCase("")]
        public void Company_WhenCreatedWithoutName_MustThrowArgumentException(string name)
        {
            Assert.That(() => _ = new Company(name), Throws.ArgumentException);
        }

        [Test]
        public void Company_WhenCreatedWithNullName_MustThrowArgumentException()
        {
            Assert.That(() => _ = new Company(null!), Throws.ArgumentNullException);
        }

        [Test]
        public void Company_WhenCreatedWithValidName_MustSetNameProperty()
        {
            var company = new Company("Valid Name");
            Assert.That(company.Name, Is.EqualTo("Valid Name"));
        }

        [Test]
        public void Company_WhenCreatedWithDomainName_MustSetDomainNameProperty()
        {
            var company = new Company("Valid Name", "example.com");
            Assert.That(company.DomainName, Is.EqualTo("example.com"));
        }

        [Test]
        public void AddContact_WhenContactIsNull_MustNotAddContact()
        {
            var company = new Company("Valid Name");
            company.AddContact(null!);
            Assert.That(company.Contacts.Count, Is.EqualTo(0));
        } 

        [Test]
        public void RemoveContact_WhenContactExists_MustRemoveFromContactList()
        {
            var company = new Company("Valid Name");
            company.AddContact(new Contact(1, "email@example.com", company.Name, _contactValidator));
            company.AddContact(new Contact(2, "email@example.com", company.Name, _contactValidator));
            company.RemoveContact(1);
            Assert.That(company.Contacts.Count, Is.EqualTo(1));
            Assert.That(company.Contacts.Any(c => c.Id == 1), Is.False);
        }

        [Test]
        public void RemoveContact_WhenContactDoesntExit_MustDoNothing()
        {
            var company = new Company("Valid Name");
            company.AddContact(new Contact(1, "email@example.com", company.Name, _contactValidator));
            company.AddContact(new Contact(2, "email@example.com", company.Name, _contactValidator));
            company.RemoveContact(3);
            Assert.That(company.Contacts.Count, Is.EqualTo(2));
        }

        [Test]
        public void AddDeal_WhenDealIsNotNull_MustAddDeal()
        {
            var company = new Company("Valid Name");
            var deal = new Deal(1, company.Name, "Test Deal", 1000m);

            company.AddDeal(deal);
            Assert.That(company.Deals.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddDeal_WhenDealIsNull_MustNotAddDeal()
        {
            var company = new Company("Valid Name");
            company.AddDeal(null!);

            Assert.That(company.Deals.Count, Is.EqualTo(0));
        }
    }
}