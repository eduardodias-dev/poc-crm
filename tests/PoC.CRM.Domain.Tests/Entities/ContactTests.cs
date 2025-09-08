using NSubstitute;
using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Tests.Entities;

public class ContactTests
{
    private IContactValidator _contactValidator;

    [SetUp]
    public void Setup()
    {
        _contactValidator = Substitute.For<IContactValidator>();
    }

    [TestCase(" ")]
    [TestCase("")]
    public void Contact_WhenCreatedWithEmptyCompanyName_MustThrowException(string companyName)
    {
        Assert.That(() => _ = new Contact(1, "mycontact@example.com", companyName, _contactValidator), Throws.ArgumentException);
    }

    [Test]
    public void Contact_WhenCreatedWithNullCompanyName_MustThrowException()
    {
        Assert.That(() => _ = new Contact(1, "mycontact@example.com", null!, _contactValidator), Throws.ArgumentException);
    }

    [Test]
    public void Contact_WhenCreatedWithValidCompanyName_MustSetCompanyName()
    {
        var company = new Contact(1, "mycontact@example.com", "Valid Company", _contactValidator);
        Assert.That(company.CompanyName, Is.EqualTo("Valid Company"));
    }

    [Test]
    public void Contact_WhenCreatedWithDuplicatedEmail_MustThrowInvalidOperationException()
    {
        var email = "test@example.com";
        _contactValidator.EmailExists(email).Returns(true);
        Assert.That(() => _ = new Contact(1, email, "Valid Company", _contactValidator), Throws.ArgumentException);
    }

    [Test]
    public void Contact_WhenCreatedWithValidEmail_MustSetEmail()
    {
        var email = "test@example.com";
        var contact = new Contact(1, email, "Valid Company", _contactValidator);
        Assert.That(contact.Email, Is.EqualTo(email));
    }
}
