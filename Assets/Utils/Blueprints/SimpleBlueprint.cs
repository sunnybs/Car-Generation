using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils.Blueprints
{
    public class SimpleBlueprint : BaseBlueprint
    {
        public override void StickTransmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f; //отступ для сдвига деталей по ширине
            var zOffset = 0f; //отступ для сдвига деталей по длине
            foreach (var transmission in transmissions)
            {
                var form = transmission.GetComponent<Detail>();
              
                transmission.transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMesh(transmission.transform.position, new Vector3(form.MaxWidth, form.MaxHeight, form.MaxLength), DetailType.Transmission);
                zOffset += form.MaxLength;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh)
        {
           
            CarBuilder.TransformBody(body, carMesh, 5);
        }


        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            try
            {
                CarBuilder.TransformArmor(armors[0], carMesh, 0);
                CarBuilder.TransformArmor(armors[1], carMesh, 46);

            }
            catch (Exception e)
            {
            }


        }
    }
}