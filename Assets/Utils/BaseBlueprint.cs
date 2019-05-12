using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    public abstract class BaseBlueprint
    {
        public abstract void StickTransmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel);

        public abstract void StickBody(GameObject body, CarMesh carMesh);

        public abstract void StickArmors(List<GameObject> armors, CarMesh carMesh);

        public void StickWheels(List<GameObject> wheels, CarMesh carMesh)
        {
            var places = carMesh.FindFreeSides();
            int startOffset = 1, endOffset = 1;
            const int offset = 2;
            for (var i = 0; i < wheels.Count / 2; i++)
            {
                var form = wheels[i].GetComponent<Detail>();
                var modelCenterOffset = wheels[i].GetComponentInChildren<Collider>().bounds.center;

                if (i % 2 == 0)
                {
                    wheels[i].transform.position =
                        carMesh.Mesh[places.LeftSidesKeys[places.LeftSidesKeys.Count - 1 - endOffset]].Center;
                    endOffset += offset;
                }
                else
                {
                    wheels[i].transform.position =
                        carMesh.Mesh[places.LeftSidesKeys[startOffset]].Center;
                    startOffset += offset;
                }

                wheels[i].transform.Translate(-form.MaxWidth, 0, 0);
                carMesh.AddMesh(wheels[i].transform.position,
                    new Vector3(form.MaxWidth, form.MaxHeight, form.MaxLength),
                    DetailType.Wheel);
                wheels[i].transform.Translate(-modelCenterOffset.x,-modelCenterOffset.y, -modelCenterOffset.z);
            }

            startOffset = 1;
            endOffset = 1;
            var offsetForRightSide = wheels.Count / 2;
            for (var i = 0; i < wheels.Count / 2 + wheels.Count % 2; i++)
            {
                var index = i + offsetForRightSide;
                var form = wheels[index].GetComponent<Detail>();
                var modelCenterOffset = wheels[index].GetComponentInChildren<Collider>().bounds.center;

                if (i % 2 == 0)
                {
                    wheels[index].transform.position =
                        carMesh.Mesh[places.RightSidesKeys[places.RightSidesKeys.Count - 1 - endOffset]].Center;
                    endOffset += offset;
                }
                else
                {
                    wheels[index].transform.position =
                        carMesh.Mesh[places.RightSidesKeys[startOffset]].Center;
                    startOffset += offset;
                }

                wheels[index].transform.Translate(form.MaxWidth, 0, 0);
                carMesh.AddMesh(wheels[index].transform.position,
                    new Vector3(form.MaxWidth, form.MaxHeight, form.MaxLength),
                    DetailType.Wheel);
                wheels[index].transform.Translate(-modelCenterOffset.x, -modelCenterOffset.y, -modelCenterOffset.z);
            }
        }
    }
}