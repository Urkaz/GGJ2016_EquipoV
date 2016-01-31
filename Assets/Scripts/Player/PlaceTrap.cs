using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlaceTrap : MonoBehaviour {

    public Transform[] trapList;

    private Camera cam;

    private int woolNum;
    private int sandNum;
    private int laserNum;

    public int baseMoney = 200;
    public int bonusMoney = 500;

    public LayerMask ignoredLayers;

    //private int money;

    // Use this for initialization
    void Start() {
        //RANDOM VALUES, also add BONUS
        laserNum = 10;
        woolNum = 7;
        sandNum = 5;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {

        GUIManager.guiManager.laserText.GetComponent<Text>().text = laserNum + "";
        GUIManager.guiManager.woolText.GetComponent<Text>().text = woolNum + "";
        GUIManager.guiManager.sandText.GetComponent<Text>().text = sandNum + "";


        int key = -1;
        if (sandNum > 0 && Input.GetKeyDown(KeyCode.Alpha1)) { //Pinchos
            key = 0;
        }
        if (woolNum > 0 && Input.GetKeyDown(KeyCode.Alpha2)) { //Bola
            key = 1;
        }
        if (laserNum > 0 && Input.GetKeyDown(KeyCode.Alpha3)) { //Laser
            key = 2;
        }

        if (key != -1) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cam.transform.forward, out hit, 20, ignoredLayers)) {
                if (hit.collider.CompareTag("Trap")) {
                    Transform tr = Instantiate(trapList[key], hit.collider.transform.position, hit.collider.transform.rotation) as Transform;
                    PointData pd = hit.collider.GetComponent<PointData>();

                    if (pd.getItem() != null)
                        Destroy(pd.getItem().gameObject);

                    pd.setItem(tr.gameObject, key);

                    switch (key) {
                        case 0:
                            //money -= 50;
                            sandNum--;
                            break;
                        case 1:
                            //money -= 50;
                            woolNum--;
                            break;
                        case 2:
                            //money -= 50;
                            laserNum--;
                            break;
                    }
                }
            }
        }
    }

    public void SetMoney(float reward) {
        //money = baseMoney + (int)(bonusMoney * reward);
    }
}
