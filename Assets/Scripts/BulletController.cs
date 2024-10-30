using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private int bulletDamage = 10;

    [SerializeField]
    private float bulletSpeed = 3f;

    private bool penetration;
    private Rigidbody2D rb;

    public void Initialize(bool penetration)
    {
        this.penetration = penetration;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * bulletSpeed;

        Destroy(gameObject, 5f); // auto destroy after 5 sec
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<ZombieController>().Hurt(bulletDamage);

            if (!penetration)
            {
                Destroy(gameObject);
            }
        }
    }
}
