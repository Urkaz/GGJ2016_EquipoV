using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float maxSpeed = 50f;
    public float acceleration = 10f;

    public CameraRotation camera;
    private CardinalPosition mCardinalPosition;

    //Walls
    public Transform northWall;
    public Transform southWall;
    public Transform eastWall;
    public Transform westWall;

    private Rigidbody objectRigidbody;

    // Use this for initialization
    void Start () {
        objectRigidbody = GetComponent<Rigidbody>();
        mCardinalPosition = camera.GetPreviousCardinalPosition();
        ChangePosition(mCardinalPosition);
	}
	
	// Update is called once per frame
	void Update () {
        CardinalPosition cameraCardinalPosition = camera.GetPreviousCardinalPosition();

	    if(mCardinalPosition != cameraCardinalPosition)
        {
            mCardinalPosition = cameraCardinalPosition;
            ChangePosition(mCardinalPosition);
            objectRigidbody.velocity = new Vector3();
        }
	}

    void FixedUpdate()
    {
        PlayerMovement();
    }

    private void ChangePosition(CardinalPosition cardinalPosition)
    {
        if(cardinalPosition == CardinalPosition.North || cardinalPosition == CardinalPosition.South)
        {
            objectRigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
        else
            objectRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        Vector3 newPlayerPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        switch (cardinalPosition)
        {
            case CardinalPosition.North:
                newPlayerPosition.x = northWall.position.x;
                break;
            case CardinalPosition.South:
                newPlayerPosition.x = southWall.position.x;
                break;
            case CardinalPosition.East:
                newPlayerPosition.z = eastWall.position.z;
                break;
            case CardinalPosition.West:
                newPlayerPosition.z = westWall.position.z;
                break;
        }

        transform.position = newPlayerPosition;
    }

    private void PlayerMovement()
    {
        if (objectRigidbody.velocity.magnitude > maxSpeed)
            return;

        Vector3 force = new Vector3();
        force.y = Input.GetAxis("Vertical");
        force += camera.transform.right * Input.GetAxis("Horizontal");
        force.Normalize();
        force *= acceleration;

        objectRigidbody.AddForce(force);
    }
}
