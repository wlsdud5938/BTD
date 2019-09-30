using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Status playerStatus;
    public GameObject[] fieldUnit;
    public GameObject[] enemyUnit;
    public GameObject enemySpawn;

    GameObject skirmisherSpawn;
    GameObject knightSpawn;
    GameObject wizardSpawn;

    [SerializeField]
    float playTime = 0.0f;
    int wave = 0;
    [SerializeField]
    float waveTime = 0.0f;
    public int nextWaveTime = 10;
    [SerializeField]
    bool waveOn = false;
    bool allSpawn = true;
    public int enemyCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        playerStatus = player.GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        playTime += Time.deltaTime;
        if(!waveOn)
            waveTime += Time.deltaTime;
        if(waveTime > nextWaveTime)
        {
            allSpawn = false;
            wave++;
            waveTime = 0;
            waveOn = true;
            StartCoroutine( EnemySpwan());
        }
        if(enemyCount == 0 && allSpawn)
        {
            waveOn = false;
        }

        
    }

    IEnumerator EnemySpwan()
    {
        int totalSpawn = wave;
        for(int i =0; totalSpawn>0; i++)
        {
            for (int j = 0; j <= Min(totalSpawn, 5) - 1; j++)
            {
               Instantiate(enemyUnit[0], enemySpawn.transform.GetChild(0).transform.GetChild(Min(totalSpawn, 5) - 1).GetChild(j).transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
               enemyCount++;
            }
            for (int j = 0; j <= Min(totalSpawn, 5) - 1; j++)
            {
                Instantiate(enemyUnit[1], enemySpawn.transform.GetChild(1).transform.GetChild(Min(totalSpawn, 5) - 1).GetChild(j).transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
                enemyCount++;
            }
            for (int j = 0; j <= Min(totalSpawn, 5) - 1; j++)
            {
                Instantiate(enemyUnit[2], enemySpawn.transform.GetChild(2).transform.GetChild(Min(totalSpawn, 5) - 1).GetChild(j).transform.position, Quaternion.Euler(90.0f, 0.0f, 0.0f));
                enemyCount++;
            }
            totalSpawn -= 5;
            if(totalSpawn <=0)
            {
                allSpawn = true;
            }
            yield return new WaitForSeconds(10.0f);
        }
    }

    int Min(int a, int b)
    {
        if (a > b)
            return b;
        return a;
    }
}
