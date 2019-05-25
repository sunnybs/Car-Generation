using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    class SmallBlueprint : BaseBlueprint
    {
        public override int[] GetInfo()
        {
            var maxTransmissionCount = 4;
            var maxWheelCount = 4;
            var maxArmorCount = 0;
            var maxGunCount = 0;
            return new[] { maxTransmissionCount, maxWheelCount, maxArmorCount, maxGunCount };
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
            CarBuilder.TransformBody(body,carMesh,2);
        }

        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            
        }

        public override void StickGuns(List<GameObject> armors, CarMesh carMesh)
        {
            
        }
    }
}
