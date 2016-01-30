using UnityEngine;
using System.Collections;

public class BallOfWoolTrap : TrapItem {

    private Transform pendulumItem;

    public uint maxAngle = 50;
    public float rotationSpeed = 100;

    private int direction = -1;

    public void Start() {
        pendulumItem = transform.Find("Pendulum");
        pendulumItem.rotation = Quaternion.Euler(new Vector3(0, 0, maxAngle));
    }

    public override void RunAnimation(float delta) {
        float rotationAngle = delta * rotationSpeed;

        float currentAngle = pendulumItem.localEulerAngles.z;
        currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;

        if (direction == 1) {
            if (currentAngle + rotationAngle < maxAngle)
                pendulumItem.Rotate(new Vector3(0, 0, rotationAngle));
            else {
                pendulumItem.rotation = Quaternion.Euler(new Vector3(0, 0, maxAngle));
                direction = -1;
                isEnabled = false;
            }
        }
        else {


            if (currentAngle - rotationAngle > -maxAngle) {
                pendulumItem.Rotate(new Vector3(0, 0, -rotationAngle));
            }
            else {
                pendulumItem.rotation = Quaternion.Euler(new Vector3(0, 0, -maxAngle));
                direction = 1;
                isEnabled = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        //collision.other; ->Cast to enemy and damage.
    }
}
