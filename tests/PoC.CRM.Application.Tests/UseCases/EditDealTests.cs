using NSubstitute;
using PoC.CRM.Application.UseCases.EditDeal;
using PoC.CRM.Data.Repositories;
using PoC.CRM.Domain.Entities;
using PoC.CRM.Domain.Repositories;

namespace PoC.CRM.Application.Tests.UseCases
{
    [TestFixture]
    public class EditDealTests
    {
        private EditDeal _systemUnderTest;
        private IDealRepository _dealRepository;

        [SetUp]
        public void Setup(){
            _dealRepository = new DealRepository();
            _systemUnderTest = new EditDeal(_dealRepository);
        }

        [Test]
        public async Task Execute_MustChangeTitle()
        {
            var input = new EditDealInput(
                Id: 1,
                Title: "New Title",
                Amount: null!,
                StageCode: null!,
                CloseDate: null!,
                LostReason: null!);
            
            var result = await _systemUnderTest.Execute(input);
            var deal = await _dealRepository.GetById(input.Id);

            Assert.That(result.Success, Is.True);
            Assert.That(deal.Title, Is.EqualTo("New Title"));
        }

        [Test]
        public async Task Execute_MustChangeAmount()
        {
            var input = new EditDealInput(
                Id: 1,
                Title: "New Title",
                Amount: 250M,
                StageCode: null!,
                CloseDate: null!,
                LostReason: null!);
            
            var result = await _systemUnderTest.Execute(input);
            var deal = await _dealRepository.GetById(input.Id);

            Assert.That(result.Success, Is.True);
            Assert.That(deal.Amount, Is.EqualTo(250M));
        }

        [Test]
        public async Task Execute_MustChangeStage()
        {
            var input = new EditDealInput(
                Id: 1,
                Title: "Title 1",
                Amount: 250M,
                StageCode: (int)DealStage.Proposal,
                CloseDate: null!,
                LostReason: null!);
            
            var result = await _systemUnderTest.Execute(input);
            var deal = await _dealRepository.GetById(input.Id);

            Assert.That(result.Success, Is.True);
            Assert.That(deal.Stage, Is.EqualTo(DealStage.Proposal));
        }

        [Test]
        public async Task Execute_MustChangeCloseDate()
        {
            var input = new EditDealInput(
                Id: 1,
                Title: "Title 1",
                Amount: 250M,
                StageCode: (int)DealStage.Proposal,
                CloseDate: new DateTime(2025,1,1),
                LostReason: null!);
            
            var result = await _systemUnderTest.Execute(input);
            var deal = await _dealRepository.GetById(input.Id);

            Assert.That(result.Success, Is.True);
            Assert.That(deal.ClosingDate, Is.EqualTo(new DateTime(2025,1,1)));
        }

        [Test]
        public async Task Execute_MustChangeLostReason()
        {
            var input = new EditDealInput(
                Id: 1,
                Title: "Title 1",
                Amount: 250M,
                StageCode: (int)DealStage.Proposal,
                CloseDate: new DateTime(2025,1,1),
                LostReason: "Foo bar reason.");
            
            var result = await _systemUnderTest.Execute(input);
            var deal = await _dealRepository.GetById(input.Id);

            Assert.That(result.Success, Is.True);
            Assert.That(deal.LostReason, Is.EqualTo("Foo bar reason."));
        }
    }
}