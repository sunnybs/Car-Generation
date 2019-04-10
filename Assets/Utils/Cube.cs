using UnityEngine;

namespace Assets.Utils
{
    //куб представляется как 8 точек вершин в пространстве
    //все вершины хранятся в Vertexes
    //все стороны из этих вершин составлены
    public class Cube
    {
        public Vector3[] Back;
        public Vector3[] Bottom;
        public Vector3[] Front;
        public Vector3[] Left;
        public Vector3[] Right;
        public Vector3[] Top;
        public Vector3[] Vertexes;

        public Cube(Vector3 position, float cubeSize)
        {
            Vertexes = new[]
            {
                position,
                new Vector3(position.x + cubeSize, position.y, position.z),
                new Vector3(position.x, position.y, position.z + cubeSize),
                new Vector3(position.x + cubeSize, position.y, position.z + cubeSize),
                new Vector3(position.x, position.y + cubeSize, position.z),
                new Vector3(position.x + cubeSize, position.y + cubeSize, position.z),
                new Vector3(position.x, position.y + cubeSize, position.z + cubeSize),
                new Vector3(position.x + cubeSize, position.y + cubeSize, position.z + cubeSize)
            };
            Bottom = new[] {Vertexes[0], Vertexes[1], Vertexes[2], Vertexes[3]};
            Top = new[] {Vertexes[4], Vertexes[5], Vertexes[6], Vertexes[7]};
            Front = new[] {Vertexes[0], Vertexes[1], Vertexes[4], Vertexes[5]};
            Back = new[] {Vertexes[2], Vertexes[3], Vertexes[6], Vertexes[7]};
            Left = new[] {Vertexes[0], Vertexes[2], Vertexes[4], Vertexes[6]};
            Right = new[] {Vertexes[1], Vertexes[3], Vertexes[5], Vertexes[7]};
        }
    }
}