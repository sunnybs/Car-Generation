using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    internal class Rect3X2Blueprint : BaseBlueprint
    {
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
                zOffset += form.MaxHeight;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh)
        {
            var bodyForm = body.GetComponent<Detail>();
            var positions = carMesh.FindBodyPlace(new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength));
            var pos = positions.Count - 3; // Вот тут можно просто подобрать более менее нормальную позицию
            carMesh.AddLevelsMesh(positions[pos], bodyForm);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            body.transform.position = new Vector3(positions[pos].x - modelCenterOffset.x,
                positions[pos].y - modelCenterOffset.y,
                positions[pos].z - modelCenterOffset.z);
        }

        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            CarBuilder.TransformArmor(armors[0], carMesh, 0);

            CarBuilder.TransformArmor(armors[1], carMesh, 8);
        }
    }
}