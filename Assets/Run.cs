using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    private bool isRun = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            transform.Rotate(new Vector3(0, 1, 0));
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 20, 70, 30), "Rotate"))
        {
            if (!isRun) isRun = true;
            else isRun = false;
        }
    }
}