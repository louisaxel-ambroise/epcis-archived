using FasTnT.Data.Mappings.Events;
using FasTnT.Domain.Model.Events;
using NUnit.Framework;
using System;

namespace FasTnT.Data.Tests.MappingTests
{
    [TestFixture]
    public class EpcisRequestMapTests : InMemoryDatabaseTest
    {
        public EpcisRequestMapTests() : base(typeof(EpcisRequestMap).Assembly)
        { }

        [Test]
        public void CanSaveAndLoadEpcisRequest()
        {
            Guid id;

            using (var tx = session.BeginTransaction())
            {
                var request = new EpcisRequest
                {
                    DocumentTime = new DateTime(2017, 11, 11, 12, 38, 00),
                    RecordTime = new DateTime(2017, 11, 11, 12, 42, 00)
                };
                request.AddEvent(new EpcisEvent
                {
                    EventType = EventType.ObjectEvent,
                    Action = EventAction.Add,
                    EventTime = new DateTime(2017, 10, 10, 10, 10, 00),
                    EventTimezoneOffset = new TimeZoneOffset{ Representation = "+01:00"}
                });

                id = (Guid)session.Save(request);
                tx.Commit();
            }

            session.Clear();

            using (var tx = session.BeginTransaction())
            {
                var request = session.Get<EpcisRequest>(id);

                Assert.AreEqual(request.DocumentTime, new DateTime(2017, 11, 11, 12, 38, 00));
                Assert.AreEqual(request.RecordTime, new DateTime(2017, 11, 11, 12, 42, 00));
                Assert.AreEqual(request.Events.Count, 1);

                tx.Commit();
            }
        }
    }
}
