using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

    public float maxSpeed = 50f;
    public float playerSpeed = 10f;

    public CameraRotation camera;
    private CardinalPosition mCardinalPosition;

    //Walls
    public Transform northWall;
    public Transform southWall;
    public Transform eastWall;
    public Transform westWall;
    

    // Use this for initialization
    void Start () {
        mCardinalPosition = camera.GetPreviousCardinalPosition();
        ChangePosition(mCardinalPosition);
	}
	
	// Update is called once per frame
	void Update () {
        CardinalPosition cameraCardinalPosition = camera.GetPreviousCardinalPosition();

	    /*if(mCardinalPosition != cameraCardinalPosition)
        {
            mCardinalPosition = cameraCardinalPosition;
            ChangePosition(mCardinalPosition);
        }*/
        PlayerMovement();
    }

    void FixedUpdate()
    {
    }

    private void ChangePosition(CardinalPosition cardinalPosition)
    {

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

    private Vector3 ChangePosition(CardinalPosition cardinalPosition, Vector3 cursorPosition)
    {

        Vector3 newPlayerPosition = cursorPosition;

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
        return newPlayerPosition;
    }

    private void PlayerMovement()
    {
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = ChangePosition(camera.GetCardinalPosition(), cursorPosition);
        return;
        

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            return;
        Vector3 force = new Vector3();
        float horizontalOutput = 0f;
        float verticalOutput = 0f;
        if (Input.GetKey(KeyCode.W))
            verticalOutput += 1f;
        if(Input.GetKey(KeyCode.S))
            verticalOutput -= 1f;

        force.y = verticalOutput;

        Vector3 cameraVector = camera.transform.right;
        cameraVector.y = 0;
        cameraVector.Normalize();
        if (Input.GetKey(KeyCode.D))
            horizontalOutput += 1f;
        
        if (Input.GetKey(KeyCode.A))
            horizontalOutput -= 1f;

        force += cameraVector * horizontalOutput;

        /*
        force.y = Input.GetAxis("Vertical");
        force += camera.transform.right * Input.GetAxis("Horizontal");*/
        force.Normalize();
        force *= playerSpeed * Time.deltaTime;


        transform.position += force;
    }
}
