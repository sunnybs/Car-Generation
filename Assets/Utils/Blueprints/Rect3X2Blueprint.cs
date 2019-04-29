using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    internal class Rect3X2Blueprint : BaseBlueprint
    {
        public override void StickTramsmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f;
            var zOffset = 0f;
            for (var i = 0; i < transmissions.Count; i++)
            {
                if (i == transmissions.Count / 2)
                {
                    zOffset = 0f;
                    xOffset += transmissions[i].transform.localScale.x;
                }

                transmissions[i].transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMesh(transmissions[i].transform.position,
                    transmissions[i].transform.localScale);
                zOffset += transmissions[i].transform.localScale.z;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh, Detail bodyForm)
        {
            var positions = carMesh.FindBodyPlace(new Vector3(bodyForm.MaxWeight, bodyForm.MaxHeight, bodyForm.MaxLength));
            var pos = positions.Count - 3; // Вот тут можно просто подобрать более менее нормальную позицию
            carMesh.AddLevelsMesh(positions[pos], bodyForm);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            body.transform.position = new Vector3(positions[pos].x - modelCenterOffset.x,
                positions[pos].y - modelCenterOffset.y,
                positions[pos].z - modelCenterOffset.z);
        }
    }
}