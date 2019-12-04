using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public GameObject target;
    public GameObject unit;
    Vector3 truePos;
    public float gridSize;
    

    // Update is called once per frame
    void LateUpdate()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;

        unit.transform.position = truePos;
    }
}
