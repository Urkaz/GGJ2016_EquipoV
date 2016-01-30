using UnityEngine;
using System.Collections;

public class GroudTrap : TrapItem {

    private Transform sandItem;
    public float heightDisplacement = 0.2f;
    private int direction = 1;


    void Start() {
        sandItem = transform.Find("Sand");
    }

    public override void RunAnimation(float delta) {
        if (direction == 1) {
            if ((sandItem.localPosition.y + delta) < heightDisplacement)
                sandItem.Translate(0, delta, 0);
            else {
                sandItem.localPosition = new Vector3(0, heightDisplacement, 0);
                direction = -1;
            }
        }
        else {
            if ((sandItem.localPosition.y - delta) > 0)
                sandItem.Translate(0, -delta, 0);
            else {
                sandItem.localPosition = new Vector3(0, 0, 0);
                direction = 1;
                isEnabled = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        //collision.other; ->Cast to enemy and damage.
    }
}
