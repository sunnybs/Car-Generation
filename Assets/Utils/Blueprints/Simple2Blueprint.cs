using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    public class Simple2Blueprint : BaseBlueprint
    {
        public override int[] GetInfo()
        {
            var maxTransmissionCount = 6;
            var maxWheelCount = 4;
            var maxArmorCount = 4;
            var maxGunCount = 4;
            return new[] {maxTransmissionCount, maxWheelCount, maxArmorCount, maxGunCount};
        }

        public override void StickTransmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f; //отступ для сдвига деталей по ширине
            var zOffset = 0f; //отступ для сдвига деталей по длине
            foreach (var transmission in transmissions)
            {
                var form = transmission.GetComponent<Detail>();

                transmission.transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMesh(transmission.transform.position,
                    new Vector3(form.MaxWidth, form.MaxHeight, form.MaxLength), DetailType.Transmission);
                zOffset += form.MaxLength;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh)
        {
            CarBuilder.TransformBody(body, carMesh, 5);
        }

        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            var name = "ArmorsSimple2Blueprint";
            var staticMode = true;
            try
            {
                if (staticMode)
                {
                    var positions =
                        CarBuilder.DeserializeTransforms("Assets/Utils/Blueprints/BlueprintsData/" + name + ".dat");
                    for (var i = 0; i < positions.Count; i++)
                        CarBuilder.TransformDetail(armors[i], carMesh, positions[i]);
                }
                else
                {
                    CarBuilder.TransformDetail(armors[0], carMesh, 0);
                    CarBuilder.TransformDetail(armors[1], carMesh, 46);
                    CarBuilder.TransformDetail(armors[2], carMesh, 33);
                    CarBuilder.TransformDetail(armors[3], carMesh, 33);
                    CarBuilder.SavePositions(armors, name);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Pos for armor in unavailable.");
            }
        }


        public override void StickGuns(List<GameObject> guns, CarMesh carMesh)
        {
            var name = "GunsSimple2Blueprint";
            var staticMode = true;
            try
            {
                if (staticMode)
                {
                    var positions =
                        CarBuilder.DeserializeTransforms("Assets/Utils/Blueprints/BlueprintsData/" + name + ".dat");
                    for (var i = 0; i < positions.Count; i++)
                        CarBuilder.TransformDetail(guns[i], carMesh, positions[i]);
                }
                else
                {
                    CarBuilder.TransformDetail(guns[0], carMesh, 100);
                    CarBuilder.TransformDetail(guns[1], carMesh, 100);
                    CarBuilder.TransformDetail(guns[2], carMesh, 10);
                    CarBuilder.TransformDetail(guns[3], carMesh, 6);
                    CarBuilder.SavePositions(guns, name);
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}