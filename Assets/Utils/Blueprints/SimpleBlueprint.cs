using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    public class SimpleBlueprint : BaseBlueprint
    {
        public override void StickTramsmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f; //отступ для сдвига деталей по ширине
            var zOffset = 0f; //отступ для сдвига деталей по длине
            foreach (var transmission in transmissions)
            {
                transmission.transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMesh(transmission.transform.position, transmission.transform.localScale);
                zOffset += transmission.transform.localScale.z;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh, Detail bodyForm)
        {
            var positions = carMesh.FindBodyPlace(new Vector3(bodyForm.MaxWeight, bodyForm.MaxHeight, bodyForm.MaxLength));
            var pos = positions.Count - 1; // Вот тут можно просто подобрать более менее нормальную позицию
            carMesh.AddLevelsMesh(positions[pos], bodyForm);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            body.transform.position = new Vector3(positions[pos].x - modelCenterOffset.x,
                positions[pos].y - modelCenterOffset.y,
                positions[pos].z - modelCenterOffset.z);
        }
    }
}