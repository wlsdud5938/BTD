using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wizard : Enemy
{
    private Animator wizardAnimator;
    NavMeshAgent nav;
    Wizard_attack attack;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        wizardAnimator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}
