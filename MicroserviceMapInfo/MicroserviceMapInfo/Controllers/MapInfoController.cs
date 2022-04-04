using GoogleMapInfo;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceMapInfo.Controllers {
    [Route("[controller]")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class MapInfoController : Controller {
        private readonly GoogleDistanceApi googleDistanceApi;

        public MapInfoController(GoogleDistanceApi googleDistanceApi) { 
            this.googleDistanceApi = googleDistanceApi;
        }

        [HttpGet]
        public GoogleDistanceData GetDistance(string originCity, string destinationCity) {
            return this.googleDistanceApi.GetMapDistanceAsync(originCity, destinationCity);
        }
    }
}
