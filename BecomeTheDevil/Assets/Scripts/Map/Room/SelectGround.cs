using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGround : MonoBehaviour
{
    public List<GameObject> groundList = new List<GameObject>();
    public int[] pos = new int[2];
    // Start is called before the first frame update
    void Awake()
    {
        int random = Random.Range(0, groundList.Count);
        GameObject child = Instantiate(groundList[random], transform) as GameObject;
        child.transform.parent = transform;
    }
}
