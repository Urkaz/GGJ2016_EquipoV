using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    public enum Direction {
        Left,
        Right
    }


    public GameObject sprite;
    public Animator animator;
    private Direction direction = Direction.Left;
    private Direction prevDrection = Direction.Left;

    private EnemySpawn spawner;

    public float movementSpeed = 5f;
    public float distanceForNextNode = 0.2f;
    public float baseHitDistance = 2f;
    public float jumpForceV = 5f;
    public float jumpForceH = 5f;
    public LayerMask ignoreLayers;

    private CameraRotation camera;
    private PathNode mNode;
    private bool mStartMoving = false;

    private bool littleJump = false;
    private bool recover = true;
    private bool isOverFloor = true;
    private Rigidbody enemyRigidbody;

    private float timerToDestroyIfStopped = 0.5f;
    private float timerDestroy = 0f;
    private float relativeSpeed = 1f;
    private Vector3 previousPosition = new Vector3(), actualPosition = new Vector3(), directionVector = new Vector3();

    // Use this for initialization
    void Start() {
        enemyRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        LookAtCamera();

        if (mStartMoving) {
            previousPosition = transform.position;
            SetDirection();
            CheckNextNode();
            MoveEnemy();

            actualPosition = transform.position;
            directionVector = actualPosition - previousPosition;
            relativeSpeed = (actualPosition - previousPosition).magnitude;
            if (isOverFloor && relativeSpeed == 0) {
                timerDestroy += Time.deltaTime;
                if (timerDestroy >= timerToDestroyIfStopped) {
                    spawner.destroyEnemy(gameObject);
                    Destroy(this.gameObject);
                }
            }
            else
                timerDestroy = 0f;
        }
    }

    void FixedUpdate() {
        if (!Physics.Raycast(transform.position, -transform.up, baseHitDistance, ignoreLayers)) {
            littleJump = true;
            isOverFloor = false;
            animator.SetState(Animator.State.JUMP);
            //return;
        }
        else {
            recover = true;
            isOverFloor = true;
            animator.SetState(Animator.State.WALK);
        }

        Jump();
    }

    private void Jump() {

        if (littleJump && recover) {
            Vector3 movementVector = mNode.transform.position - transform.position;

            Vector3 jumpDirection = new Vector3(0, jumpForceV, 0);

            if (Mathf.Abs(movementVector.x) > Mathf.Abs(movementVector.z)) {
                jumpDirection.z = 0;
                if (movementVector.x < 0)
                    jumpDirection.x = -1 * jumpForceH;
                else
                    jumpDirection.x = 1 * jumpForceH;
            }
            else {
                if (movementVector.z < 0)
                    jumpDirection.z = -1 * jumpForceH;
                else
                    jumpDirection.z = 1 * jumpForceH;
                jumpDirection.x = 0;
            }

            //jumpDirection.Normalize();
            //jumpDirection.y *= jumpForceV;
            //Debug.Log(jumpDirection);
            enemyRigidbody.AddForce(jumpDirection);
            littleJump = false;
            recover = false;
        }
    }

    private void SetDirection() {
        directionVector.y = 0;
        directionVector.Normalize();

        float angle = Vector3.Angle(camera.transform.right, directionVector);
        //Debug.Log(angle);
        if (angle < 90f) {
            direction = Direction.Right;
        }
        else
            direction = Direction.Left;

        if (prevDrection != direction) {
            prevDrection = direction;
            Vector3 theScale = sprite.transform.localScale;
            theScale.x *= -1;
            sprite.transform.localScale = theScale;
        }
    }

    private void MoveEnemy() {
        if (mNode == null) {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().Damage();
            spawner.destroyEnemy(gameObject);
            Destroy(this.gameObject);
            return;
        }
        if (!isOverFloor)
            return;

        Vector3 movementVector = mNode.transform.position - transform.position;

        movementVector.y = 0;
        movementVector.Normalize();
        movementVector *= movementSpeed * Time.deltaTime;
        transform.position += movementVector;
    }

    private void LookAtCamera() {
        //stransform.LookAt(camera.transform);
        Vector3 rotation = new Vector3();
        switch (camera.GetCardinalPosition()) {
            case CardinalPosition.North:
                rotation.y = 270;
                break;
            case CardinalPosition.South:
                rotation.y = 90;
                break;
            case CardinalPosition.East:
                rotation.y = 0;
                break;
            case CardinalPosition.West:
                rotation.y = 180;
                break;
        }
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void CheckNextNode() {
        if (Vector3.Distance(transform.position, mNode.transform.position) < distanceForNextNode)
            mNode = mNode.nextNode;
    }
    private void ForceNextNode() {
        mNode = mNode.nextNode;
    }
    public void StartMoving(PathNode node, CameraRotation camera, EnemySpawn spawner) {
        this.spawner = spawner;
        this.camera = camera;
        mStartMoving = true;
        mNode = node;
    }

    public EnemySpawn GetSpawner() {
        return spawner;
    }
}
