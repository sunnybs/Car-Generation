using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils
{
    class CarBuilder
    {
        public static void TransformBody(GameObject body, CarMesh carMesh, int pos)
        {
            var bodyForm = body.GetComponent<Detail>();
            var positions = carMesh.FindBodyPlace(new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength));
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
                carMesh.AddMesh(positions[pos].Position, new Vector3(bodyForm.MaxLength, bodyForm.MaxHeight, bodyForm.MaxWidth),
                    DetailType.Armor);
            else
                carMesh.AddMesh(positions[pos].Position, new Vector3(bodyForm.MaxWidth, bodyForm.MaxHeight, bodyForm.MaxLength),
                    DetailType.Armor);
            var modelCenterOffset = armor.GetComponentInChildren<Collider>().bounds.center;
            armor.transform.position = new Vector3(positions[pos].Position.x - modelCenterOffset.x,
                positions[pos].Position.y - modelCenterOffset.y,
                positions[pos].Position.z - modelCenterOffset.z);
            armor.transform.Rotate(positions[pos].Rotation);
        }
    }
}
