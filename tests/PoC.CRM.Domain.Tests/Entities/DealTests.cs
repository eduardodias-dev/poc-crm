using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Tests.Entities;

public class DealTests
{
    [TestCase("")]
    [TestCase(" ")]
    public void Deal_WhenCreatedWithoutValidCompanyName_MustThrowException(string companyName)
    {
        Assert.That(() => _ = new Deal(1, companyName, "Great Deal", 100m), Throws.ArgumentException);
    }

    [Test]
    public void Deal_WhenCreatedWithoutValidCompanyName_MustThrowException()
    {
        Assert.That(() => _ = new Deal(1, null!, "Great Deal", 100m), Throws.ArgumentNullException);
    }

    [Test]
    public void Deal_WhenCreatedWithValidCompanyName_MustSetCompanyName()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        Assert.That(deal.CompanyName, Is.EqualTo("Valid Company"));
    }

    [Test]
    public void Deal_WhenCreatedWithValidTitle_MustSetTitle()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        Assert.That(deal.Title, Is.EqualTo("Great Deal"));
    }

    [TestCase("")]
    [TestCase(" ")]
    public void Deal_WhenCreatedWithoutValidTitle_MustThrowException(string title)
    {
        Assert.That(() => _ = new Deal(1, "Some Company", title, 100m), Throws.ArgumentException);
    }

    [Test]
    public void Deal_WhenCreatedWithoutValidTitle_MustThrowException()
    {
        Assert.That(() => _ = new Deal(1, "Some Company", null!, 100m), Throws.ArgumentNullException);
    }

    [Test]
    public void Deal_WhenCreatedWithNegativeAmount_MustThrowException()
    {
        Assert.That(() => _ = new Deal(1, "Some Company", "Great Deal", -100m), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    public void Deal_WhenCreated_MustBeAtProspectStage()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        Assert.That(deal.Stage, Is.EqualTo(DealStage.NotInitiated));
    }

    [Test]
    public void Deal_WhenWinningDealWithoutClosingDate_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.MoveStage(DealStage.Proposal);
        Assert.That(() => deal.WinDeal(), Throws.InvalidOperationException.With.Message.EqualTo("Closing date must be set before marking the deal as Won."));
    }

    [Test]
    public void Deal_WhenWinningDealWithZeroAmount_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 0);
        deal.SetClosingDate(DateTime.Today.AddDays(1));
        deal.MoveStage(DealStage.Proposal);
        Assert.That(() => deal.WinDeal(), Throws.InvalidOperationException.With.Message.EqualTo("Amount must be greater than zero to mark the deal as Won."));
    }

    [Test]
    public void Deal_WhenWinningDealAtProspectStageWithValidClosingDateAndAmount_MustSetStageToWon()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.SetClosingDate(DateTime.Today.AddDays(1));
        deal.MoveStage(DealStage.Prospect);
        deal.WinDeal();
        Assert.That(deal.Stage, Is.EqualTo(DealStage.Won));
    }

    [Test]
    public void Deal_WhenWinningDealNotAtProspectStage_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.SetClosingDate(DateTime.Today.AddDays(1));
        deal.MoveStage(DealStage.Prospect);
        deal.WinDeal();
        Assert.That(() => deal.WinDeal(), Throws.InvalidOperationException.With.Message.EqualTo("Only deals in Prospect or Proposal stages can be marked as Won."));
    }

    [Test]
    public void Deal_WhenLosingDealNotAtProspectStage_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.SetClosingDate(DateTime.Today.AddDays(1));
        deal.MoveStage(DealStage.Prospect);
        deal.WinDeal();
        Assert.That(() => deal.LoseDeal("Lost to competitor"), Throws.InvalidOperationException.With.Message.EqualTo("Only deals in Prospect stage can be marked as Lost."));
    }

    [Test]
    public void Deal_WhenLosingDealAtProspectStageWithoutReason_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.MoveStage(DealStage.Prospect);
        Assert.That(() => deal.LoseDeal(""), Throws.ArgumentException.With.Message.EqualTo("Reason for losing the deal must be provided. (Parameter 'reason')"));
    }

    [Test]
    public void Deal_WhenLosingDealAtProspectStageWithNullReason_MustThrowException()
    {
        var deal = new Deal(1, "Valid Company", "Great Deal", 100m);
        deal.MoveStage(DealStage.Prospect);
        Assert.That(() => deal.LoseDeal(null!), Throws.ArgumentException.With.Message.EqualTo("Reason for losing the deal must be provided. (Parameter 'reason')"));
    }
}
