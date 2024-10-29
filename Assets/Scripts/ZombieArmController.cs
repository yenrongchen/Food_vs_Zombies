using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArmController : MonoBehaviour
{
    private int frame = 0;

    // Update is called once per frame
    void Update()
    {
        frame++;

        if (frame < 120)
        {
            this.transform.position += new Vector3(0f, -0.0035f, 0f);
        }
        else if (frame < 144)
        {
            this.transform.position += new Vector3(0.00035f, 0.0035f, 0f);
        }
        else if (frame < 200)
        {
            this.transform.position += new Vector3(0.00035f, -0.0035f, 0f);
            this.transform.Rotate(new Vector3(0f, 0f, 480f * Time.deltaTime));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
