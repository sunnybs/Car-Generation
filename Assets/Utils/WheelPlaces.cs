using System.Collections.Generic;

namespace Assets.Utils
{
    public class WheelPlaces
    {
        public List<Cube> PlacesOnLeftSide;
        public List<Cube> PlacesOnRightSide;

        public WheelPlaces(List<Cube> placesOnLeftSide, List<Cube> placesOnRightSide)
        {
            PlacesOnLeftSide = placesOnLeftSide;
            PlacesOnRightSide = placesOnRightSide;
        }
    }
}