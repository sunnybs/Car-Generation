using System.Collections.Generic;
using System.Linq;
using Assets.Utils;
using UnityEngine;


//генерация происходит из префабов(заготовленных ранее объектов с уже заданными размерами).
//Все префабы в папке Prefabs
public class CarGenerator : MonoBehaviour
{
    private readonly int yLevel = 3; // уровень, на котором генерируются рамы
    public GameObject[] BodyObjects;
    public GameObject CarInstance; // родительский объект для всех деталей машины (указывается через юнити)
    public GameObject[] TranmissionObjects; // объекты рам (загружаются из префабов через юнити)
    public int Transmission2X4Count = 4;
    public int Transmission4X4Count;
    public GameObject[] Transmission4X4Objects;
    public int WheelCount = 4;
    public GameObject[] WheelObjects; // колеса


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 30), "Generate"))
        {
            var carMesh = new CarMesh();
            var tramsmissions = LoadDetails(DetailType.Transmission2X4, Transmission2X4Count);
            var blueprint = BlueprintManager.PickByCount(Transmission2X4Count);
            blueprint.StickTramsmissions(tramsmissions, carMesh, yLevel);
            var wheels = LoadDetails(DetailType.Wheel, WheelCount);
            var wheelPlaces = carMesh.FindWheelsPlaces(wheels);
            blueprint.StickWheels(wheels, wheelPlaces);
            var body = LoadDetails(DetailType.Body, 1);
            blueprint.StickBody(body.First(), carMesh, new Vector3(4, 4, 6));
            //PrintMesh(carMesh);
        }
    }

    //загрузка деталей. Нужно убрать рандомность
    private List<GameObject> LoadDetails(DetailType type, int count)
    {
        var details = new List<GameObject>();
        for (var i = 0; i < count; i++)
        {
            GameObject randomDetail;
            if (type == DetailType.Transmission2X4)
                randomDetail = TranmissionObjects[Random.Range(0, TranmissionObjects.Length - 1)];
            else if (type == DetailType.Wheel)
                randomDetail = WheelObjects[Random.Range(0, WheelObjects.Length - 1)];
            else if (type == DetailType.Transmission4X4)
                randomDetail = Transmission4X4Objects[Random.Range(0, Transmission4X4Objects.Length - 1)];
            else if (type == DetailType.Body)
                randomDetail = BodyObjects[Random.Range(0, BodyObjects.Length - 1)];
            else
                randomDetail = new GameObject();
            var prefab = Instantiate(randomDetail, new Vector3(0, 0, 0), Quaternion.identity,
                CarInstance.transform); // добавляет объект на карту и задает КарИнстанс как родительский объект
            details.Add(prefab);
        }

        return details;
    }

    private void PrintMesh(CarMesh carMesh)
    {
        var allMeshVertexes = new HashSet<Vector3>();
        foreach (var cube in carMesh.TransmissionMesh.Values)
        foreach (var vert in cube.Vertexes)
            allMeshVertexes.Add(vert);
        var lineRender = CarInstance.GetComponent<LineRenderer>();
        lineRender.widthMultiplier = 0.1f;
        lineRender.positionCount = allMeshVertexes.Count;
        lineRender.SetPositions(allMeshVertexes.ToArray());
    }

    private enum DetailType
    {
        Transmission4X4,
        Transmission2X4,
        Wheel,
        Body
    }
}