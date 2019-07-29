using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{

    private Transform trans;
    private Vector3 MovePos = Vector3.zero;

    // 마우스 위치
    private Vector3 mousePos;
    private float height;
    private float width;

    private float angle;

    //Green Bullet Point
    Vector3 greenBulletPoint_right;
    Vector3 greenBulletPoint_up;
    Vector3 greenBulletPoint_down;
    Vector3 greenBulletPoint_left;

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

        greenBulletPoint_right = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("right").transform.position;
        greenBulletPoint_up = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("up").transform.position;
        greenBulletPoint_down = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("down").transform.position;
        greenBulletPoint_left = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("left").transform.position;


        /************************
        trans = GetComponent<Transform>();
        greenBulletPoint_right = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("right").transform.position;
        greenBulletPoint_up = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("up").transform.position;
        greenBulletPoint_down = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("down").transform.position;
        greenBulletPoint_left = gameObject.transform.Find("GreenBulletPoint").gameObject.transform.Find("left").transform.position;

        whiteBulletPoint_1 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("1").transform.position;
        whiteBulletPoint_2 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("2").transform.position;
        whiteBulletPoint_3 = gameObject.transform.Find("WhiteBulletPoint").gameObject.transform.Find("3").transform.position;
        ********************/
    }

    // Update is called once per frame
    void Update()
    {
        trans = GetComponent<Transform>();
        KeyCheck();

        /**  기존 코드 ***
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
    **/
    }

    public float CalculateAngle(Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - trans.position).eulerAngles.z;
    }

    // 입력키 확인
    void KeyCheck()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();
    }

    void GreenBulletAttack()
    {
        BulletInfoSetting(ObjectManager.Call().GetObject("GreenBullet"));
    }

    void BulletInfoSetting(GameObject _Bullet)
    {
        
        if (_Bullet == null) return;

        if ((angle > -315 && angle <= 45) || angle > 315)
        {
            _Bullet.transform.position = transform.position + greenBulletPoint_up;
        }
        else if (angle > 45 && angle <= 135)
        {
            _Bullet.transform.position = transform.position + greenBulletPoint_left;
        }
        else if (angle > 135 && angle <= 225)
        {
            _Bullet.transform.position = transform.position + greenBulletPoint_down;
        }
        else if (angle > 225 && angle <= 315)
        {
            _Bullet.transform.position = transform.position + greenBulletPoint_right;
        }
        
        _Bullet.SetActive(true);
        _Bullet.GetComponent<Bullet>().target = mousePos;
        _Bullet.GetComponent<Bullet>().StartCoroutine("MoveBullet");
    }

    void MouseDown()
    {

        height = Screen.height;
        width = Screen.width;
        mousePos = new Vector3(Input.mousePosition.x - (width / 2), Input.mousePosition.y - (height / 2));
        angle = CalculateAngle(mousePos);

        GreenBulletAttack();

    }


    /*********************************이전 코드*********************************************
    public float CalculateAngle(Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - trans.position).eulerAngles.z;
    }

    private void MouseDown()
    {

        height = Screen.height;
        width = Screen.width;
        mousePosition = new Vector3(Input.mousePosition.x-(width/2), Input.mousePosition.y - (height / 2));
        angle = CalculateAngle(mousePosition);
        if (isGreen)
            GreenBulletAttack();
        else
            WhiteBulletAttack();

    }
    void GreenBulletAttack()
    {
        if ((angle > -315 && angle <= 45)||angle>315)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_up, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 45 && angle <= 135)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_left, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 135 && angle <= 225)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_down, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 225 && angle <= 315)
        {
            newBullet = Instantiate(bullet, greenBulletPoint_right, Quaternion.Euler(90.0f, 0.0f, 0.0f));
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
    }

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
