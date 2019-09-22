using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    private Animator wizardAnimator;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wizardAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }


}
