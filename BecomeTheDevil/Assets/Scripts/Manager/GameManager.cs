using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Status playerStatus;
    public GameObject[] fieldUnit;

    [SerializeField]
    float playTime = 0.0f;
    int wave = 0;
    float waveTime = 0.0f;
    public int nextWaveTime = 15;
    bool waveOff = true;

    int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
        if(waveOff)
            waveTime += Time.deltaTime;
        if(waveTime > nextWaveTime)
        {
            wave++;
            waveTime = 0;
            waveOff = false;
        }

        if(enemyCount == 0)
        {
            waveOff = true;
        }
    }
}
