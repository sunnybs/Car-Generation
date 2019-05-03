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
                carMesh.AddMesh(transmission.transform.position, transmission.transform.localScale, DetailType.Transmission);
                zOffset += transmission.transform.localScale.z;
            }
        }

        public override void StickBody(GameObject body, CarMesh carMesh)
        {
            var pos = 4;
            CarBuilder.TransformBody(body, carMesh, pos);
        }


        public override void StickArmors(List<GameObject> armors, CarMesh carMesh)
        {
            
            CarBuilder.TransformArmor(armors[0], carMesh, 0);
            
            CarBuilder.TransformArmor(armors[1], carMesh, 8);
        }

        
    }
}