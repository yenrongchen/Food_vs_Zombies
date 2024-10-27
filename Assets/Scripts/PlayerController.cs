using System.Collections;
using System.Collections.Generic;
using UnityEngine;  

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 2f;

    [SerializeField]
    private float directionX = 0;

    [SerializeField]
    private float directionY = 0;

    [SerializeField]
    private bool isInfrontOfFood;

    private bool isTaking;

    private Animator animator;
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        isTaking = false;
        isInfrontOfFood = false;

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

        if (directionX * this.transform.localScale.x > 0)
        {
            this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
        }

        if (Input.GetKey(KeyCode.F) && isTaking == false && isInfrontOfFood == true)
        {
            isTaking = true;
        }

        if (Input.GetKey(KeyCode.G) && isTaking == true)
        {
            isTaking = false;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "RiceBox" || other.name == "MeatBox" || other.name == "VegetableBox")
        {
            isInfrontOfFood = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "RiceBox" || other.name == "MeatBox" || other.name == "VegetableBox")
        {
            isInfrontOfFood = false;
        }
    }
}
