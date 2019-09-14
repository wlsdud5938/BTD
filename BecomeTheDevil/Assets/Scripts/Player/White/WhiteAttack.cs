using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteAttack : MonoBehaviour
{

    /**
    private Transform trans;
    public float angle;
    public GameObject bullet;
    GameObject newBullet;
    GameObject newBullet1;
    GameObject newBullet2;
    GameObject newBullet3;
    public Vector3 mousePosition;
    public float height;
    public float width;
    public bool isGreen = true;
    Vector3 greenBulletPoint_right;
    Vector3 greenBulletPoint_up;
    Vector3 greenBulletPoint_down;
    Vector3 greenBulletPoint_left;
    Vector3 whiteBulletPoint_1;
    Vector3 whiteBulletPoint_2;
    Vector3 whiteBulletPoint_3;
    **/

    // Start is called before the first frame update
    void Start()
    {
        /**********이전 코드**************
        whiteBulletPoint_1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform.position;
        whiteBulletPoint_2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform.position;
        whiteBulletPoint_3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform.position;
        **********************************/
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    /*********************************이전 코드*********************************************
    void WhiteBulletAttack()
    {

        newBullet1 = Instantiate(bullet, whiteBulletPoint_1, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newBullet1.GetComponent<Bullet>().target = mousePosition;

        newBullet2 = Instantiate(bullet, whiteBulletPoint_2, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newBullet2.GetComponent<Bullet>().target = mousePosition;

        newBullet3 = Instantiate(bullet, whiteBulletPoint_3, Quaternion.Euler(90.0f, 0.0f, 0.0f));
        newBullet3.GetComponent<Bullet>().target = mousePosition;

    }
    ***************************************************/
}
