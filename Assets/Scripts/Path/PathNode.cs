using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour {

    public PathNode nextNode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        if (nextNode == null)
            return;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = nextNode.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
