using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHeadController : MonoBehaviour
{
    private float offsetX;
    private float offsetY;
    private float newX;
    private float timer = 0f;

    void Start()
    {
        offsetX = this.transform.position.x;
        offsetY = this.transform.position.y;
        newX = 0f;
        Destroy(this.gameObject, 2f);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 1.5)
        {
            newX += 0.6f * Time.deltaTime;
            float newY = -1 * newX * newX;
            transform.position = new Vector3(newX + offsetX, newY + offsetY, 0);
        }
        else if (timer < 1.65)
        {
            this.transform.position += new Vector3(0.002f, 0.001f, 0f);
        }
        else if (timer < 1.8)
        {
            this.transform.position += new Vector3(0.002f, -0.001f, 0f);
        }
        else
        {
            this.transform.position += new Vector3(0.002f, 0f, 0f);
        }

        this.transform.Rotate(new Vector3(0f, 0f, -60f * Time.deltaTime));
    }
}
