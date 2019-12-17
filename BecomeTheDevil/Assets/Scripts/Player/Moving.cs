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

    private PlayerHealth playerHealth;
    private Animator greenAnimator;
    private Animator whiteAnimator;
    private WhiteAttack whiteAttack;

    public int state = 0;           // 0:그린, 1:화이트, 2:그린 슬라임

    public bool isUp = false;
    public bool isDown = false;
    public bool isLeft = false;
    public bool isRight = false;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        greenAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        whiteAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        whiteAttack = transform.GetChild(1).gameObject.GetComponent<WhiteAttack>();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (state == 0)
        {
            greenAnimator.SetBool("Walking", false);
        }
        else if (state == 1)
        {
            whiteAnimator.SetBool("Walking", false);
        }

        if (Input.GetKeyDown("space"))
        {
            if(!whiteAttack.isRuneActive) changeState();            
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
                if (state == 0)
                {
                    greenAnimator.SetFloat("DirX", h);
                    greenAnimator.SetFloat("DirY", v);
                    greenAnimator.SetBool("Walking", true);
                }

                if (state == 1)
                {
                    whiteAnimator.SetFloat("DirX", h);
                    whiteAnimator.SetFloat("DirY", v);
                    whiteAnimator.SetBool("Walking", true);
                }
            }

        }



    }

    // 그린 <-> 화이트 체인지
    public void changeState()
    {
        if (state == 0)
        {
            state = 1;
            DataManager.Instance.userData_status.SetPlayingChara(1);
            CharacterPortrait.Instance.SetPannels(1);

            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(false);

            //마법진
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        else if (state == 1)
        {
            state = 0;
            DataManager.Instance.userData_status.SetPlayingChara(0);
            CharacterPortrait.Instance.SetPannels(0);

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

        // 해당 캐릭터로 체력 세팅
        playerHealth.AssignCharacter();
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
        if (other.CompareTag("Gem"))
        {
            other.GetComponent<FieldGem>().background.isClosed = true;
            Debug.Log("Destroy Gem");
            other.GetComponent<FieldGem>().die();
        }
        if(other.CompareTag("Field"))
        {
            other.GetComponent<FieldMonster>().die();
        }
        //if (other.CompareTag("Enemy"))
        //    other.GetComponent<Enemy>().die();
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

}
