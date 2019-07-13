using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnitCreate : MonoBehaviour
{
    public GameObject target;
    public GameObject unit;
    Vector3 truePos;
    public float gridSize;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Raycast from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] allHit = Physics.RaycastAll(ray, 20.0f);
        //Here acceptableLayer is set to my Ground layer that's what I want to check against
        if (allHit.Length==1&&allHit[0].collider.CompareTag("Map"))
        {
            {
                Debug.Log(allHit[0].collider);
                int x = Mathf.FloorToInt(allHit[0].point.x / gridSize);
                int z = Mathf.FloorToInt(allHit[0].point.z / gridSize);

                unit.transform.position = new Vector3(x * gridSize, allHit[0].point.y, z * gridSize);
            }
        }



        //    // Update is called once per frame
        //    void LateUpdate()
        //{
        //    truePos.x = Mathf.Floor(Input.mousePosition.x / gridSize) * gridSize;
        //    truePos.y = 0;
        //    truePos.z = Mathf.Floor(Input.mousePosition.y / gridSize) * gridSize;

        //    unit.transform.position = truePos;
        //}

    }
}
