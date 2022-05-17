using FluentAssertions;
using InvoiceMicroservice;
using MassTransit.Testing;
using MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Producer.Test {
    public class InvoiceToCreateCommandConsumesMessages {
        [Fact]
        public async Task VerifyInvoiceToCreateCommandConsumed() {
            var harness = new InMemoryTestHarness();
            var consumerHarness = harness.Consumer<EventConsumer>();

            await harness.Start();

            try {
                await harness.InputQueueSendEndpoint.Send<IInvoiceToCreate>(
                    new {
                        CustomerNumber = 19282,
                        InvoiceItems = new List<InvoiceItems> {
                            new InvoiceItems {
                                Price = Math.Round(1020.99),
                                ActualMileage = 40,
                                BaseRate = 12.50,
                                IsHazardousMaterial = false,
                                IsOversized = true,
                                IsRefrigerated = false
                            }
                        }
                    });

                Assert.True(await harness.Consumed.Any<IInvoiceToCreate>());

                Assert.True(await consumerHarness.Consumed.Any<IInvoiceToCreate>());

                harness.Published.Select<IInvoiceCreated>().Count().Should().Be(1);
            } finally {
                await harness.Stop();
            }
        }
    }
}
