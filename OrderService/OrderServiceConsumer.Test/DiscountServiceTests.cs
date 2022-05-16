using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace OrderServiceConsumer.Test {
    public class DiscountServiceTests : IClassFixture<DiscountServiceMock> {
        private readonly IMockProviderService mockProviderService;
        private readonly string serviceUri;

        public DiscountServiceTests(DiscountServiceMock discountServiceMock) { 
            this.mockProviderService = discountServiceMock.MockProviderService;
            this.serviceUri = discountServiceMock.ServiceUri;
            this.mockProviderService.ClearInteractions();
        }

        [Fact]
        public async Task GetDiscountAdjustmentAmount() {
            var discountModel = new DiscountModel {
                CustomerRating = 4.1
            };

            this.mockProviderService
                .Given("Rate")
                .UponReceiving("Give a custom rating, an adjustment discount amount will be returned.")
                .With(new ProviderServiceRequest {
                    Method = HttpVerb.Post,
                    Path = "/discount",
                    Body = discountModel,
                    Headers = new Dictionary<string, object> {
                        { "Content-Type", "application/json; charset=utf-8" }
                    }
                })
                .WillRespondWith(new ProviderServiceResponse {
                    Status = 200,
                    Headers = new Dictionary<string, object> {
                        { "Content-Type", "application/json; charset=utf-8" }
                    },
                    Body = new DiscountModel { CustomerRating = 4.1, AmountToDiscount = .41 }
                });

            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync($"{this.serviceUri}/discount", discountModel);
            var discountModelReturned = await response.Content.ReadFromJsonAsync<DiscountModel>();

            Assert.Equal(discountModel.CustomerRating, discountModelReturned.CustomerRating);
        }
    }
}
