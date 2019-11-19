using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPortrait : MonoBehaviour
{
    public GameObject player;

    public string currentPlayerState;
    private string playerState;

    public Sprite[] playerPannels;

    // Start is called before the first frame update
    void Start()
    {
        playerState = "green";
        //currentPlayerState = "green";

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerState = player.GetComponent<Moving>().state;
        if (playerState != currentPlayerState) SetPannels();
    }


    void SetPannels()
    {
        Debug.Log("it is Done");
        playerState = currentPlayerState;
        if (playerState == "green")
        {
            gameObject.GetComponent<Image>().sprite = playerPannels[0];
        }
        else if (playerState == "white") {
            gameObject.GetComponent<Image>().sprite = playerPannels[1];
        }
        else {
            gameObject.GetComponent<Image>().sprite = playerPannels[2];
        }
    }
}
