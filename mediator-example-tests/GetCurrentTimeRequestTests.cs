using System;
using System.Threading.Tasks;
using MediatorExample.Mediator.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mediator_example_tests {
    [TestClass]
    public class GetCurrentTimeRequestTests {

        [TestMethod]
        public async Task CurrentTime_ShouldBeLaterThanBaseline() {
            var handler = new GetCurrentTimeHandler();

            var baseline = DateTime.Now;
            var currentTime = await handler.Handle(new GetCurrentTimeRequest(), default).ConfigureAwait(false);
            Assert.IsTrue(
                currentTime > baseline, 
                "Current time from handler should be greater than baseline."
            );
        }


        [TestMethod]
        public async Task CurrentTime_ShouldBeEarlierThanPreviousMeasurement() {
            var handler = new GetCurrentTimeHandler();

            var baseline = DateTime.Now.AddSeconds(10);
            var currentTime = await handler.Handle(new GetCurrentTimeRequest(), default).ConfigureAwait(false);
            Assert.IsTrue(
                currentTime < baseline,
                "Current time from handler should be earlier than baseline."
            );
        }

    }
}
