using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterPortrait : MonoBehaviour
{
    public GameObject player;

    public Slider hpBar;

    public string currentPlayerState;
    private string playerState;

    public Sprite[] playerPannels;  // 0 그린 스텐딩, 1 화이트, 2 그린 슬라임

    public float currentHP;
    public float maxHP;
    public float slidingTimeHP = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerState = "green";
   
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerState = player.GetComponent<Moving>().state;
        if (playerState != currentPlayerState) SetPannels();

        if(maxHP != player.GetComponent<Health>().maxHP)
        {
            maxHP = player.GetComponent<Health>().maxHP;
            SetMaxHP(maxHP);
        }
        if (currentHP != player.GetComponent<Health>().currentHP)
        {
            currentHP = player.GetComponent<Health>().currentHP;
            SetCurrentHP(currentHP);
        }
    }


    void SetPannels()
    {
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

    void SetMaxHP(float maxHP)
    {
        DOTween.To(() => hpBar.GetComponentInChildren<RectTransform>().sizeDelta,
            x => hpBar.GetComponentInChildren<RectTransform>().sizeDelta = x,
            new Vector2(maxHP * 2, 20f), slidingTimeHP);
        DOTween.To(() => hpBar.maxValue, x => hpBar.maxValue = x, maxHP, slidingTimeHP);
        //hpBar.GetComponentInChildren<RectTransform>().sizeDelta = new Vector3(maxHP * 2, 20f);
        //hpBar.maxValue = maxHP;
    }

    void SetCurrentHP(float currentHP)
    {
        DOTween.To(() => hpBar.value, x => hpBar.value = x, currentHP, slidingTimeHP);
        //hpBar.value = currentHP;
    }
}
