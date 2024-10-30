using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArmController : MonoBehaviour
{
    private float timer = 0f;

    void Start()
    {
        Destroy(this.gameObject, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer < 0.6)
        {
            this.transform.position += new Vector3(0f, -0.4f * Time.deltaTime, 0f);
        }
        else if (timer < 0.7)
        {
            this.transform.position += new Vector3(0.07f * Time.deltaTime, 0.08f * Time.deltaTime, 0f);
        }
        else
        {
            this.transform.position += new Vector3(-0.08f * Time.deltaTime, -0.55f * Time.deltaTime, 0f);
            this.transform.Rotate(new Vector3(0f, 0f, 105f * Time.deltaTime));
        }
    }
}
