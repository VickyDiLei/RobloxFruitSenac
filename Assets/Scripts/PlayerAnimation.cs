using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    CharacterController cc;
    Animator animator;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {

        if( PlayerJump.estadoPulo != EstadoPulo.Solo)
        {
            animator.SetBool("estaPulando", true);
            //animator.SetBool("estaCorrendo", false);
            return;
        }

        animator.SetBool("estaPulando", false);

        if( cc.velocity.magnitude > 0.01f)
        {
            animator.SetBool("estaCorrendo", true);
        }
        else
        {
            animator.SetBool("estaCorrendo", false);
        }

    }
}
