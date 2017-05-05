using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementPlayer : MonoBehaviour
{
    public float normalSpeed = 3;
    public int lvlRatio = 90;
    float actualSpeed;
    float fastSpeed;
    bool takeoffed = false;
    bool running;
    private Animator animator;
    int energy = 100;
    int MAXenergy = 100;
    int cooldown;
    int posi = 0;
    public Text energiaUI;
    public Text vidaUI;
    public Text gameStatus;
    public Text expUI;
    int vida = 80;
    int MAXvida = 80;
    int exp = 0;
    int lvl = 1;
    int nextLevelExp;
    int status = 1;

    void Start()
    {
        nextLevelExp = lvlRatio;
        Time.timeScale = 1;
        actualSpeed = 0;
        fastSpeed = normalSpeed + 2;
        animator = this.GetComponent<Animator>();
        animator.SetInteger("Emocion", 0);

    }
  
        


    void Update()
    {

        vidaUI.text = "Life: " + vida + "/" + MAXvida ;
        energiaUI.text = "Energy: " + energy + "/" + MAXenergy;
        if (vida == 0)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 0;
            gameStatus.color = Color.red;
            gameStatus.text = "YOU ARE DEAD";
            expUI.text = "LVL: " + lvl + " + EXP: " + exp;


        }
        else
        {
            moveChar();


        }        
        

        //Debug.Log("Y " + transform.position.y +"     PS: " + posi);
    }

    private void moveChar()
    {
        if (!takeoffed)
        {

            status = 0;
            animator.SetInteger("Emocion", status);
            actualSpeed += 1.0f * Time.deltaTime;

            if ((actualSpeed) >= (normalSpeed))
            {
                actualSpeed = normalSpeed;
                takeoffed = true;
            }
            
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && posi < 1)
        {
            MoveY(+0.8f);
            posi += 1;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow) && posi > -1)
        {
            MoveY(-0.8f);
            posi -= 1;
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.P))
        {
            exp = exp + 60;
            checkLVL();
        }


        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && energy > 7)
        {
            cooldown = 0;
            status = 2;
            actualSpeed = fastSpeed;
            //StartCoroutine(wait3secs());
            energy -= 7;
            //Debug.Log(energy);

        }
        else
        {
            //int dt = (int)Time.deltaTime;
            cooldown += 1;
            //Debug.Log(cooldown);
            //Debug.Log("dt="+cooldown);
            if(status != 3 || status == 1)
            {
                status = 1;
            }
            actualSpeed = normalSpeed;
            if ((cooldown) > 75 && ((cooldown % 6) == 1) && (energy < MAXenergy))
            {
              //  Debug.Log(cooldown);
                energy+=2;
                if(energy > MAXenergy)
                {
                    energy = MAXenergy;
                }
               // Debug.Log(energy);
                
            }


        }
        animator.SetInteger("Emocion", status);
        transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);
    }

    void MoveY(float n)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + n, transform.position.z);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            vida = 0;
           // Debug.Log("DEATH");
        }
        if (collision.gameObject.tag == "Finish")
        {
            gameStatus.color = Color.white;
            gameStatus.text = "YOU WIN";
            Time.timeScale = 0;
            expUI.text = "EXP: " + exp;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            status = 3;
            StartCoroutine(hurt());
            vida = vida - 10;
            if (vida > 0)
            {
                Destroy(collision.gameObject);
                int addXP = (lvl * (int)Mathf.Floor(lvlRatio / 4));
                exp = exp + addXP;
                //Debug.Log("+XP: " + addXP);
                checkLVL();
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if (collision.gameObject.tag == "Obstacle")
        {
            takeoffed = false;
        }
       

    }

    private void checkLVL()
    {
        if (exp >= nextLevelExp)
        {
            exp = nextLevelExp - exp;
            lvl++;
            nextLevelExp = (int)(Mathf.Pow(lvl, 1.06f) * 120);
            MAXenergy = MAXenergy + (( (int)Mathf.Log(lvl+1)) * 35);
            Debug.Log("MAXENERGY + "+ (((int)Mathf.Log(lvl+1)) * 35));
            MAXvida = MAXvida + ((lvl + (int)Mathf.Log(lvl)) * 5);
            vida = MAXvida;
        }
    }


    IEnumerator wait3secs()
    {
        yield return new WaitForSeconds(3.0f);
    }

    private IEnumerator hurt()
    {
        status = 3;
        yield return new WaitForSeconds(1.5f);
        status = 1;
       // Debug.Log("Exito" + status);
    }
}

