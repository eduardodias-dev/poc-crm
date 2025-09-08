using PoC.CRM.Domain.Entities;

namespace PoC.CRM.Domain.Tests.Entities
{
    public class ActivityTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Activity_WhenCreatedWithoutReferenceObject_MustThrowNullException()
        {
            Assert.That(() => new Activity(null!, DateTime.Today), Throws.ArgumentNullException);
        }

        [Test]
        public void Activity_WhenCreatedWithValidReferenceObject_MustAssignReference()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            Assert.That(activity.ReferenceObject, Is.EqualTo(company));
        }

        [Test]
        public void Activity_WhenCreated_MustHaveDueDate()
        {
            var dueDate = DateTime.Today;
            var activity = new Activity(new Company("Test Company", "test.com"), dueDate);
            Assert.That(activity.DueDate, Is.EqualTo(dueDate));
        }

        [Test]
        public void Activity_WhenCreated_MusthaveStatus()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.Pending));
        }

        [Test]
        public void Cancel_WhenCalled_MustChangeStatusToCanceled()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            activity.Cancel("Cancelled due to client request.");
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.Canceled));
        }

        [Test]
        public void Cancel_WhenCalledANotPendingActivity_MustThrowException()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            activity.Cancel("Cancelled due to client request.");
            Assert.That(() => activity.Cancel("Another cancellation reason."), Throws.InvalidOperationException);
        }

        [Test]
        public void Cancel_WhenCalled_MustSetCancelReason()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            activity.Cancel("Cancelled due to client request.");
            Assert.That(activity.CancelReason, Is.EqualTo("Cancelled due to client request."));
        }

        [Test]
        public void Complete_WhenCalledWithEmptyReason_MustThrowException()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            Assert.That(() => activity.Complete(""), Throws.ArgumentException);
        }

        [Test]
        public void Complete_WhenCalledProperly_MustSetResult()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            activity.Complete("Completed successfully.");
            Assert.That(activity.Result, Is.EqualTo("Completed successfully."));
        }

        [Test]
        public void Complete_WhenCalledProperly_MustSetStatusToCompleted()
        {
            var company = new Company("Test Company", "test.com");
            var activity = new Activity(company, DateTime.Today);
            activity.Complete("Completed successfully.");
            Assert.That(activity.Status, Is.EqualTo(ActivityStatus.Completed));
        }
    }
}