using UnityEngine;
using System.Collections;

public class TrapActivator : MonoBehaviour {

    public Transform trapPointsItem;
    private Transform[] trapPoints;

    private float activationRadius = 0.75f;
    private int key = -1;

    // Use this for initialization
    void Start() {
        trapPoints = new Transform[trapPointsItem.childCount];
        for (int i = 0; i < trapPointsItem.childCount; i++) {
            trapPoints[i] = trapPointsItem.GetChild(i);
        }
    }  

    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Alpha1)) {
            key = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            key = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3)) {
            key = 2;
        }

        if (key != -1) {
            float dist;
            for (int i = 0; i < trapPointsItem.childCount; i++) {
                trapPoints[i] = trapPointsItem.GetChild(i);
                dist = Vector2.Distance(new Vector2(transform.position.y, transform.position.z),
                    new Vector2(trapPoints[i].position.y, trapPoints[i].position.z));
                if (dist < activationRadius) {
                    trapPoints[i].GetComponent<PointData>().getItem().GetComponent<TrapItem>().ActivateTrap();
                    break;
                }
            }
        }
    }
}
