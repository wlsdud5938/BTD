using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CommonAnimation : MonoBehaviour
{
    public GameObject shakingObject;                            // 흔들리는 오브젝트
    public SpriteRenderer[] sprites;                            // 색 효과를 받을 스프라이트
    public SpriteRenderer shadowSprite;                        // 그림자 스프라이트
    public float hitJumpFloat = 0.0f;                           // 피격시 튀어오르는 양

    private static Color hitColor = new Color(1, 0.7f, 0.7f);    // 피격시 스프라이트에 입혀지는 색
    private Vector3 originalPosition;

    public void Start()
    {
        originalPosition = shakingObject.transform.localPosition;
    }

    public void TestDie()
    {
        StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        // 밝아진다
        for (int i = 0; i < sprites.Length; ++i) sprites[i].material.DOFloat(2, "_Brightness", 0.3f);
        yield return new WaitForSeconds(0.3f);

        // 사라진다
        for (int i = 0; i < sprites.Length; ++i) sprites[i].DOFade(0, 0.2f);
        shadowSprite.DOFade(0f, 0.2f);
        yield return new WaitForSeconds(2.0f);




        for (int i = 0; i < sprites.Length; ++i) sprites[i].material.DOFloat(1, "_Brightness", 0);
        for (int i = 0; i < sprites.Length; ++i) sprites[i].DOFade(1, 0.2f);
        shadowSprite.DOFade(1, 0.2f);


    }

    public void hitAnimation()
    {
        for(int i = 0; i < sprites.Length; ++i) StartCoroutine(ColorChange(sprites[i]));
        ShakeObject();

        Debug.Log("hit");
    }

    private IEnumerator ColorChange(SpriteRenderer sprite)
    {
        sprite.color = hitColor;
        yield return new WaitForSeconds(0.05f);
        sprite.color = Color.white;
    }

    private void ShakeObject()
    {
        shakingObject.transform.DOLocalJump(originalPosition, hitJumpFloat, 1, 0.5f);
    }
}
