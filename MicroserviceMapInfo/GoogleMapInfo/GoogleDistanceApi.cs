namespace GoogleMapInfo {
    public class GoogleDistanceApi {
        public GoogleDistanceData GetMapDistanceAsync(string originCity, string destinationCity) {
            return new GoogleDistanceData {
                OriginCity = originCity,
                DestinationCity = destinationCity,
                Distance = new Random().NextInt64()
            };
        }
    }
}
