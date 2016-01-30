using UnityEngine;
using System.Collections;

public class LaserTrap : TrapItem {

    private Transform laserSpawn;
    LineRenderer lineRenderer;

    private int raycastDistance = 10;
    private float lineDistance;

    public float timeToStop = 2;
    private float timer = 0;

    private enum State { DOWN, LASER, UP, DISABLED };
    private State state = State.DISABLED;

    public float heightDisplacement = 0.5f;

    private ArrayList collidedEnemies;

    // Use this for initialization
    void Start() {
        laserSpawn = transform.Find("LaserStartPoint");
        lineRenderer = transform.Find("LaserStartPoint").GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    public override void RunAnimation(float delta) {
        switch (state) {
            case State.DISABLED:
                collidedEnemies = new ArrayList();
                state = State.DOWN;
                break;
            //El laser baja
            case State.DOWN:

                if ((transform.localPosition.y - delta) > -heightDisplacement)
                    transform.Translate(0, -delta, 0);
                else {
                    transform.localPosition = new Vector3(0, -heightDisplacement, 0);
                    lineRenderer.enabled = true;
                    state = State.LASER;
                }
                break;
            //El laser está activo
            case State.LASER:
                lineDistance = raycastDistance;

                RaycastHit hitinfo = new RaycastHit();
                if (Physics.Raycast(laserSpawn.position, laserSpawn.forward, out hitinfo, raycastDistance)) {
                    lineDistance = Mathf.Abs(hitinfo.point.z - laserSpawn.position.z);
                }

                lineRenderer.SetPosition(1, new Vector3(0, 0, lineDistance));

                if (hitinfo.collider != null) {
                    EnemyDamage ed = hitinfo.collider.GetComponent<EnemyDamage>();

                    if (ed != null) {
                        if (!collidedEnemies.Contains(ed)) {
                            collidedEnemies.Add(ed);
                            ed.Damage(1);
                        }
                    }
                }

                timer += delta;
                if (timer > timeToStop) {
                    state = State.UP;
                    timer = 0;
                    lineDistance = 0;
                    lineRenderer.enabled = false;
                }
                break;
            //El laser sube
            case State.UP:
                if ((transform.localPosition.y + delta) < 0)
                    transform.Translate(0, delta, 0);
                else {
                    transform.localPosition = new Vector3(0, 0, 0);
                    state = State.DISABLED;
                    isEnabled = false;
                }
                break;
        }
    }

    void OnDrawGizmos() {
        if (laserSpawn != null) {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(laserSpawn.position, laserSpawn.position + new Vector3(0, 0, lineDistance * laserSpawn.forward.z));
            Gizmos.DrawSphere(laserSpawn.position + new Vector3(0, 0, lineDistance * laserSpawn.forward.z), 0.2f);
        }
    }
}
