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
                carMesh.AddMeshOfTransmission(transmission.transform.position, transmission.transform.localScale);

                var isXOffset = false; /* тут определяется будет ли сдвиг по ширине или по длине
                нужно убрать рандомность и заставить рамы сдвигаться так, чтобы общая склеенная рама получилась
                симметричной*/
                if (isXOffset)
                    xOffset += transmission.transform.localScale.x; // в localScale хранятся данные о форме объекта
                else zOffset += transmission.transform.localScale.z;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh, Vector3 bodyBottomForm)
        {
            var positions = carMesh.FindBodyPlace(bodyBottomForm);
            var pos = positions.Count - 1; // Вот тут можно просто подобрать более менее нормальную позицию
            body.transform.localScale = new Vector3(1.3f, 2, 1.3f);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            body.transform.position = new Vector3(positions[pos].x - modelCenterOffset.x,
                positions[pos].y - modelCenterOffset.y,
                positions[pos].z - modelCenterOffset.z);
        }
    }
}