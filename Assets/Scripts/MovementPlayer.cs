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
    public AudioSource lvlup;
    public AudioSource grab;
    public AudioSource fight;
    public AudioSource end;
    public AudioSource death;
    public AudioSource obst;
    public AudioSource tensiona;
    public AudioSource chase2;
    

    void Start()
    {
        nextLevelExp = lvlRatio;
        Time.timeScale = 1;
        actualSpeed = normalSpeed / 5;
        fastSpeed = normalSpeed + 2;
        animator = this.GetComponent<Animator>();
        animator.SetInteger("Emocion", 0);


        chase2 = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip chaseClip = Resources.Load<AudioClip>("Chase2");
        chase2.clip = chaseClip;
        chase2.loop = true;

        grab = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip grabClip  = Resources.Load<AudioClip>("Food");
        grab.clip = grabClip;
        grab.loop = false;

        obst = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip obstClip = Resources.Load<AudioClip>("Obst");
        obst.clip = obstClip;
        obst.loop = false;

        death = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip deathClip = Resources.Load<AudioClip>("GameOver");
        death.clip = deathClip;
        death.loop = false;

        fight = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip fightClip = Resources.Load<AudioClip>("Figth");
        fight.clip = fightClip;
        fight.loop = false;

        end = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip endClip = Resources.Load<AudioClip>("End");
        end.clip = endClip;
        end.loop = false;

        lvlup = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip lvlClip = Resources.Load<AudioClip>("LVL");
        lvlup.clip = grabClip;
        lvlup.loop = false;

        tensiona = gameObject.AddComponent<AudioSource>() as AudioSource;
        AudioClip tensClip = Resources.Load<AudioClip>("tension");
        tensiona.clip = tensClip;
        tensiona.loop = true;

        chase2.Play();
    }
  
        


    void Update()
    {

        vidaUI.text = "Life: " + vida + "/" + MAXvida ;
        energiaUI.text = "Energy: " + energy + "/" + MAXenergy;
        if (vida <= 0)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 0;
            gameStatus.color = Color.red;
            gameStatus.text = "YOU ARE DEAD";
            expUI.text = "LVL: " + lvl + " + EXP: " + exp;
            chase2.Stop();
            death.Play();

        }
        else
        {
            moveChar();


        }        
        

        //Debug.Log("Y " + transform.position.y +"     PS: " + posi);
    }

    private void moveChar()
    {
        
       if(!takeoffed)
        {
            
            Debug.Log("speeding");
            status = 0;
            
            actualSpeed += 2.0f * Time.deltaTime;

            if ((actualSpeed) >= (normalSpeed))
            {
                actualSpeed = normalSpeed;
                takeoffed = true;
            }

            animator.SetInteger("Emocion", status);
        }


        else { 
        if (Input.GetKeyDown(KeyCode.UpArrow) && posi < 1)
        {
            MoveY(0.8f);
            posi += 1;
        }


        if (Input.GetKeyDown(KeyCode.DownArrow) && posi > -1)
        {
            MoveY(-0.8f);
            posi -= 1;
        }
        if (Input.GetKeyDown(KeyCode.X) && Input.GetKeyDown(KeyCode.P) && Input.GetKeyDown(KeyCode.LeftShift))
        {
            exp = exp + (int)nextLevelExp/2;
            Debug.Log("XP CHEAT: +"+(int)nextLevelExp / 2+"XP");
            checkLVL();
        }


        if ((Input.GetKey(KeyCode.Space)) && energy > 7)
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
            if ((cooldown) > 75 && ((cooldown % 6) == 1) && (energy < MAXenergy) && (Time.timeScale > 0))
            {
              //  Debug.Log(cooldown);
                energy+= (2*(int)Time.timeScale) + lvl;
                if(energy > MAXenergy)
                {
                    energy = MAXenergy;
                }
               // Debug.Log(energy);
                
            }


        }
        
        transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);

         } 
        animator.SetInteger("Emocion", status);
    }

    void MoveY(float n)
    {
        float i = transform.position.y + n;
        Vector2 going = new Vector2(transform.position.x, i);
       // Debug.Log(going.x + " and Y " + going.y);
        if (n > 0)
        {
            while (transform.position.y < i)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), going , 0.001f * Time.deltaTime);
            }
            if (transform.position.y > i)
                transform.position = new Vector2(transform.position.x, i);
        }
        if (n < 0)
        {
            while (transform.position.y > i)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), going, 0.001f * Time.deltaTime);
            }
            if (transform.position.y < i)
                transform.position = new Vector2(transform.position.x, i);
        }




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
            end.Play();
            chase2.Stop();
            Time.timeScale = 0;
            gameStatus.color = Color.white;
            gameStatus.text = "YOU WIN";
            expUI.text = "LVL: " + lvl + " + EXP: " + exp;
            tensiona.Play();

        }
        if (collision.gameObject.tag == "Enemy")
        {
            fight.Play();
            status = 3;
            StartCoroutine(hurt());
            vida = vida - ((int)MAXvida/5 ) + 1;
            if (vida > 0)
            {
                Destroy(collision.gameObject);
                int addXP = (lvl * (int)Mathf.Floor(lvlRatio / 5));
                exp = exp + addXP;
                //Debug.Log("+XP: " + addXP);
                checkLVL();
            }

        }

        if (collision.gameObject.tag == "Obstacle")
        {
            obst.Play();
            takeoffed = false;
            actualSpeed = normalSpeed / 5;
        }
        if (collision.gameObject.tag == "Food")
        {
            grab.Play();
            Destroy(collision.gameObject);
            vida = vida + MAXvida / 3;
            if (vida > MAXvida)
            {
                vida = MAXvida;
            }
            energy = MAXenergy;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    

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
            MAXvida = MAXvida + ((lvl + (int)Mathf.Log(lvl)) * 15);
            vida = MAXvida;
            lvlup.Play();
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

