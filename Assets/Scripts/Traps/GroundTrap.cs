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

        /*Debug.Log(displaced);

        switch (state) {
            case State.DISABLED:
                state = State.DOWN;
                break;
            //El laser baja
            case State.DOWN:
                if (displaced < heightDisplacement) {
                    transform.Translate(0, delta, 0);
                    displaced += delta;
                }
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
                    displaced = 0;
                }
                break;
            //El laser sube
            case State.UP:
                if ((displaced - delta) > 0) {
                    transform.Translate(0, -delta, 0);
                    displaced += delta;
                }
                else {
                    transform.localPosition = new Vector3(0, 0, 0);
                    state = State.DISABLED;
                    isEnabled = false;
                }
                break;
        }*/
    }

    public void OnTriggerEnter(Collider collider) {
        EnemyDamage ed = collider.GetComponent<EnemyDamage>();
        if(ed != null) {
            ed.Damage(1);
        }
    }
}
