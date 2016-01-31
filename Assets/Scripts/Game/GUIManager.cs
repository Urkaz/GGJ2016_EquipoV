using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    public static GUIManager guiManager;

    public GameObject woolText;
    public GameObject sandText;
    public GameObject laserText;

    void Awake()
    {
        guiManager = this;
    }

	// Use this for initialization
	void Start () {

        woolText = GameObject.Find("WoolText");
        sandText = GameObject.Find("SandText");
        laserText = GameObject.Find("LaserText");
	}
}
