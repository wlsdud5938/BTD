using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomActive : MonoBehaviour
{
    public int ran;
    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(1, 9);
        if((ran&1) == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if ((ran & 2) == 2)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        if ((ran & 4) == 4)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        if ((ran & 8) == 8)
        {
            transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
