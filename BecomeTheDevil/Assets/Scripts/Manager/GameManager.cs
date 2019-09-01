using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Status playerStatus;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
