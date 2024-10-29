using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArmController : MonoBehaviour
{
    private float timer = 0f;

    void Start()
    {
        Destroy(this.gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 0.5)
        {
            this.transform.position += new Vector3(0f, -0.004f, 0f);
        }
        else if (timer < 0.55)
        {
            this.transform.position += new Vector3(0.0004f, 0.0005f, 0f);
        }
        else
        {
            this.transform.position += new Vector3(0f, -0.0015f, 0f);
            this.transform.Rotate(new Vector3(0f, 0f, 80f * Time.deltaTime));
        }
    }
}
