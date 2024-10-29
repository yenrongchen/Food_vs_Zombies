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
        healthBarObj = Instantiate(healthBarPrefab, this.transform.position + new Vector3(0f, 0.85f, 0f), this.transform.rotation);
        healthBarObj.GetComponent<ZombieHealthBar>().setMaxHP(maxHP);
    }

    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            this.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
            healthBarObj.transform.position -= new Vector3(speed * Time.deltaTime, 0f, 0f);
        }

        // broke the arm
        if (curHP <= maxHP / 2 && isWalking && !hasBrokenArm)
        {
            if (isEating)
            {
                animator.SetInteger("state", 3);
            }
            else 
            {
                animator.SetInteger("state", 1);
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
            
            Destroy(this.gameObject, 1.5f);
            Destroy(healthBarObj, 1f);  // both 1.5 seccond or differ?

            Instantiate(headPrefab, this.transform.position + new Vector3(0.05f, 0.15f, 0f), this.transform.rotation);
            hasBrokenHead = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "bullet-food")  // hit by food bullets
        {
            float dmg = 10;  // change bullet damage here
            curHP -= dmg;
            curHP = Mathf.Clamp(curHP, 0, maxHP);
            healthBarObj.GetComponent<ZombieHealthBar>().getDamaged(dmg);
        }
        else if (other.gameObject.tag == "food")  // start attacking
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
        if (other.gameObject.tag == "food")  // continue walking
        {
            isWalking = true;
            isEating = false;
        }
    }
}
