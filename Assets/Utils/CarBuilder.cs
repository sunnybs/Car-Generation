using System.Collections.Generic;
using UnityEngine;

namespace Assets.Utils
{
    internal class CarBuilder
    {
        //склейка рам в пространстве и добавление их в сетку машины
        //рамы склеиваются путем сдвига в пространстве на ширину или длину детали
        public static void StickTramsmissions(List<GameObject> transmissions, CarMesh carMesh, float yLevel)
        {
            var xOffset = 0f; //отступ для сдвига деталей по ширине
            var zOffset = 0f; //отступ для сдвига деталей по длине
            foreach (var transmission in transmissions)
            {
                transmission.transform.Translate(xOffset, yLevel, zOffset);
                carMesh.AddMeshOfTransmission(transmission.transform.position, transmission.transform.localScale);

                var isXOffset = Random.Range(0, 2) >= 1; /* тут определяется будет ли сдвиг по ширине или по длине
                нужно убрать рандомность и заставить рамы сдвигаться так, чтобы общая склеенная рама получилась
                симметричной*/
                if (isXOffset) xOffset += transmission.transform.localScale.x; // в localScale хранятся данные о форме объекта
                else zOffset += transmission.transform.localScale.z;
            }
        }

        //приклеивание колес к раме на доступные места
        public static void StickWheels(List<GameObject> wheels, WheelPlaces places)
        {
            for (var i = 0; i < wheels.Count; i++)
            {
                //конечные места, где будут расположены колеса, выбираются рандомно
                //нужно убрать рандом и заставить колеса размещаться где-то в углах рамы, чтобы это было приемлимо
                if (i >= wheels.Count / 2)
                {
                    wheels[i].transform.position =
                        places.PlacesOnRightSide[Random.Range(0, places.PlacesOnRightSide.Count - 1)].Left[0];
                    wheels[i].transform.Translate(wheels[i].transform.localScale.x, 0, 0); //сдвиг относительно рамы
                }
                else
                {
                    wheels[i].transform.position =
                        places.PlacesOnLeftSide[Random.Range(0, places.PlacesOnRightSide.Count - 1)].Left[0];
                    wheels[i].transform.Translate(-wheels[i].transform.localScale.x, 0, 0);
                }

                //помещение колеса в центр куба сетки
                wheels[i].transform.Translate(wheels[i].transform.localScale.x / 2,
                    wheels[i].transform.localScale.y / 2,
                    wheels[i].transform.localScale.z / 2);
            }
        }
    }
}