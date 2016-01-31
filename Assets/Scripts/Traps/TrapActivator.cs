using UnityEngine;
using System.Collections;

public class TrapActivator : MonoBehaviour {

    public Transform trapPointsItem;
    private Transform[] trapPoints;

    private float activationRadius = 0.75f;
    // Use this for initialization
    void Start() {
        trapPoints = new Transform[trapPointsItem.childCount];
        for (int i = 0; i < trapPointsItem.childCount; i++) {
            trapPoints[i] = trapPointsItem.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update() {
        int key = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            key = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            key = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            key = 2;
        }

        if (key != -1) {
            float dist;
            for (int i = 0; i < trapPointsItem.childCount; i++) {
                trapPoints[i] = trapPointsItem.GetChild(i);
                dist = Vector2.Distance(new Vector2(transform.position.y, transform.position.z),
                    new Vector2(trapPoints[i].position.y, trapPoints[i].position.z));
                if (dist < activationRadius) {
                    PointData pd = trapPoints[i].GetComponent<PointData>();

                    Transform tr;
                    if (pd.getID() == key) {
                        switch (key) {
                            case 0:
                                tr = pd.getItem().transform.Find("Sand");
                                tr.gameObject.GetComponent<TrapItem>().ActivateTrap();
                                break;
                            case 1:
                                tr = pd.getItem().transform.Find("Pendulum");
                                tr.gameObject.GetComponent<TrapItem>().ActivateTrap();
                                break;
                            case 2:
                                tr = pd.getItem().transform.Find("LaserSupport");
                                tr.gameObject.GetComponent<TrapItem>().ActivateTrap();
                                break;
                        }
                    }
                    else
                        pd.getItem().GetComponent<TrapItem>().enableCooldown();

                    break;
                }
            }
        }
    }
}
