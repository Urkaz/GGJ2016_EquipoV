using UnityEngine;
using System.Collections;

public class GroundTrap : TrapItem {

    public float heightDisplacement = 0.2f;

    private enum State { DOWN, STATIONARY, UP, DISABLED };
    private State state = State.DISABLED;

    public float timeToStop = 2;
    private float timer = 0;

    private float displaced = 0;

    public override void RunAnimation(float delta) {

        switch (state) {
            case State.DISABLED:
                state = State.UP;
                break;
            case State.UP:
                if(displaced + delta < heightDisplacement) {
                    transform.Translate(0, delta, 0);
                    displaced += delta;
                }
                else {
                    transform.Translate(0, heightDisplacement - displaced, 0);
                    state = State.STATIONARY;
                }
                break;
            case State.STATIONARY:
                timer += delta;
                if (timer > timeToStop) {
                    state = State.DOWN;
                    timer = 0;
                    displaced = heightDisplacement;
                }
                break;
            case State.DOWN:
                if ((displaced - delta) > 0) {
                    transform.Translate(0, -delta, 0);
                    displaced -= delta;
                }
                else {
                    transform.Translate(0, -displaced, 0);
                    state = State.DISABLED;
                    isEnabled = false;
                }
                break;
        }
    }

    void OnTriggerEnter(Collider other) {
        /*if (state != State.UP || state != State.STATIONARY)
            return;*/
        EnemyDamage ed = other.GetComponent<EnemyDamage>();
        if(ed != null && isEnabled) {
            ed.Damage(1);
        }
    }
}
