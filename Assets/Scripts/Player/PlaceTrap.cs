using UnityEngine;
using System.Collections;

public class PlaceTrap : MonoBehaviour {

    public Transform[] trapList;

    public Transform trapPointsItem;
    private Transform[] trapPoints;

    private float radius = 0.75f;
    private int trapIndex;

    private bool spawn = false;

    // Use this for initialization
    void Start() {
        trapPoints = new Transform[trapPointsItem.childCount];
        for (int i = 0; i < trapPointsItem.childCount; i++) {
            trapPoints[i] = trapPointsItem.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update() {
        if (transform.right.x == 1 || transform.right.x == -1) {
            float dist;
            for (int i = 0; i < trapPointsItem.childCount; i++) {
                trapPoints[i] = trapPointsItem.GetChild(i);
                dist = Vector2.Distance(new Vector2(transform.position.y, transform.position.z),
                    new Vector2(trapPoints[i].position.y, trapPoints[i].position.z));
                if (dist < radius) {
                    trapIndex = i;
                    spawn = true;
                    break;
                }
                else
                    spawn = false;
            }
        }
        int key = -1;
        if (spawn) {
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
                Quaternion rotation = Quaternion.Euler(
                 new Vector3(360 - trapPoints[trapIndex].eulerAngles.x,
                 trapList[1].eulerAngles.y,
                 trapPoints[trapIndex].eulerAngles.z));

                PointData pd = trapPoints[trapIndex].GetComponent<PointData>();

                if (pd.getItem() != null)
                    Destroy(pd.getItem().gameObject);

                if (transform.right.x == 1 || transform.right.x == -1) {
                    Transform ti = (Transform)Instantiate(trapList[key], trapPoints[trapIndex].position, rotation);
                    trapPoints[trapIndex].GetComponent<PointData>().setItem(ti.gameObject, key);
                }
            }
        }
    }
}
