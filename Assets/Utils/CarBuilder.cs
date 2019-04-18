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

                var isXOffset = false; /* тут определяется будет ли сдвиг по ширине или по длине
                нужно убрать рандомность и заставить рамы сдвигаться так, чтобы общая склеенная рама получилась
                симметричной*/
                if (isXOffset)
                    xOffset += transmission.transform.localScale.x; // в localScale хранятся данные о форме объекта
                else zOffset += transmission.transform.localScale.z;
            }
        }

        public static void StickBody(GameObject body, CarMesh carMesh)
        {
            body.transform.localScale = new Vector3(1.3f,1,1.3f);
           
            var pos = carMesh.FindBodyPlace(new Vector3(4.6f,2.4f,8.6f));
            body.transform.position = pos;
        }

        //приклеивание колес к раме на доступные места
        //ЭТО ПРОСТО ДИЧЬ, работает по сути для любых четырехколесных, но надо отрефакторить, а то слишком грязно,
        public static void StickWheels(List<GameObject> wheels, WheelPlaces places)
        {
            var i = 0;
            var cubeList = new List<Vector3>();
            while (i < wheels.Count)
            {
                if (i % 2 == 0)
                {
                    if (cubeList.Count !=2)
                    {
                        wheels[i].transform.position =
                            places.PlacesOnLeftSide[places.PlacesOnRightSide.Count - 2].Left[0];
                        wheels[i].transform.Translate(-wheels[i].transform.localScale.x, 0, 0);
                        cubeList.Insert(i, wheels[i].gameObject.transform.position);
                    }
                    else
                    {
                        wheels[i].transform.position = places.PlacesOnLeftSide[1].Left[0];
                        wheels[i].transform.Translate(-wheels[i].transform.localScale.x, 0, 0);
                        cubeList.Insert(i, wheels[i].gameObject.transform.position);
                    }
                }
                else
                {

                    if (cubeList.Count !=3)
                    {
                        wheels[i].transform.position =
                            places.PlacesOnRightSide[places.PlacesOnRightSide.Count - 2].Left[0];
                        wheels[i].transform.Translate(wheels[i].transform.localScale.x, 0, 0); //сдвиг относительно рамы
                        cubeList.Insert(i, wheels[i].gameObject.transform.position);
                    }
                    else
                    {
                        wheels[i].transform.position = places.PlacesOnRightSide[1].Left[0];
                        wheels[i].transform.Translate(wheels[i].transform.localScale.x, 0, 0);
                        cubeList.Insert(i, wheels[i].gameObject.transform.position);
                    }
                }

                 wheels[i].transform.Translate(wheels[i].transform.localScale.x / 2,
                wheels[i].transform.localScale.y / 2,
                wheels[i].transform.localScale.z / 2);
                 i++;
            }
        }
    }
}

    
  