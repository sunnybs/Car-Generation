using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            body.transform.localScale = new Vector3(1.3f, 2, 1.3f);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            var pos = carMesh.FindBodyPlace(bodyBottomForm);
            body.transform.position = new Vector3(pos[pos.Count - 1].x - modelCenterOffset.x,
                pos[pos.Count - 1].y - modelCenterOffset.y,
                pos[pos.Count - 1].z - modelCenterOffset.z);
        }
    }
}
