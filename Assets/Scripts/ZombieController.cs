using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private float hp = 12f;

    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private float attack = 4f;

    [SerializeField]
    private Animator animator;

    private float hpThreshold;
    private bool isWalking = true;
    private bool isEating = false;

    // Start is called before the first frame update
    void Start()
    {
        hpThreshold = hp / 2;
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        // broke the arm
        if (hp <= hpThreshold && hp > 0 && isWalking)
        {
            animator.SetInteger("state", 1);
        }

        // died
        if (hp <= 0)
        {
            isWalking = false;

            if(isEating)
            {
                animator.SetInteger("state", 5);
            } 
            else
            {
                animator.SetInteger("state", 4);
            }
            
            Destroy(this.gameObject, 1.5f);
        }
    }

    // collide with food => start attacking
    public void OnCollisionEnter2D(Collision2D collision)
    {
        isWalking = false;
        isEating = true;

        if (hp > hpThreshold)
        {
            animator.SetInteger("state", 2);
        }
        else
        {
            animator.SetInteger("state", 3);
        }
    }

    // hit by food bullets
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet-food")
        {
            float dmg = 10;  // change bullet damage here
            hp -= dmg;
        }
    }
}
