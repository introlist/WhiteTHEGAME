using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public float normalSpeed = 3;
    float actualSpeed;
    float fastSpeed;
    bool takeoffed = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!takeoffed)
        {


            actualSpeed += 1.5f * Time.deltaTime;

            if ((actualSpeed) >= (normalSpeed + 2.0f))
            {
                actualSpeed = normalSpeed;
                takeoffed = true;
            }
        }
        transform.Translate(Vector2.right * actualSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        Destroy(collision.gameObject);
    }
}