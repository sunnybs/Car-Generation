using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public class CarMesh
    {
        private readonly float cubeSize = 1f;
        public Dictionary<Vector3,Cube> TransmissionMesh;
        
        public CarMesh()
        {
            TransmissionMesh = new Dictionary<Vector3, Cube>();
        }

        //создание сетки для трансмиссии. Можно переделать этот метод для создания сетки для любого типа объекта
        public void AddMeshOfTransmission(Vector3 position, Vector3 form)
        {
            for (var y = position.y; y < position.y + form.y; y += cubeSize)
            for (var z = position.z; z < position.z + form.z; z += cubeSize)
            for (var x = position.x; x < position.x + form.x; x += cubeSize)
            {
                var cube = new Cube(new Vector3(x - form.x / 2, y - form.y / 2, z - form.z / 2), cubeSize);
                TransmissionMesh[new Vector3(x, y, z)] = cube;   
            }
    
        }

        //поиск работает путем выискивания свободных сторон(тоесть таких, к которым не присоединен никакой куб)
        public WheelPlaces FindWheelsPlaces(List<GameObject> wheels)
        {
            var placesOnLeftSide = new List<Cube>();
            var placesOnRightSide = new List<Cube>();
            var rightSides = TransmissionMesh.Select(side => side.Value.Right).ToArray();
            var leftSides = TransmissionMesh.Select(side => side.Value.Left).ToArray();

            foreach (var cube in TransmissionMesh.Values)
            {
                if (IsSideFree(rightSides, cube.Left)) placesOnLeftSide.Add(cube);
                
                if (IsSideFree(leftSides, cube.Right)) placesOnRightSide.Add(cube);
            }
            // вилПласес тут нужен только для возращения двух элементов из функции
            return new WheelPlaces(placesOnLeftSide, placesOnRightSide);  
        }

        public List<Vector3> FindBodyPlace(Vector3 bodyForm)
        {
            var goodPositions = new List<Vector3>();
            foreach (var pos in TransmissionMesh)
            {
                if (IsPositionGoodForBody(pos.Key, bodyForm))
                {
                    var goodPos = new Vector3(pos.Value.Vertexes[0].x + bodyForm.x / 2,
                        pos.Value.Vertexes[0].y + 1 + bodyForm.y / 2,
                        pos.Value.Vertexes[0].z + bodyForm.z / 2);
                    goodPositions.Add(goodPos);
                }
                   
            }

            return goodPositions;
        }

        private bool IsPositionGoodForBody(Vector3 pos, Vector3 bodyForm)
        {
            var temp = new Cube(new Vector3(0,0,0),1 );
            return (TransmissionMesh.TryGetValue(pos, out temp)
                    && TransmissionMesh.TryGetValue(new Vector3(pos.x + bodyForm.x - 1, pos.y, pos.z), out temp)
                    && TransmissionMesh.TryGetValue(new Vector3(pos.x, pos.y, pos.z + bodyForm.z - 1), out temp)
                    && TransmissionMesh.TryGetValue(new Vector3(pos.x + bodyForm.x - 1, pos.y, pos.z + bodyForm.z - 1),
                        out temp));

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