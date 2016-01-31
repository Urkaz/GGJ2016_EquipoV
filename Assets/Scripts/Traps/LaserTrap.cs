using UnityEngine;
using System.Collections;

public class LaserTrap : TrapItem {

    private Transform laserSpawn1;
    private LineRenderer lineRenderer1;
    private Transform laserSpawn2;
    private LineRenderer lineRenderer2;

    private int raycastDistance = 10;
    private float lineDistance1;
    private float lineDistance2;

    public float timeToStop = 2;
    private float timer = 0;

    private enum State { DOWN, LASER, UP, DISABLED };
    private State state = State.DISABLED;

    public float heightDisplacement = 0.5f;

    public LayerMask layerMask;

    private ArrayList collidedEnemies;

    // Use this for initialization
    void Start() {
        laserSpawn1 = transform.Find("LaserStartPoint1");
        lineRenderer1 = transform.Find("LaserStartPoint1").GetComponent<LineRenderer>();
        lineRenderer1.enabled = false;

        laserSpawn2 = transform.Find("LaserStartPoint2");
        lineRenderer2 = transform.Find("LaserStartPoint2").GetComponent<LineRenderer>();
        lineRenderer2.enabled = false;
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
                    lineRenderer1.enabled = true;
                    lineRenderer2.enabled = true;
                    state = State.LASER;
                }
                break;
            //El laser está activo
            case State.LASER:
                lineDistance1 = raycastDistance;
                lineDistance2 = raycastDistance;

                RaycastHit hitinfo1 = new RaycastHit();
                if (Physics.Raycast(laserSpawn1.position, -transform.forward, out hitinfo1, 10000, layerMask)) {
                    if(!hitinfo1.collider.gameObject.name.Equals("Nivel1") && !hitinfo1.collider.gameObject.name.Equals("Wall"))
                        Debug.Log(hitinfo1.collider.gameObject.name);
                    //lineDistance1 = Mathf.Abs(hitinfo1.transform.position.z - (laserSpawn1.position - (transform.right * 2)).z);
                }

                RaycastHit hitinfo2 = new RaycastHit();
                if (Physics.Raycast(laserSpawn2.position, transform.forward, out hitinfo2, 10000,layerMask)) {
                    if (!hitinfo2.collider.gameObject.name.Equals("Nivel1") && !hitinfo2.collider.gameObject.name.Equals("Wall"))
                        Debug.Log(hitinfo2.collider.gameObject.name);
                    //lineDistance2 = Mathf.Abs(hitinfo2.transform.position.z - (laserSpawn2.position + (transform.right * 2)).z);
                }

                if (hitinfo2.collider != null) {
                    lineRenderer1.SetPosition(1, hitinfo2.collider.transform.position);

                }
                if (hitinfo1.collider != null) 
                    lineRenderer2.SetPosition(1, hitinfo1.collider.transform.position);
                

                if (hitinfo1.collider != null) {
                    EnemyDamage ed = hitinfo1.collider.GetComponent<EnemyDamage>();

                    if (ed != null) {
                        if (!collidedEnemies.Contains(ed)) {
                            collidedEnemies.Add(ed);
                            ed.Damage(1);
                        }
                    }
                }

                if (hitinfo2.collider != null) {
                    EnemyDamage ed = hitinfo2.collider.GetComponent<EnemyDamage>();

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
                    lineDistance1 = 0;
                    lineDistance2 = 0;
                    lineRenderer1.enabled = false;
                    lineRenderer2.enabled = false;
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
        if (laserSpawn1 != null) {
            Gizmos.color = Color.red;
            //Gizmos.DrawLine(laserSpawn.position, laserSpawn.position + new Vector3(0, 0, lineDistance * laserSpawn.forward.z));
            Gizmos.DrawSphere(laserSpawn1.position + new Vector3(0, 0, lineDistance1 * laserSpawn1.forward.z), 0.2f);
            Gizmos.DrawSphere(laserSpawn2.position + new Vector3(0, 0, lineDistance2 * laserSpawn2.forward.z), 0.2f);
        }
    }
}
