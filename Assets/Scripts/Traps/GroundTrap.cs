using UnityEngine;
using System.Collections;

public class GroundTrap : TrapItem {

    public float heightDisplacement = 0.2f;

    private enum State { DOWN, STATIC, UP, DISABLED };
    private State state = State.DISABLED;

    public float timeToStop = 2;
    private float timer = 0;

    public override void RunAnimation(float delta) {

        switch (state) {
            case State.DISABLED:
                state = State.DOWN;
                break;
            //El laser baja
            case State.DOWN:
                if ((transform.localPosition.y + delta) < heightDisplacement)
                    transform.Translate(0, delta, 0);
                else {
                    transform.localPosition = new Vector3(0, heightDisplacement, 0);
                    state = State.STATIC;
                }
                break;
            //El laser está activo
            case State.STATIC:
                timer += delta;
                if (timer > timeToStop) {
                    state = State.UP;
                    timer = 0;
                }
                break;
            //El laser sube
            case State.UP:
                if ((transform.localPosition.y - delta) > 0)
                    transform.Translate(0, -delta, 0);
                else {
                    transform.localPosition = new Vector3(0, 0, 0);
                    state = State.DISABLED;
                    isEnabled = false;
                }
                break;
        }
    }

    public void OnTriggerEnter(Collider collider) {
        EnemyDamage ed = collider.GetComponent<EnemyDamage>();
        if(ed != null) {
            ed.Damage(1);
        }
    }
}
