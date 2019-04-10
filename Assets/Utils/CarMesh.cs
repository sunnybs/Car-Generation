using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public class CarMesh
    {
        private readonly float cubeSize = 1f;
        public List<Cube> TransmissionMesh;

        public CarMesh()
        {
            TransmissionMesh = new List<Cube>();
        }

        //создание сетки для трансмиссии. Можно переделать этот метод для создания сетки для любого типа объекта
        public void AddMeshOfTransmission(Vector3 position, Vector3 form)
        {
            for (var y = position.y; y < position.y + form.y; y += cubeSize)
            for (var z = position.z; z < position.z + form.z; z += cubeSize)
            for (var x = position.x; x < position.x + form.x; x += cubeSize)
                TransmissionMesh.Add(new Cube(new Vector3(x - form.x / 2, y - form.y / 2, z - form.z / 2), cubeSize));
        }

        //поиск работает путем выискивания свободных сторон(тоесть таких, к которым не присоединен никакой куб)
        public WheelPlaces FindWheelsPlaces(List<GameObject> wheels)
        {
            var placesOnLeftSide = new List<Cube>();
            var placesOnRightSide = new List<Cube>();
            var rightSides = TransmissionMesh.Select(side => side.Right).ToArray();
            var leftSides = TransmissionMesh.Select(side => side.Left).ToArray();

            foreach (var cube in TransmissionMesh)
            {
                if (IsSideFree(rightSides, cube.Left)) placesOnLeftSide.Add(cube);
                
                if (IsSideFree(leftSides, cube.Right)) placesOnRightSide.Add(cube);
            }
            // вилПласес тут нужен только для возращения двух элементов из функции
            return new WheelPlaces(placesOnLeftSide, placesOnRightSide);  
        }

        
        private static bool IsSideFree(Vector3[][] sides, Vector3[] sideToFind)
        {
            foreach (var side in sides)
                if (VectorsEqual(side[0], sideToFind[0]) &&
                    VectorsEqual(side[1], sideToFind[1]) &&
                    VectorsEqual(side[2], sideToFind[2]) &&
                    VectorsEqual(side[3], sideToFind[3]))
                    return false;
            return true;
        }

        private static bool VectorsEqual(Vector3 a, Vector3 b)
        {
            return Mathf.Abs(a.x - b.x) <= float.Epsilon &&
                   Mathf.Abs(a.y - b.y) <= float.Epsilon &&
                   Mathf.Abs(a.z - b.z) <= float.Epsilon;
        }
    }
}