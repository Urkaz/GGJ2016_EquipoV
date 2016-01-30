using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public float movementSpeed = 5f;
    public float distanceForNextNode = 2f;

    private PathNode mNode;
    private bool mStartMoving = false;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (mStartMoving)
        {
            CheckNextNode();
            MoveEnemy();
        }
	}

    private void MoveEnemy()
    {
        if (mNode == null)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 movementVector = mNode.transform.position - transform.position;
        movementVector.y = 0;
        movementVector.Normalize();
        movementVector *= movementSpeed * Time.deltaTime;
        transform.position += movementVector;
    }
    private void CheckNextNode()
    {
        if (Vector3.Distance(transform.position, mNode.transform.position) < movementSpeed * Time.deltaTime + distanceForNextNode)
            mNode = mNode.nextNode;
    }

    public void StartMoving(PathNode node)
    {
        mStartMoving = true;
        mNode = node;
    }
}
