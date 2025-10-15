using NSubstitute;
using PoC.CRM.Application.UseCases.CreateDeal;
using PoC.CRM.Data.Repositories;
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
            _companyRepository = new CompanyRepository();
            _dealRepository = new DealRepository();
        }

        [Test]
        public async Task Execute_WhenCalled_MustReturnDealInFirstStage()
        {
            var createDeal = new CreateDeal(_companyRepository, _dealRepository);
            var input = new CreateDealInput(1, "EduSmart", "First Deal", 100m, DateTime.Today.AddDays(30), new DateTime());
            var result = await createDeal.Execute(input);
            Assert.That(result.DealStage, Is.EqualTo("Prospect"));
        }

        [Test]
        public void Execute_WhenCalledWithInvalidAmount_MustThrowException()
        {
            var createDeal = new CreateDeal(_companyRepository, _dealRepository);
            var input = new CreateDealInput(1, "EduSmart", "First Deal", -10m, DateTime.Today.AddDays(30), new DateTime());
            
            Assert.That(async () => _ = await createDeal.Execute(input), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public async Task Execute_WhenCalled_MustCreateDealWithCode()
        {
            _dealRepository = Substitute.For<IDealRepository>();
            _dealRepository.GetNextSequenceNumber().Returns(1);

            var createDeal = new CreateDeal(_companyRepository, _dealRepository);
            var input = new CreateDealInput(1, "EduSmart", "First Deal", 100m, DateTime.Today.AddDays(30), new DateTime(2022, 1, 1));
            var result = await createDeal.Execute(input);
            Assert.That(result.DealCode, Is.EqualTo("202200000001"));
        }
    }
}