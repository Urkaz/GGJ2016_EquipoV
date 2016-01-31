using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlaceTrap : MonoBehaviour {

    public Transform[] trapList;

    public Transform trapPointsItem;
    private Transform[] trapPoints;

    private int woolNum;
    private int sandNum;
    private int laserNum;  

    public int baseMoney = 200;
    public int bonusMoney = 500;

    private float radius = 0.75f;
    private int trapIndex;

    private bool spawn = false;

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
        }
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

    public void SetMoney(float reward)
    {
        this.money = baseMoney + (int)(bonusMoney * reward);
    }
}
