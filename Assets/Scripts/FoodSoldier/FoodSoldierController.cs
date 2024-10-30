using System.Collections;
using UnityEngine;

public class FoodSoldier : MonoBehaviour
{
    [SerializeField]
    private float waitTimeConst = 100f;

    [SerializeField]
    private FoodSoldierData statData; // the one spawner use to spawn a food

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private GameObject healthBarPrefab;

    private Tile _tile;
    private HealthBar healthBar;
    private Animator animator;
    private float maxHp;
    private float curHp;
    private float atk;
    private float atkSpeed;
    private bool penetration;

    public void Initialize(FoodSoldierData newStat, Tile tile)
    {
        // get stats form the FoodSoldierData
        statData = newStat;
        maxHp = statData.stat.hp;
        curHp = maxHp;
        atk = statData.stat.atk;
        atkSpeed = statData.stat.atkSpeed;
        penetration = statData.stat.penetration;
        animator.runtimeAnimatorController = statData.animatorController;

        _tile = tile;

        // spawn a health bar
        GameObject healthBarGameObject = Instantiate(healthBarPrefab);
        healthBarGameObject.transform.position = transform.position + new Vector3(-0.8f, 1, 0); // set bar above head
        healthBar = healthBarGameObject.GetComponent<HealthBar>(); // get the FoodSoldierHealthBar class
    }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
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
            yield return new WaitForSeconds(waitTimeConst / atkSpeed); // wait between attacks
        }
    }

    // one attack
    private IEnumerator Attack()
    {
        int bulletCnt = (int)atk / 10; // total number of bullets
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().Initialize(penetration);
    }

    public void Hurt(float damage)
    {
        if (curHp <= 0) return;

        curHp -= damage;
        healthBar.SetHealthBar(curHp / maxHp);

        if (curHp <= 0)
        {
            StopCoroutine(AttackRoutine());
            _tile.Clear();
            animator.SetTrigger("Die");
            Destroy(gameObject, 1.3f);
        }
    }

    public void Kill()
    {
        healthBar.SetHealthBar(0);
        _tile.Clear();
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.3f);
    }

    public bool IsDead()
    {
        return curHp <= 0;
    }
}

