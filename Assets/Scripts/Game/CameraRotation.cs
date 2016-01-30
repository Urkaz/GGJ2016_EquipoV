using UnityEngine;
using System.Collections;
using System;

public enum Rotate
{
    Left,
    Right,
    Stop
}
public enum CardinalPosition
{
    East,
    South,
    West,
    North
}

public class CameraRotation : MonoBehaviour {
    private Rotate mRotate = Rotate.Stop;
    private CardinalPosition mCardinalPosition = CardinalPosition.North;
    private CardinalPosition mPreviousCardinalPosition;


    public float cameraSpeed = 5.0f;

    private Vector3 rotation;
    private Vector3 nextRotation;

	// Use this for initialization
	void Start () {
        rotation = CopyVector3(transform.rotation.eulerAngles);
        nextRotation = CopyVector3(transform.rotation.eulerAngles);

        SetCardinalPosition((int)rotation.y);
        mPreviousCardinalPosition = mCardinalPosition;
    }
	
    private Vector3 CopyVector3(Vector3 vector)
    {
        return new Vector3(vector.x, vector.y, vector.z);
    }

	// Update is called once per frame
	void Update () {
        CameraControls();
        CameraPosition();
    }

    private void CameraPosition()
    {
        if (mRotate == Rotate.Stop)
        {
            mPreviousCardinalPosition = mCardinalPosition;
            //Debug.Log("Cardinal position: " + mCardinalPosition);
        }
        /*else
            Debug.Log("Going from " + mPreviousCardinalPosition + " to " + mCardinalPosition);*/



        float speed = cameraSpeed * Time.deltaTime;

        float angleDifference = Mathf.Abs(rotation.y - nextRotation.y);
        if (angleDifference <= speed)
        {
            rotation = nextRotation;
            transform.rotation = Quaternion.Euler(rotation);
            mRotate = Rotate.Stop;
        }

        switch (mRotate)
        {
            case Rotate.Left:
                rotation.y += speed;
                transform.rotation = Quaternion.Euler(rotation);
                break;
            case Rotate.Right:
                rotation.y -= speed;
                transform.rotation = Quaternion.Euler(rotation);
                break;
        }

        if (nextRotation.y < 0)
            nextRotation.y += 360;

        if (nextRotation.y > 360)
            nextRotation.y -= 360;

        if(rotation.y < 0)
        {
            rotation.y += 360;
            transform.rotation = Quaternion.Euler(rotation);
        }
        if (rotation.y > 360)
        {
            rotation.y -= 360;
            transform.rotation = Quaternion.Euler(rotation);
        }


    }

    private void SetCardinalPosition(int eulerAngle)
    {

        int angleCardinalPosition = eulerAngle / 90;

        switch (angleCardinalPosition)
        {
            case 0:
                mCardinalPosition = CardinalPosition.East;
                break;
            case 1:
                mCardinalPosition = CardinalPosition.South;
                break;
            case 2:
                mCardinalPosition = CardinalPosition.West;
                break;
            case 3:
                mCardinalPosition = CardinalPosition.North;
                break;
        }
    }

    private void CameraControls()
    {
        if (mRotate != Rotate.Stop)
            return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            mPreviousCardinalPosition = mCardinalPosition;
            mRotate = Rotate.Left;
            mCardinalPosition = SetNextCardinalPoint(mCardinalPosition, mRotate);
            nextRotation.y += 90;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mPreviousCardinalPosition = mCardinalPosition;
            mRotate = Rotate.Right;
            mCardinalPosition = SetNextCardinalPoint(mCardinalPosition, mRotate);
            nextRotation.y -= 90;
        }
    }

    private CardinalPosition SetNextCardinalPoint(CardinalPosition cardinalPoint, Rotate direction)
    {

        CardinalPosition newCardinal = CardinalPosition.North;

        if(direction == Rotate.Left)
        {
            switch (cardinalPoint)
            {
                case CardinalPosition.North:
                    newCardinal = CardinalPosition.East;
                    break;
                case CardinalPosition.East:
                    newCardinal = CardinalPosition.South;
                    break;
                case CardinalPosition.South:
                    newCardinal = CardinalPosition.West;
                    break;
                case CardinalPosition.West:
                    newCardinal = CardinalPosition.North;
                    break;
            }
        }

        else if(direction == Rotate.Right)
        {
            switch (cardinalPoint)
            {
                case CardinalPosition.North:
                    newCardinal = CardinalPosition.West;
                    break;
                case CardinalPosition.East:
                    newCardinal = CardinalPosition.North;
                    break;
                case CardinalPosition.South:
                    newCardinal = CardinalPosition.East;
                    break;
                case CardinalPosition.West:
                    newCardinal = CardinalPosition.South;
                    break;
            }
        }
        return newCardinal;
    }

    public CardinalPosition GetPreviousCardinalPosition()
    {
        return mPreviousCardinalPosition;
    }

    public CardinalPosition GetCardinalPosition()
    {
        return mCardinalPosition;
    }
}
