using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadController : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private float newX;
    private int frame = 0;

    void Start()
    {
        offsetX = this.transform.position.x;
        offsetY = this.transform.position.y;
        newX = 0f;
        Destroy(this.gameObject, 2.2f);
    }

    void Update()
    {
        if (frame < 240)
        {
            newX += 0.8f * Time.deltaTime;
            float newY = -1 * newX * newX;
            transform.position = new Vector3(newX + offsetX, newY + offsetY, 0);
        }
        else if (frame < 256)
        {
            this.transform.position += new Vector3(0.00035f, 0.0035f, 0f);
        }
        else if (frame < 272)
        {
            this.transform.position += new Vector3(0.00035f, -0.0035f, 0f);
        }
        else
        {
            this.transform.position += new Vector3(0.00035f, 0f, 0f);
        }

        this.transform.Rotate(new Vector3(0f, 0f, -60f * Time.deltaTime));

        frame++;
    }
}
