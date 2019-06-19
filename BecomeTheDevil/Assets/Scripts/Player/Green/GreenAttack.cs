using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAttack : MonoBehaviour
{
    private Transform trans;
    public float angle;
    public GameObject bullet;
    GameObject newBullet;
    public Vector3 mousePosition;
    public float height;
    public float width;
    // Start is called before the first frame update
    void Start()
    {

        trans = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDown();
    }

    public float CalculateAngle(Vector3 to)
    {
        return Quaternion.FromToRotation(Vector3.up, to - trans.position).eulerAngles.z;
    }

    private void MouseDown()
    {

        height = Screen.height;
        width = Screen.width;
        mousePosition = new Vector3(Input.mousePosition.x-width/2, Input.mousePosition.y - height / 2,0);
        angle = CalculateAngle(mousePosition);
        if(angle>-315 && angle<=45)
        {
            newBullet = Instantiate(bullet, gameObject.transform.Find("BulletPoint").gameObject.transform.Find("up").transform);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 45 && angle <= 135)
        {
            newBullet = Instantiate(bullet, gameObject.transform.Find("BulletPoint").gameObject.transform.Find("left").transform);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 135 && angle <= 225)
        {
            newBullet = Instantiate(bullet, gameObject.transform.Find("BulletPoint").gameObject.transform.Find("down").transform);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
        else if (angle > 225 && angle <= 315)
        {
            newBullet = Instantiate(bullet, gameObject.transform.Find("BulletPoint").gameObject.transform.Find("right").transform);
            newBullet.GetComponent<Bullet>().target = mousePosition;
        }
    }


}
