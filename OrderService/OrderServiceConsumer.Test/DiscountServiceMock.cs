using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PactNet;
using PactNet.Mocks.MockHttpService;
using System;

namespace OrderServiceConsumer.Test {
    public class DiscountServiceMock : IDisposable {
        private readonly IPactBuilder pactBuilder;
        private readonly int servicePort = 9222;
        private bool disposed;

        public DiscountServiceMock() {
            var pactConfig = new PactConfig {
                SpecificationVersion = "2.0.0",
                PactDir = @".\temp\pact\OrderServiceConsumer",
                LogDir = @".\temp\pact\OrderServiceConsumer\logs"
            };

            this.pactBuilder = new PactBuilder(pactConfig)
                .ServiceConsumer("Orders")
                .HasPactWith("Discounts");

            this.MockProviderService = this.pactBuilder.MockService(this.servicePort, new JsonSerializerSettings {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public IMockProviderService MockProviderService { get; }
        public string ServiceUri => $"http://localhost:{servicePort}";

        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    this.pactBuilder.Build();
                }
                this.disposed = true;
            }
        }

        public void Dispose() { 
            this.Dispose(true);
        }
    }
}
