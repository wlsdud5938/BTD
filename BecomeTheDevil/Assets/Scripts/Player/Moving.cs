using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;

    private Transform trans;
    public float moveSpeed = 10.0f;
    Vector3 moveDir;

    private Animator greenAnimator;
    private Animator whiteAnimator;

    public string state = "green";

    bool isUp = false;
    bool isDown = false;
    bool isLeft = false;
    bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        greenAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        whiteAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "green")
        {
            greenAnimator.SetBool("Walking", false);
        }
        else if (state == "white")
        {
            whiteAnimator.SetBool("Walking", false);
        }

        if (Input.GetKeyDown("space"))
        {

            changeState(state);


            if (state == "green")
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(false);
            }
            if (state == "white")
            {
                transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(0).gameObject.SetActive(false);
            }


        }


        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            //Debug.Log("aa");
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            if (isUp && v > 0)
                v = 0;
            if (isDown && v < 0)
                v = 0;
            if (isLeft && h < 0)
                h = 0;
            if (isRight && h > 0)
                h = 0;
            if (h != 0 || v != 0)
            {
                moveDir = (Vector3.up * v) + (Vector3.right * h);

                trans.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
                trans.position.Set(trans.position.x, 0, trans.position.z);
                if (state == "green")
                {
                    greenAnimator.SetFloat("DirX", h);
                    greenAnimator.SetFloat("DirY", v);
                    greenAnimator.SetBool("Walking", true);
                }

                if (state == "white")
                {
                    whiteAnimator.SetFloat("DirX", h);
                    whiteAnimator.SetFloat("DirY", v);
                    whiteAnimator.SetBool("Walking", true);
                }
            }

        }



    }

    void changeState(string state)
    {
        if (state == "green")
        {
            this.state = "white";
        }

        else if (state == "white")
        {
            this.state = "green";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("UpWall"))
            isUp = true;
        if (other.CompareTag("DownWall"))
            isDown = true;
        if (other.CompareTag("LeftWall"))
            isLeft = true;
        if (other.CompareTag("RightWall"))
            isRight = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.tag);
        if (other.CompareTag("UpWall"))
            isUp = false;
        if (other.CompareTag("DownWall"))
            isDown = false;
        if (other.CompareTag("LeftWall"))
            isLeft = false;
        if (other.CompareTag("RightWall"))
            isRight = false;
    }


}
