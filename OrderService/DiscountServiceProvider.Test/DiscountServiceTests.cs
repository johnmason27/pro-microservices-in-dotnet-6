using PactNet;
using PactNet.Infrastructure.Outputters;
using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace DiscountServiceProvider.Test {
    public class DiscountServiceTests : IDisposable {
        private readonly ITestOutputHelper output;
        private bool disposed = false;
        private readonly string serviceUri;

        public DiscountServiceTests(ITestOutputHelper output) {
            this.output = output;
            serviceUri = "http://localhost:5247";
        }

        [Fact]
        public void PactWithOrderServiceShouldBeVerified() {
            var config = new PactVerifierConfig {
                Verbose = true,
                ProviderVersion = "2.0.0",
                CustomHeaders = new Dictionary<string, string> {
                    { "Content-Type", "application/json; charset=utf-8" }
                },
                Outputters = new List<IOutput> {
                    new XUnitOutput(this.output)
                }
            };

            new PactVerifier(config)
                .ServiceProvider("Discounts", this.serviceUri)
                .HonoursPactWith("Orders")
                .PactUri(@"C:\Development\pro-microservices-in-dotnet-6\OrderService\OrderServiceConsumer.Test\bin\Debug\net6.0\temp\pact\OrderServiceConsumer\orders-discounts.json")
                .Verify();
        }

        protected virtual void Dispose(bool disposing) { 
            if (!this.disposed) {
                if (disposing) {
                    //
                }

                this.disposed = true;
            }
        }

        public void Dispose() { 
            this.Dispose(true);
        }
    }
}
