using GoogleMapInfo;
using Grpc.Core;
using MicroserviceMapInfo.Protos;

namespace MicroserviceMapInfo.Services {
    public class DistanceInfoService : DistanceInfo.DistanceInfoBase {
        private readonly ILogger<DistanceInfoService> logger;
        private readonly GoogleDistanceApi googleDistanceApi;

        public DistanceInfoService(ILogger<DistanceInfoService> logger, GoogleDistanceApi googleDistanceApi) { 
            this.logger = logger;
            this.googleDistanceApi = googleDistanceApi;
        }

        public override Task<DistanceData> GetDistance(Cities cities, ServerCallContext context) {
            this.logger.LogInformation("Getting distance data using gRPC request.");
            var distanceData = this.googleDistanceApi.GetMapDistance(cities.OriginCity, cities.DestinationCity);
            return Task.FromResult(new DistanceData { Miles = distanceData.Distance });
        }
    }
}
