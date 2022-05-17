using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using MessageContracts;
using PaymentMicroservice;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Consumer.Test {
    public class InvoiceCreatedConsumerConsumesMessages {
        [Fact]
        public async Task VerifyInvoiceCreatedMessageConsumed() {
            var harness = new InMemoryTestHarness();

            var consumerHarness = harness.Consumer<InvoiceCreatedConsumer>();
            await harness.Start();

            try {
                await harness.Bus.Publish<IInvoiceCreated>(
                    new { 
                        InvoiceCreated = InVar.Id 
                    }
                );

                Assert.True(await harness.Consumed.Any<IInvoiceCreated>());

                Assert.True(await consumerHarness.Consumed.Any<IInvoiceCreated>());

                harness.Published.Select<IInvoiceCreated>().Count().Should().Be(1);
            } finally {
                await harness.Stop();
            }
        }
    }
}
