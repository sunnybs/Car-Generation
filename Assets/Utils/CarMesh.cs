using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Utils
{
    public class CarMesh
    {
        private readonly float cubeSize = 1f;
        public Dictionary<Vector3, DetailType> DetailsMesh;
        public Dictionary<Vector3, Cube> Mesh;


        public CarMesh()
        {
            Mesh = new Dictionary<Vector3, Cube>();
            DetailsMesh = new Dictionary<Vector3, DetailType>();
        }


        public void AddMesh(Vector3 position, Vector3 form, DetailType type)
        {
            for (var y = position.y; y < position.y + form.y; y += cubeSize)
            for (var z = position.z; z < position.z + form.z; z += cubeSize)
            for (var x = position.x; x < position.x + form.x; x += cubeSize)
            {
                var key = new Vector3(x - form.x / 2, y - form.y / 2, z - form.z / 2);
                var cube = new Cube(key, cubeSize);
                Mesh[key] = cube;
                DetailsMesh[key] = type;
            }
        }

        public void AddLevelsMesh(Vector3 position, Detail form)
        {
            var startPos = new Vector3(position.x - form.MaxWidth / 2,
                position.y - form.MaxHeight / 2,
                position.z - form.MaxLength / 2);
            var yOffset = 0f;
            foreach (var level in form.FormLevels)
            {
                var pos = new Vector3(startPos.x + level.x / 2,
                    startPos.y + level.y / 2 + yOffset,
                    startPos.z + level.z / 2);
                AddMesh(pos, level, form.Type);
                yOffset += level.y;
            }
        }


        public List<DetailPlace> FindDetailPlace(Detail detail)
        {
            var result = new List<DetailPlace>();
            var freePlaces = FindFreeSides();

            foreach (var topPlace in freePlaces.TopSidesKeys)
            {
                var rotations = Enumerable.Range(0, 4).Select(rot => new Vector3(0, 90 * rot, 0)).ToArray();

                var pos = new Vector3(Mesh[topPlace].Vertexes[0].x + detail.MaxWidth / 2,
                    Mesh[topPlace].Vertexes[0].y + cubeSize + detail.MaxHeight / 2,
                    Mesh[topPlace].Vertexes[0].z + detail.MaxLength / 2);
                result.Add(new DetailPlace(pos, rotations[0]));

                pos = new Vector3(Mesh[topPlace].Vertexes[0].x + detail.MaxLength / 2,
                    Mesh[topPlace].Vertexes[0].y + cubeSize + detail.MaxHeight / 2,
                    Mesh[topPlace].Vertexes[0].z + detail.MaxWidth / 2);
                result.Add(new DetailPlace(pos, rotations[1]));

                pos = new Vector3(Mesh[topPlace].Vertexes[0].x + detail.MaxWidth / 2,
                    Mesh[topPlace].Vertexes[0].y + cubeSize + detail.MaxHeight / 2,
                    Mesh[topPlace].Vertexes[0].z - detail.MaxLength / 2 + cubeSize);
                result.Add(new DetailPlace(pos, rotations[2]));

                pos = new Vector3(Mesh[topPlace].Vertexes[0].x - detail.MaxLength / 2 + cubeSize,
                    Mesh[topPlace].Vertexes[0].y + cubeSize + detail.MaxHeight / 2,
                    Mesh[topPlace].Vertexes[0].z + detail.MaxWidth / 2);
                result.Add(new DetailPlace(pos, rotations[3]));
            }

            return result;
        }


        public FoundSidesKeys FindFreeSides()
        {
            var result = new FoundSidesKeys();
            foreach (var cube in Mesh)
            {
                if (!Mesh.ContainsKey(new Vector3(cube.Key.x + cubeSize, cube.Key.y, cube.Key.z)))
                    result.RightSidesKeys.Add(cube.Key);
                if (!Mesh.ContainsKey(new Vector3(cube.Key.x - cubeSize, cube.Key.y, cube.Key.z)))
                    result.LeftSidesKeys.Add(cube.Key);
                if (!Mesh.ContainsKey(new Vector3(cube.Key.x, cube.Key.y + cubeSize, cube.Key.z)))
                    result.TopSidesKeys.Add(cube.Key);
                if (!Mesh.ContainsKey(new Vector3(cube.Key.x, cube.Key.y, cube.Key.z + cubeSize)))
                    result.ForwardSidesKeys.Add(cube.Key);
                if (!Mesh.ContainsKey(new Vector3(cube.Key.x, cube.Key.y, cube.Key.z - cubeSize)))
                    result.BackSidesKeys.Add(cube.Key);
            }

            return result;
        }

        public List<Vector3> FindBodyPlace(Vector3 bodyForm)
        {
            var goodPositions = new List<Vector3>();
            foreach (var pos in Mesh)
                if (IsPositionGoodForBody(pos.Key, bodyForm))
                {
                    var goodPos = new Vector3(pos.Value.Vertexes[0].x + bodyForm.x / 2,
                        pos.Value.Vertexes[0].y + cubeSize + bodyForm.y / 2,
                        pos.Value.Vertexes[0].z + bodyForm.z / 2);
                    goodPositions.Add(goodPos);
                }

            return goodPositions;
        }

        private bool IsPositionGoodForBody(Vector3 pos, Vector3 bodyForm)
        {
            return Mesh.ContainsKey(pos)
                   && Mesh.ContainsKey(new Vector3(pos.x + bodyForm.x - cubeSize, pos.y, pos.z))
                   && Mesh.ContainsKey(new Vector3(pos.x, pos.y, pos.z + bodyForm.z - cubeSize))
                   && Mesh.ContainsKey(new Vector3(pos.x + bodyForm.x - cubeSize, pos.y,
                       pos.z + bodyForm.z - cubeSize));
        }
    }
}