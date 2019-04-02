﻿using System.Collections.Generic;
using Assets.Utils;
using UnityEngine;

public class CarGenerator : MonoBehaviour
{
    private readonly int yLevel = 3;
    public GameObject CarInstance;
    public GameObject[] TranmissionObjects;
    public int TransmissionCount = 4;
    public int WheelCount = 4;
    public GameObject[] WheelObjects;


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 70, 30), "Generate"))
        {
            var carMesh = new CarMesh();
            var tramsmissions = LoadDetails(DetailType.Transmission, TransmissionCount);
            CarBuilder.StickTramsmissions(tramsmissions, carMesh, yLevel);
            var wheels = LoadDetails(DetailType.Wheel, WheelCount);
            var wheelPlaces = carMesh.FindWheelsPlaces(wheels);
            CarBuilder.StickWheels(wheels, wheelPlaces);
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
            else
                randomDetail = new GameObject();
            var prefab = Instantiate(randomDetail, randomDetail.transform.position, Quaternion.identity,
                CarInstance.transform);
            details.Add(prefab);
        }

        return details;
    }

    private enum DetailType
    {
        Transmission,
        Wheel
    }
}