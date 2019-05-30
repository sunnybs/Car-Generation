using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Utils
{
    internal class CarBuilder
    {
        public static void TransformBody(GameObject body, CarMesh carMesh, int pos)
        {
            var bodyForm = body.GetComponent<Detail>();
            var positions =
                carMesh.FindBodyPlace(new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength));
            if (pos >= positions.Count) pos = positions.Count - 1;
            carMesh.AddLevelsMesh(positions[pos], bodyForm);
            var modelCenterOffset = body.GetComponentInChildren<Collider>().bounds.center;
            body.transform.position = new Vector3(positions[pos].x - modelCenterOffset.x,
                positions[pos].y - modelCenterOffset.y,
                positions[pos].z - modelCenterOffset.z);
        }

        public static void TransformDetail(GameObject armor, CarMesh carMesh, int pos)
        {
            var bodyForm = armor.GetComponent<Detail>();
            var positions = carMesh.FindDetailPlace(bodyForm);
            if (pos >= positions.Count) pos = positions.Count - 1;
            if (Mathf.Abs(positions[pos].Rotation.y - 180) == 90)
                carMesh.AddMesh(positions[pos].Position,
                    new Vector3(bodyForm.MaxLength, bodyForm.MaxHeight, bodyForm.MaxWidth),
                    DetailType.Armor);
            else
                carMesh.AddMesh(positions[pos].Position,
                    new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength),
                    DetailType.Armor);
            var modelCenterOffset = armor.GetComponentInChildren<Collider>().bounds.center;
            armor.transform.position = new Vector3(positions[pos].Position.x - modelCenterOffset.x,
                positions[pos].Position.y - modelCenterOffset.y,
                positions[pos].Position.z - modelCenterOffset.z);
            armor.transform.Rotate(positions[pos].Rotation);
        }

        public static void TransformDetail(GameObject armor, CarMesh carMesh, DetailPlace pos)
        {
            var bodyForm = armor.GetComponent<Detail>();
            if (Mathf.Abs(pos.Rotation.y - 180) == 0)
                carMesh.AddMesh(pos.Position, new Vector3(bodyForm.MaxLength, bodyForm.MaxHeight, bodyForm.MaxWidth),
                    DetailType.Armor);
            else
                carMesh.AddMesh(pos.Position, new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength),
                    DetailType.Armor);

            armor.transform.position = new Vector3(pos.Position.x,
                pos.Position.y,
                pos.Position.z);
            armor.transform.rotation = new Quaternion();
            armor.transform.Rotate(pos.Rotation);
        }

        public static void TransformTransmissionsInRect(List<GameObject> transmissions, CarMesh carMesh, float yLevel, int x, int z)
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

        public static void SavePositions(IEnumerable<GameObject> objects, string blueprint)
        {
            var positions = objects.Select(pos => pos.transform).ToList();
            var serialized = SerializeTransforms(positions);
            File.WriteAllText("Assets/Utils/Blueprints/BlueprintsData/" + blueprint + ".dat", serialized);
        }

        private static string SerializeTransforms(IEnumerable<Transform> positions)
        {
            var serialized = new StringBuilder();
            foreach (var pos in positions)
            {
                serialized.AppendLine(pos.position.x.ToString());
                serialized.AppendLine(pos.position.y.ToString());
                serialized.AppendLine(pos.position.z.ToString());
                serialized.AppendLine(pos.eulerAngles.x.ToString());
                serialized.AppendLine(pos.eulerAngles.y.ToString());
                serialized.AppendLine(pos.eulerAngles.z.ToString());
                serialized.AppendLine("$");
            }

            return serialized.ToString();
        }

        public static List<DetailPlace> DeserializeTransforms(string filePath)
        {
            var result = new List<DetailPlace>();
            var serialized = File.ReadAllText(filePath);
            var transforms = serialized.Split(new[] {"$"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var trans in transforms)
                try
                {
                    var coords = trans.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);
                    var place = new DetailPlace(
                        new Vector3(float.Parse(coords[0]), float.Parse(coords[1]), float.Parse(coords[2])),
                        new Vector3(float.Parse(coords[3]), float.Parse(coords[4]), float.Parse(coords[5])));
                    result.Add(place);
                }
                catch (Exception e)
                {
                }

            return result;
        }
    }
}