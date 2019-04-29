using System.Collections.Generic;
using System.Linq;
using Assets;
using Assets.Utils;
using UnityEngine;


//генерация происходит из префабов(заготовленных ранее объектов с уже заданными размерами).
//Все префабы в папке Prefabs
public class CarGenerator : MonoBehaviour
{
    public GameObject[] BodyObjects;
    public GameObject CarInstance; // родительский объект для всех деталей машины (указывается через юнити)
    public GameObject[] TranmissionObjects; // объекты рам (загружаются из префабов через юнити)
    public GameObject[] WheelObjects; // колеса
    public int TransmissionCount = 4;
    public int WheelCount = 4;
    private readonly int yLevel = 3; // уровень, на котором генерируются рамы

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 30), "Generate"))
        {
            var carMesh = new CarMesh();
            var tramsmissions = LoadDetails(DetailType.Transmission, TransmissionCount);
            var blueprint = BlueprintManager.PickByCount(TransmissionCount);
            blueprint.StickTramsmissions(tramsmissions, carMesh, yLevel);
            var wheels = LoadDetails(DetailType.Wheel, WheelCount);
            var wheelPlaces = carMesh.FindWheelsPlaces(wheels);
            blueprint.StickWheels(wheels, wheelPlaces);
            var body = LoadDetails(DetailType.Body, 1);
            var bodyForm = body[0].GetComponent<Detail>();
            blueprint.StickBody(body.First(), carMesh, bodyForm);
            PrintMesh(carMesh);
        }
    }


    private List<GameObject> LoadDetails(DetailType type, int count)
    {
        var details = new List<GameObject>();
        for (var i = 0; i < count; i++)
        {
            GameObject randomDetail;
            if (type == DetailType.Transmission)
                randomDetail = TranmissionObjects[Random.Range(0, TranmissionObjects.Length - 1)];
            else if (type == DetailType.Wheel)
                randomDetail = WheelObjects[Random.Range(0, WheelObjects.Length - 1)];
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
        foreach (var cube in carMesh.Mesh.Values)
        foreach (var vert in cube.Vertexes)
            allMeshVertexes.Add(vert);
        var lineRender = CarInstance.GetComponent<LineRenderer>();
        lineRender.widthMultiplier = 0.1f;
        lineRender.positionCount = allMeshVertexes.Count;
        lineRender.SetPositions(allMeshVertexes.ToArray());
    }

    
}
