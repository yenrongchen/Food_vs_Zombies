using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Food
{
    none,
    rice,
    meat,
    vege,
    cookedRice,
    cookedMeat,
    cookedVege,
    choppedMeat,
    choppedVege,
    cookedChoppedMeat,
    cookedChoppedVege,
    salad,
    sashimi,
    riceBall,
    vegeMealBox,
    friedMeatVege,
    friedRice
}

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2f;

    private float directionX = 0;

    private float directionY = 0;

    private bool isTaking;

    [SerializeField]
    private Food currentTaking;

    private Animator animator;
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        isTaking = false;
        currentTaking = Food.none;

        rbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // player movement
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");
        rbody2D.velocity = new Vector2(directionX * walkSpeed, directionY * walkSpeed);

        // reflect the sprites when switching direction
        if (directionX * this.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }

        // player animation
        if (isTaking == false && (directionX != 0 || directionY != 0))  // turn to walk
        {
            animator.SetInteger("PlayerState", 1);
        }
        else if (isTaking == true && directionX == 0 && directionY == 0)  // turn to take
        {
            animator.SetInteger("PlayerState", 2);
        }
        else if (isTaking == true && (directionX != 0 || directionY != 0))  // turn to take & walk
        {
            animator.SetInteger("PlayerState", 3);
        }
        else                                                                // turn to stand
        {
            animator.SetInteger("PlayerState", 0);
        }

        // make sure player dont tilt because of rbody2D
        this.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // enable take & discard function
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "CanTake")  //  taking rice from RiceBox
        {
            if (Input.GetKey(KeyCode.Mouse0) && isTaking == false)
            {
                currentTaking = other.gameObject.GetComponent<IngredientsController>().GetItem();
            }
        }
        else if (other.name == "TrashCan")  //  throw away to TrashCan
        {
            if (Input.GetKey(KeyCode.Mouse1) && isTaking == true)
            {
                isTaking = false;
                currentTaking = Food.none;
                GameObject.Find("SpawnPoint").GetComponent<SpawnPointController>().DestoryItem();

                return;
            }
        }
        else if (other.name == "CookingAreaRice")  //  putting rice into CookingAreaRice
        {
            if (Input.GetKey(KeyCode.Mouse1) && currentTaking == Food.rice && other.gameObject.GetComponent<CookingAreaRiceController>().IsEmpty() 
                && !other.gameObject.GetComponent<CookingAreaRiceController>().IsWorking())
            {
                other.gameObject.GetComponent<CookingAreaRiceController>().StartWorking();

                isTaking = false;
                currentTaking = Food.none;

                return;
            }
        }


        if(currentTaking != Food.none && isTaking == false)
        {
            isTaking = true;
            GameObject.Find("SpawnPoint").GetComponent<SpawnPointController>().SpawnItem(currentTaking);
        }
    }

    // disable take & discard function
    private void OnTriggerExit2D(Collider2D other)
    {
    }
}
