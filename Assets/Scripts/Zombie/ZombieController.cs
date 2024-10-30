using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 100f;

    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private float attack = 4f;

    [SerializeField]
    private GameObject healthBarPrefab;

    [SerializeField]
    private GameObject armPrefab;

    [SerializeField]
    private GameObject headPrefab;

    private GameObject healthBarObj;
    private Animator animator;
    private float curHP;
    private bool isWalking = true;
    private bool isEating = false;
    private bool hasBrokenArm = false;
    private bool hasBrokenHead = false;

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        healthBarObj = Instantiate(healthBarPrefab, this.transform.position + new Vector3(-0.1f, 0.75f, 0f), this.transform.rotation);
        healthBarObj.GetComponent<ZombieHealthBar>().setMaxHP(maxHP);
    }

    void Start()
    {
        curHP = maxHP;
    }

    void Update()
    {
        // moving
        if (isWalking)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        // update health bar position
        healthBarObj.transform.position = this.transform.position + new Vector3(-0.1f, 0.75f, 0f);

        // broke the arm
        if (curHP <= maxHP / 2 && !hasBrokenArm)
        {
            if (isEating)
            {
                animator.SetInteger("state", 3);  // broken_eat
            }
            else 
            {
                animator.SetInteger("state", 1);  // broken_walk
            }
            
            Instantiate(armPrefab, this.transform.position + new Vector3(0.05f, 0.05f, 0f), this.transform.rotation);
            hasBrokenArm = true;
        }

        // died
        if (curHP == 0 && !hasBrokenHead)
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

            Destroy(GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2f);
            Destroy(healthBarObj, 2f);

            Instantiate(headPrefab, this.transform.position + new Vector3(-0.05f, 0.21f, 0f), this.transform.rotation);
            hasBrokenHead = true;
        }

        if (this.transform.position.x < -5.15)
        {
            // TODO: Game over
        }
    }

    // hit by food bullets
    public void Hurt(float dmg)
    {
        if (this.transform.position.x <= 8.8)
        {
            curHP -= dmg;
            curHP = Mathf.Clamp(curHP, 0, maxHP);
            healthBarObj.GetComponent<ZombieHealthBar>().getDamaged(dmg);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "FoodSoldier")  // start attacking
        {
            isWalking = false;
            isEating = true;

            if (curHP > maxHP / 2)  // eat
            {
                animator.SetInteger("state", 2);
            }
            else  // broken_eat
            {
                animator.SetInteger("state", 3);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "FoodSoldier")  // continue walking
        {
            isWalking = true;
            isEating = false;
        }
    }
}
