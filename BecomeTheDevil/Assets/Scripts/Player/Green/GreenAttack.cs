using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
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
    Transform greenBulletPoint_right;
    Transform greenBulletPoint_up;
    Transform greenBulletPoint_down;
    Transform greenBulletPoint_left;
    Transform whiteBulletPoint_1;
    Transform whiteBulletPoint_2;
    Transform whiteBulletPoint_3;

    // Start is called before the first frame update
    void Start()
    {

        trans = GetComponent<Transform>();
        greenBulletPoint_right = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("right").transform;
        greenBulletPoint_up = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("up").transform;
        greenBulletPoint_down = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("down").transform;
        greenBulletPoint_left = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("left").transform;

        whiteBulletPoint_1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform;
        whiteBulletPoint_2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform;
        whiteBulletPoint_3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();
        if(Input.GetKey(KeyCode.Space))
        {
            if (isGreen)
            {
                isGreen = false;
            }
            else
                isGreen = true;
        }
    }

    public float CalculateAngle(Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - trans.position).eulerAngles.z;
    }

    private void MouseDown()
    {

        height = Screen.height;
        width = Screen.width;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = CalculateAngle(mousePosition);
        if (isGreen)
            GreenBulletAttack();
        else
            WhiteBulletAttack();

    }
    void GreenBulletAttack()
    {
        if (angle > -315 && angle <= 45)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_up);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 45 && angle <= 135)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_left);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 135 && angle <= 225)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_down);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 225 && angle <= 315)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_right);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
    }

    void WhiteBulletAttack()
    {

        newBullet1 = Instantiate(bullet, whiteBulletPoint_1);
        newBullet1.GetComponent<Bullet>().target = mousePosition;

        newBullet2 = Instantiate(bullet, whiteBulletPoint_2);
        newBullet2.GetComponent<Bullet>().target = mousePosition;

        newBullet3 = Instantiate(bullet, whiteBulletPoint_3);
        newBullet3.GetComponent<Bullet>().target = mousePosition;

    }

}
