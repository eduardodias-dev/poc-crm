using NSubstitute;
using PoC.CRM.Application.UseCases.CreateDeal;
using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Application.Tests.UseCases
{
    public class CreateDealTests
    {
        private ICompanyRepository _companyRepository;
        private IDealRepository _dealRepository;
        [SetUp]
        public void Setup()
        {
            _companyRepository = Substitute.For<ICompanyRepository>();
            _dealRepository = Substitute.For<IDealRepository>();
        }

        [Test]
        public async Task Execute_WhenCalled_MustReturnDealInFirstStage()
        {
            _companyRepository.GetByName("Company1").Returns(new Company("Company1", "example.com"));
            var createDeal = new CreateDeal(_companyRepository, _dealRepository);
            var input = new CreateDealInput(1, "Company1", "First Deal", 100m, DateTime.Today.AddDays(30));
            var result = await createDeal.Execute(input);
            Assert.That(result.DealStage, Is.EqualTo("Prospect"));
        }

        [Test]
        public void Execute_WhenCalledWithInvalidAmount_MustThrowException()
        {
            _companyRepository.GetByName("Company1").Returns(new Company("Company1", "example.com"));
            var createDeal = new CreateDeal(_companyRepository, _dealRepository);
            var input = new CreateDealInput(1, "Company1", "First Deal", -10m, DateTime.Today.AddDays(30));
            
            Assert.That(async () => _ = await createDeal.Execute(input), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}