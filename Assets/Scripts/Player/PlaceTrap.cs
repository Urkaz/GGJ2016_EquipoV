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

    private int money;

    // Use this for initialization
    void Start() {
        trapPoints = new Transform[trapPointsItem.childCount];
        for (int i = 0; i < trapPointsItem.childCount; i++) {
            trapPoints[i] = trapPointsItem.GetChild(i);
        }

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

        if (money <= 0)
            return;

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
        //if (money <= 0)
            //return;
		
        int key = -1;
        if (spawn) {
            if (Input.GetKey(KeyCode.Alpha1) && sandNum > 0) { //Pinchos
                key = 0;
                money -= 50;
                sandNum--;
            }
            else if (Input.GetKey(KeyCode.Alpha2) && laserNum > 0) { //Laser
                key = 1;
                money -= 50;
                laserNum--;
            }
            else if (Input.GetKey(KeyCode.Alpha3) && woolNum > 0) { //Bola
                key = 2;
                money -= 50;
                woolNum--;
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
    }
}
