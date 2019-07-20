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
        greenAnimator.SetBool("Walking", false);

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
            Debug.Log("aa");
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");

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
}
