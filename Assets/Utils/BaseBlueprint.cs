using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    public abstract class BaseBlueprint
    {
        public abstract void StickTramsmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel);

        public abstract void StickBody(GameObject body, CarMesh carMesh, Vector3 bodyBottomForm);

        public void StickWheels(List<GameObject> wheels, WheelPlaces places)
        {
            int startOffset = 1, endOffset = 1;
            const int offset = 2;
            for (var i = 0; i < wheels.Count / 2; i++)
            {
                if (i % 2 == 0)
                {
                    wheels[i].transform.position =
                        places.PlacesOnLeftSide[places.PlacesOnRightSide.Count - 1 - endOffset].Center;
                    endOffset += offset;
                }
                else
                {
                    wheels[i].transform.position =
                        places.PlacesOnLeftSide[startOffset].Center;
                    startOffset += offset;
                }

                wheels[i].transform.Translate(-wheels[i].transform.localScale.x, 0, 0);
            }

            startOffset = 1;
            endOffset = 1;
            var offsetForRightSide = wheels.Count / 2;
            for (var i = 0; i < wheels.Count / 2 + wheels.Count % 2; i++)
            {
                if (i % 2 == 0)
                {
                    wheels[i + offsetForRightSide].transform.position =
                        places.PlacesOnRightSide[places.PlacesOnRightSide.Count - 1 - endOffset].Center;
                    endOffset += offset;
                }
                else
                {
                    wheels[i + offsetForRightSide].transform.position =
                        places.PlacesOnRightSide[startOffset].Center;
                    startOffset += offset;
                }

                wheels[i + offsetForRightSide].transform.Translate(wheels[i].transform.localScale.x, 0, 0);
            }
        }
    }
}