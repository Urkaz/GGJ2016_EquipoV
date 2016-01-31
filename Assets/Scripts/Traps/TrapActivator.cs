using UnityEngine;
using System.Collections;

public class TrapActivator : MonoBehaviour {

    private Camera cam;

    public LayerMask layerMask;

    private float activationRadius = 0.75f;
    // Use this for initialization
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        int key = -1;
        if (Input.GetKeyDown(KeyCode.Alpha1)) { //Pinchos
            key = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { //Bola
            key = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { //Laser
            key = 2;
        }

        if (key != -1) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cam.transform.forward, out hit, 20, layerMask)) {
                if (hit.collider.CompareTag("Trap")) {

                    PointData pd = hit.collider.GetComponent<PointData>();

                    if (pd != null) {
                        if (pd.getItem() != null) {
                            if (pd.getID() == key) {
                                pd.getItem().GetComponentInChildren<TrapItem>().ActivateTrap();
                            }
                            else
                                pd.getItem().GetComponentInChildren<TrapItem>().enableCooldown();
                        }
                    }
                }
            }
        }
    }
}
