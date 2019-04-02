using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    internal class CarBuilder
    {
        public static void StickTramsmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f;
            var zOffset = 0f;
            foreach (var transmission in transmissions)
            {
                transmission.transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMeshOfTransmission(transmission.transform.position, transmission.transform.localScale);

                var isXOffset = Random.Range(0, 2) >= 1;
                if (isXOffset) xOffset += transmission.transform.localScale.x;
                else zOffset += transmission.transform.localScale.z;
            }
        }

        public static void StickWheels(List<GameObject> wheels, WheelPlaces places)
        {
            for (var i = 0; i < wheels.Count; i++)
            {
                if (i >= wheels.Count / 2)
                {
                    wheels[i].transform.position =
                        places.PlacesOnRightSide[Random.Range(0, places.PlacesOnRightSide.Count - 1)].Left[0];
                    wheels[i].transform.Translate(wheels[i].transform.localScale.x, 0, 0);
                }
                else
                {
                    wheels[i].transform.position =
                        places.PlacesOnLeftSide[Random.Range(0, places.PlacesOnRightSide.Count - 1)].Left[0];
                    wheels[i].transform.Translate(-wheels[i].transform.localScale.x, 0, 0);
                }

                wheels[i].transform.Translate(wheels[i].transform.localScale.x / 2,
                    wheels[i].transform.localScale.y / 2,
                    wheels[i].transform.localScale.z / 2);
            }
        }
    }
}