using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;



public class WhiteBullet : MonoBehaviour
{
    public Vector3 bulletPos;
    public float angle;
    public float posNum;

    public bool isLoad;

    private Animator animator;

    private Sequence seq;

    IEnumerator LoadBullet()
    {
        isLoad = true;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        animator.Play("white_bullet_basic_load");
        if (angle >= -90 && angle <= 90) angle += 180;

        if(posNum ==2) Debug.Log(angle);

        Sequence seq = DOTween.Sequence();
        seq.Prepend(transform.DOMove(bulletPos, 0.8f));
        seq.Insert(0.55f, transform.DORotate(new Vector3(0, angle, 0), 0.3f));

        seq.Play();

        while (isLoad)
        {
            yield return null;
        }

        animator.Play("white_bullet_basic_shoot");
        gameObject.GetComponent<Bullet>().StartCoroutine("MoveBullet");

    }
}
