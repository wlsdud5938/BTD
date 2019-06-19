using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;

    private Transform trans;
    public float moveSpeed = 10.0f;
    Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        moveDir = (Vector3.up * v) + (Vector3.right * h);

        trans.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);


    }
}
