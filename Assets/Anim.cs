using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour {


    private Animator animator;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        var vertical = Input.GetAxis("Vertical");
        var horizontal = Input.GetAxis("Horizontal");
        if (vertical > 0) animator.SetInteger("emoc", 1);
        else if (vertical < 0) animator.SetInteger("emoc", 1);
        else if (horizontal > 0) animator.SetInteger("emoc", 1);
        else if (horizontal < 0) animator.SetInteger("emoc", 1);
        else if (vertical > 0 && Input.GetKey(KeyCode.LeftShift)) animator.SetInteger("emoc", 2);
        else if (vertical < 0 && Input.GetKey(KeyCode.LeftShift)) animator.SetInteger("emoc", 2);
        else if (horizontal > 0 && Input.GetKey(KeyCode.LeftShift)) animator.SetInteger("emoc", 2);
        else if (horizontal < 0 && Input.GetKey(KeyCode.LeftShift)) animator.SetInteger("emoc", 2);

    }
    
}
