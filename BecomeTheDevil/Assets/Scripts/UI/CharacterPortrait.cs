using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterPortrait : MonoBehaviour
{
    public Slider hpBar;
    public Image portrait, barImage;
    public Sprite greenBar, whiteBar;
    
    public Sprite[] playerPannels;  // 0 그린 스탠딩, 1 화이트, 2 그린 슬라임

    public float currentHP;
    public float maxHP;
    public float slidingTimeHP = 0.5f;

    private GameObject player;

    public static CharacterPortrait Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
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


    public void SetPannels(int index)
    {
        // 얼굴과 바 이미지 세팅
        portrait.sprite = playerPannels[index];
        barImage.sprite = index == 1 ? whiteBar : greenBar;

        if (index == 0)
        {
            // 그린의 경우
            SetMaxHP(DataManager.Instance.userData_status.greenHealth.getMaxHP());
            SetCurrentHP(DataManager.Instance.userData_status.greenHealth.getCurrentHP());
        }
        else if (index == 1)
        {
            // 화이트의 경우
            SetMaxHP(DataManager.Instance.userData_status.whiteHealth.getMaxHP());
            SetCurrentHP(DataManager.Instance.userData_status.whiteHealth.getCurrentHP());
        }
        else if (index == 2)
        {
            // 그린 슬라임의 경우
            SetMaxHP(DataManager.Instance.userData_status.greenHealth.getMaxHP()*4);
            SetCurrentHP(DataManager.Instance.userData_status.greenHealth.getCurrentHP()*4);
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

    public void SetCurrentHP(float currentHP)
    {
        DOTween.To(() => hpBar.value, x => hpBar.value = x, currentHP, slidingTimeHP);
        //hpBar.value = currentHP;
    }
}
