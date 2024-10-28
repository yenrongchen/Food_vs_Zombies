using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*public enum Food
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
}*/

public class PlayerController : MonoBehaviour
{
    const int SUCCESS_BUT_DONT_DESTORY = 1;
    const int FAILED = 0;
    const int SUCCESS = 2;

    [SerializeField]
    private float walkSpeed = 2f;

    private float directionX = 0;

    private float directionY = 0;

    [SerializeField]
    private bool isTaking;

    [SerializeField]
    private GameObject CurrentTaking;

    private Animator animator;
    private Rigidbody2D rbody2D;

    // Start is called before the first frame update
    void Start()
    {
        isTaking = false;
        CurrentTaking = null;

        rbody2D = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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
        if (other.gameObject.tag == "CanTake")  //  taking Ingredients from boxes
        {
            if (Input.GetKey(KeyCode.Mouse0) && isTaking == false)
            {
                CurrentTaking = other.gameObject.GetComponent<IngredientsController>().GetItem();
            }
        }
        else if (other.name == "TrashCan")  //  throw away to TrashCan
        {
            if (Input.GetKey(KeyCode.Mouse1) && isTaking == true)
            {
                isTaking = false;
                Destroy(CurrentTaking);
                CurrentTaking = null;

                return;
            }
        }
        else if (other.gameObject.tag == "CanBoth" && other.name != "CombineArea")  //  put Food to cook areas
        {
            if (Input.GetKey(KeyCode.Mouse1) && isTaking == true && other.gameObject.GetComponent<CookingAreaController>().IsEmpty() 
                && !other.gameObject.GetComponent<CookingAreaController>().IsWorking())
            {
                if ( other.gameObject.GetComponent<CookingAreaController>().StartWorking(CurrentTaking) )
                {
                    isTaking = false;
                    Destroy(CurrentTaking);
                    CurrentTaking = null;

                    return;
                }
            }                                                                     //  take food from cook areas
            else if(Input.GetKey(KeyCode.Mouse0) && isTaking == false && !other.gameObject.GetComponent<CookingAreaController>().IsEmpty()
                && !other.gameObject.GetComponent<CookingAreaController>().IsWorking())
            {
                CurrentTaking = other.gameObject.GetComponent<CookingAreaController>().GetItem();
                if (CurrentTaking != null)
                {
                    other.gameObject.GetComponent<CookingAreaController>().SetCurrentOfferingToNullAfterGetItem();
                }
            }
        }
        else if(other.name == "CombineArea")
        {
            if (Input.GetKey(KeyCode.Mouse1) && isTaking == true)
            {
                int state = other.gameObject.GetComponent<CombineAreaController>().StartWorking(CurrentTaking);
                if (state == SUCCESS)
                {
                    isTaking = false;
                    Destroy(CurrentTaking);
                    CurrentTaking = null;

                    return;
                }
                else if(state == SUCCESS_BUT_DONT_DESTORY)
                {
                    isTaking = false;
                    CurrentTaking = null;

                    return;
                }
            }
            else if (Input.GetKey(KeyCode.Mouse0) && isTaking == false)
            {
                CurrentTaking = other.gameObject.GetComponent<CombineAreaController>().GetItem();
                if (CurrentTaking != null)
                {
                    other.gameObject.GetComponent<CombineAreaController>().SetCurrentOfferingToNullAfterGetItem();
                }
            }
        }


        if(CurrentTaking != null && isTaking == false)
        {
            isTaking = true;
        }
    }

}
