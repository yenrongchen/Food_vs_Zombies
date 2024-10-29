using System.Collections;
using UnityEngine;

public class FoodSoldier : MonoBehaviour
{
    [SerializeField]
    private FoodSoldierData statData; // the one spawner use to spawn a food

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject healthBarPrefab;

    private FoodSoldierHealthBar healthBar;
    private Animator animator;
    private int maxHp;
    private int curHp;
    private int atk;
    private float atkSpeed;
    private bool penetration;

    public void Initialize(FoodSoldierData newStat)
    {
        // get stats form the FoodSoldierData
        statData = newStat;
        maxHp = statData.stat.hp;
        curHp = maxHp;
        atk = statData.stat.atk;
        atkSpeed = statData.stat.atkSpeed;
        penetration = statData.stat.penetration;
        animator.runtimeAnimatorController = statData.animatorController;

        // spawn a health bar
        GameObject healthBarGameObject = Instantiate(healthBarPrefab);
        healthBarGameObject.transform.position = transform.position + new Vector3(-0.8f, 1, 0); // set bar above head
        healthBar = healthBarGameObject.GetComponent<FoodSoldierHealthBar>(); // get the FoodSoldierHealthBar class
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    // keep attacking
    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            animator.SetTrigger("Shoot");

            yield return StartCoroutine(Attack()); // wait until one attack finished
            yield return new WaitForSeconds(100 / atkSpeed); // wait between attacks
        }
    }

    // one attack
    private IEnumerator Attack()
    {
        int bulletCnt = atk / 10; // total number of bullets
        float interval = 0.6f / bulletCnt;

        yield return new WaitForSeconds(0.6f); // (hold the gun up)

        // shoot bullets during the shooting animation
        for (int i = 0; i < bulletCnt; i++)
        {
            FireBullet();
            yield return new WaitForSeconds(interval); // gaps between bullets
        }
    }

    // fire a bullet
    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, new Vector2(this.transform.position.x + 0.25f, this.transform.position.y + 0.2f), Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(penetration);
    }

    public void Hurt(int damage)
    {
        curHp -= damage;
        healthBar.SetHealthBar((float)curHp / maxHp);

        if (curHp <= 0)
        {
            StopCoroutine(AttackRoutine());
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.3f);
        }
    }

    /* debug
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Hurt(10);
        }
    }*/
}

