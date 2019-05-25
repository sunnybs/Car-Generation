using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    internal class Rect3X2Blueprint : BaseBlueprint
    {
        public override int[] GetInfo()
        {
            var maxTransmissionCount = 6;
            var maxWheelCount = 4;
            var maxArmorCount = 4;
            var maxGunCount = 2;
            return new[] { maxTransmissionCount, maxWheelCount, maxArmorCount, maxGunCount };
        }

        public override void StickTransmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f;
            var zOffset = 0f;
            for (var i = 0; i < transmissions.Count; i++)
            {
                var form = transmissions[i].GetComponent<Detail>();
                if (i == transmissions.Count / 2)
                {
                    zOffset = 0f;
                    xOffset += form.MaxWidth;
                }

                transmissions[i].transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMesh(transmissions[i].transform.position,
                    new Vector3(form.MaxWidth,form.MaxHeight,form.MaxLength),DetailType.Transmission);
                zOffset += form.MaxLength;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh)
        {
            CarBuilder.TransformBody(body, carMesh, 2);
        }

        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            var name = "RectBlueprintArmors";
            bool staticMode = true;
            try
            {
                if (staticMode) 
                {
                    var positions =
                        CarBuilder.DeserializeTransforms("Assets/Utils/Blueprints/BlueprintsData/" + name + ".dat");
                    for (var i = 0; i < positions.Count; i++)
                    {
                        CarBuilder.TransformDetail(armors[i], carMesh, positions[i]);
                    }
                }
                else
                {
                    CarBuilder.TransformDetail(armors[0], carMesh, 0);
                    CarBuilder.TransformDetail(armors[1], carMesh, 8);
                    CarBuilder.TransformDetail(armors[2], carMesh, 46);
                    CarBuilder.TransformDetail(armors[3], carMesh, 42);
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
            var name = "RectBlueprintGuns";
            bool staticMode = true;
            try
            {
                if (staticMode)
                {
                    var positions =
                        CarBuilder.DeserializeTransforms("Assets/Utils/Blueprints/BlueprintsData/" + name + ".dat");
                    for (var i = 0; i < positions.Count; i++)
                    {
                        CarBuilder.TransformDetail(guns[i], carMesh, positions[i]);
                    }
                }
                else
                {
                    CarBuilder.TransformDetail(guns[0], carMesh, 52);
                    CarBuilder.TransformDetail(guns[1], carMesh, 52);
                    CarBuilder.SavePositions(guns, name);
                }
            }
            catch (Exception e)
            {
                Debug.Log("Pos for gun in unavailable.");
            }
        }
    }
}