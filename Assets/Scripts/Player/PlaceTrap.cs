using UnityEngine;
using System.Collections;

public class PlaceTrap : MonoBehaviour {

    public Transform[] trapList;

    //public Transform trapPointsItem;
    //private Transform[] trapPoints;

    private Camera cam;

    public int baseMoney = 200;
    public int bonusMoney = 500;

    //private float radius = 0.75f;
    //private int trapIndex;

    private bool spawn = false;

    private int money;

    // Use this for initialization
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        //if (money <= 0)
            //return;

        int key = -1;
        if (Input.GetKey(KeyCode.Alpha1)) { //Pinchos
            key = 0;
        }
        else if (Input.GetKey(KeyCode.Alpha2)) { //Laser
            key = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha3)) { //Bola
            key = 2;
        }

        if (key != -1) {

            RaycastHit hit;

            if (Physics.Raycast(transform.position, cam.transform.forward, out hit, 20)) {
                if (hit.collider.CompareTag("Trap")) {
                    Transform tr = Instantiate(trapList[key], hit.collider.transform.position, hit.collider.transform.rotation) as Transform;
                    PointData pd = hit.collider.GetComponent<PointData>();

                    if (pd.getItem() != null)
                        Destroy(pd.getItem().gameObject);

                    pd.setItem(tr.gameObject, key);

                    switch (key) {
                        case 0:
                            money -= 50;
                            break;
                        case 1:
                            money -= 50;
                            break;
                        case 2:
                            money -= 50;
                            break;

                    }
                }
            }
        }
    }

    public void SetMoney(float reward) {
        money = baseMoney + (int)(bonusMoney * reward);
        Debug.Log(money);
    }
}
