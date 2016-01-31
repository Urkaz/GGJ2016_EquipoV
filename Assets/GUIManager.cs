using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public static GUIManager guiManager;

    public GameObject woolText;
    public GameObject sandText;
    public GameObject laserText;
    public GameObject enemiesText;
    public GameObject warningRight;
    public GameObject warningLeft;
    public bool leftRepeating;
    public bool rightRepeating;

    void Awake()
    {
        guiManager = this;
    }

	// Use this for initialization
	void Start () {

        woolText = GameObject.Find("WoolText");
        sandText = GameObject.Find("SandText");
        laserText = GameObject.Find("LaserText");
        enemiesText = GameObject.Find("EnemiesText");
        warningRight = GameObject.Find("WarningRight");
        warningRight.SetActive(false);
        warningLeft = GameObject.Find("WarningLeft");
        warningLeft.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        if (warningLeft.activeSelf && !leftRepeating)
        {
            InvokeRepeating("BlinkLeft", 0, 1f);
            leftRepeating = true;
        }
            

        if (warningRight.activeSelf && !rightRepeating)
        {
            InvokeRepeating("BlinkRight", 0, 1f);
            rightRepeating = true;
        }     
    }

    void BlinkLeft()
    {
        warningLeft.SetActive(!warningLeft.activeSelf);
    }

    void BlinkRight()
    {
        warningRight.SetActive(!warningRight.activeSelf);
    }
}
