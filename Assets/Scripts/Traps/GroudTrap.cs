using UnityEngine;
using System.Collections;

public class GroudTrap : TrapItem {

    public float heightDisplacement = 0.2f;
    private int direction = 1;


    void Start() {
    }

    public override void RunAnimation(float delta) {
        if (direction == 1) {
            if ((transform.localPosition.y + delta) < heightDisplacement)
                transform.Translate(0, delta, 0);
            else {
                transform.localPosition = new Vector3(0, heightDisplacement, 0);
                direction = -1;
            }
        }
        else {
            if ((transform.localPosition.y - delta) > 0)
                transform.Translate(0, -delta, 0);
            else {
                transform.localPosition = new Vector3(0, 0, 0);
                direction = 1;
                isEnabled = false;
            }
        }
    }

    public void OnTriggerEnter(Collider collider) {
        EnemyDamage ed = collider.GetComponent<EnemyDamage>();
        if(ed != null) {
            ed.Damage(1);
        }
    }
}
