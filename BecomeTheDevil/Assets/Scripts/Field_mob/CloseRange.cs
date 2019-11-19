using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRange : MonoBehaviour
{
    FieldMonster field;
    Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        field = transform.parent.GetComponent<FieldMonster>();
        animation = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Unit"))
            Close();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player") || other.CompareTag("Unit"))
            Open();
    }

    void Open()
    {
        animation.SetBool("Open", true);
    }
    void Close()
    {
        animation.SetBool("Open", false);
    }

}
