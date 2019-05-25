using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Utils;
using UnityEngine;


//генерация происходит из префабов(заготовленных ранее объектов с уже заданными размерами).
//Все префабы в папке Prefabs
public class CarGenerator : MonoBehaviour
{
    public GameObject CarInstance; // родительский объект для всех деталей машины (указывается через юнити)
    public GameObject[] ArmorObjects;
    public GameObject[] BodyObjects;
    public GameObject[] GunObjects;
    public GameObject[] TransmissionObjects;
    public GameObject[] WheelObjects;
    public int ArmorCount;
    public int GunCount;
    public int TransmissionCount;
    public int WheelCount;
    private readonly int yLevel = 3; // уровень, на котором генерируются рамы

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 30), "Generate"))
        {
            ClearChildren(CarInstance);
            if (!AreEnoughDetails()) return;

            var carMesh = new CarMesh();
            var blueprint = BlueprintManager.PickByCount(TransmissionCount, WheelCount, ArmorCount, GunCount);
            var info = blueprint.GetInfo();
            SetCorrectDetailsCount(info);

            var transmissions = LoadDetails(DetailType.Transmission, TransmissionCount);
            blueprint.StickTransmissions(transmissions, carMesh, yLevel);
            var wheels = LoadDetails(DetailType.Wheel, WheelCount);
            blueprint.StickWheels(wheels, carMesh);
            var body = LoadDetails(DetailType.Body, 1);
            blueprint.StickBody(body.First(), carMesh);
            var armors = LoadDetails(DetailType.Armor, ArmorCount);
            blueprint.StickArmors(armors, carMesh);
            var guns = LoadDetails(DetailType.Gun, GunCount);
            blueprint.StickGuns(guns, carMesh);

            //PrintMesh(carMesh);
        }
    }

    private bool AreEnoughDetails()
    {
        if (TransmissionCount < 3)
        {
            Debug.Log("Transmission count can't be less than 3.");
            return false;
        }

        return true;
    }

    public void ClearChildren(GameObject obj)
    {
        var allChildren = new List<GameObject>();
        foreach (Transform child in obj.transform)
            allChildren.Add(child.gameObject);
        foreach (var child in allChildren)
            Destroy(child.gameObject);
    }

    private void SetCorrectDetailsCount(int[] info)
    {
        if (TransmissionCount > info[0]) TransmissionCount = info[0];
        if (WheelCount > info[1]) WheelCount = info[1];
        if (ArmorCount > info[2]) ArmorCount = info[2];
        if (GunCount > info[3]) GunCount = info[3];
    }


    private List<GameObject> LoadDetails(DetailType type, int count)
    {
        var details = new List<GameObject>();
        for (var i = 0; i < count; i++)
        {
            GameObject randomDetail;
            if (type == DetailType.Transmission)
                randomDetail = TransmissionObjects[Random.Range(0, TransmissionObjects.Length - 1)];
            else if (type == DetailType.Wheel)
                randomDetail = WheelObjects[Random.Range(0, WheelObjects.Length - 1)];
            else if (type == DetailType.Body)
                randomDetail = BodyObjects[Random.Range(0, BodyObjects.Length - 1)];
            else if (type == DetailType.Armor)
                randomDetail = ArmorObjects[Random.Range(0, ArmorObjects.Length - 1)];
            else if (type == DetailType.Gun)
                randomDetail = GunObjects[Random.Range(0, GunObjects.Length - 1)];
            else
                randomDetail = new GameObject();
            var prefab = Instantiate(randomDetail, new Vector3(0, 0, 0), randomDetail.transform.rotation,
                CarInstance.transform);
            details.Add(prefab);
        }

        return details;
    }

    private void PrintMesh(CarMesh carMesh)
    {
        var allMeshVertexes = new HashSet<Vector3>();
        foreach (var cube in carMesh.Mesh.Values)
        foreach (var vert in cube.Vertexes)
            allMeshVertexes.Add(vert);
        var lineRender = CarInstance.GetComponent<LineRenderer>();
        lineRender.widthMultiplier = 0.1f;
        lineRender.positionCount = allMeshVertexes.Count;
        lineRender.SetPositions(allMeshVertexes.ToArray());
    }
}