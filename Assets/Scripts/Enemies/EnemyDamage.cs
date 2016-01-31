using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    public int hits = 2;

    private float damageTimer = 0f;

    public virtual void Damage(int amount) {
        Debug.Log("damaged");
        damageTimer = 2f;
        if (hits - amount <= 0) {
            GetComponent<EnemyMovement>().GetSpawner().destroyEnemy(gameObject);
            Destroy(gameObject);
        }
        else
            hits -= amount;

    }



    public void Update() {
        if (damageTimer > 0) {
            damageTimer -= Time.deltaTime;
            GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
        }
        else
            GetComponentInChildren<SpriteRenderer>().color = Color.black;

    }
}
