using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float normalSpeed = 3;
    float actualSpeed;
    float fastSpeed;
    private Animator animator;
    bool takeoffed = false;
    int energy = 100;
    float cooldown;
    int posi = 0;

    void Start()
    {
        actualSpeed = 0;
        fastSpeed = normalSpeed + 2;
        animator = this.GetComponent<Animator>();
        animator.SetInteger("Emocion", 0);

    }
  
        


    void Update()
    {
        if (!takeoffed)
        {
            
            
            actualSpeed += 1.0f * Time.deltaTime;

            if ((actualSpeed) >= (normalSpeed)) {
                actualSpeed = normalSpeed;
                takeoffed = true;
                    }
        }
        if (Input.GetKey(KeyCode.UpArrow) && posi < 1)
        {
            MoveY(+0.8f);
            posi += 1;
        }


        if (Input.GetKey(KeyCode.DownArrow) && posi > -1)
        {
            MoveY(-0.8f);
            posi -= 1;
        }

        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetInteger("Emocion", 2);
            actualSpeed = fastSpeed;
                
        }
        else {
            cooldown += 1.0f * Time.deltaTime;
            animator.SetInteger("Emocion", 1);
            if (0 < (cooldown) && (cooldown%10) < 1) {
                energy++;
                Debug.Log(energy);
                Debug.Log(cooldown);
            }


        }
        transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);

        Debug.Log("Y " + transform.position.y +"     PS: " + posi);
    }

    void MoveY(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + n, transform.position.z);
    }
}

