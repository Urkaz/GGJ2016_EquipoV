using UnityEngine;
using System.Collections;

public class BallOfWoolTrap : TrapItem {

    public uint maxAngle = 50;
    public float rotationSpeed = 100;

    private int direction = -1;

    public void Start() {
        transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxAngle));
    }

    public override void RunAnimation(float delta) {
        float rotationAngle = delta * rotationSpeed;

        float currentAngle = transform.localEulerAngles.z;
        currentAngle = (currentAngle > 180) ? currentAngle - 360 : currentAngle;

        if (direction == 1) {
            if (currentAngle + rotationAngle < maxAngle)
                transform.Rotate(new Vector3(0, 0, rotationAngle));
            else {
                transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, maxAngle));
                direction = -1;
                isEnabled = false;
            }
        }
        else {
            if (currentAngle - rotationAngle > -maxAngle) {
                transform.Rotate(new Vector3(0, 0, -rotationAngle));
            }
            else {
                transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -maxAngle));
                direction = 1;
                isEnabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        EnemyDamage ed = other.GetComponent<EnemyDamage>();
        if (ed != null && isEnabled) {
            ed.Damage(1);
        }
    }
}
