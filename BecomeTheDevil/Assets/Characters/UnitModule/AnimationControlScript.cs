using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationControlScript : MonoBehaviour
{
    public SpriteRenderer[] sprites;                            // 색 효과를 받을 스프라이트
    public SpriteRenderer shadowSprite;                        // 그림자 스프라이트

    private static Color hitColor = new Color(1, 0.7f, 0.7f);    // 피격시 스프라이트에 입혀지는 색
    private UnitInfo unitInfo;

    //Animator animator;
    
    void Start()
    {
        //animator = GetComponent<Animator>();
        unitInfo = GetComponent<UnitInfo>();
    }

    public void Hit()
    {
        for (int i = 0; i < sprites.Length; ++i) StartCoroutine(ColorChange(sprites[i]));
    }
    
    private IEnumerator ColorChange(SpriteRenderer sprite)
    {
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white;
    }

    public IEnumerator Die()
    {
        if (gameObject.CompareTag("Enemy"))
            GameManager.instance.enemyCount--;
        // 밝아진다
        for (int i = 0; i < sprites.Length; ++i) sprites[i].material.DOFloat(2, "_Brightness", 0.2f);
        yield return new WaitForSeconds(0.2f);

        // 사라진다
        for (int i = 0; i < sprites.Length; ++i) sprites[i].DOFade(0, 0.2f);
        shadowSprite.DOFade(0f, 0.2f);

        // 아이템 드랍
        for (int i = 0; i < unitInfo.dropMoney; ++i) ItemManager.Instance.ItemDrop(transform.position, 0, 50);
        for (int i = 0; i < unitInfo.dropItem.Length; ++i)
            ItemManager.Instance.ItemDrop(transform.position, unitInfo.dropItem[i].itemIndex, unitInfo.dropItem[i].itemChange);

        yield return new WaitForSeconds(1.0f);

        gameObject.SetActive(false);
    }
}
