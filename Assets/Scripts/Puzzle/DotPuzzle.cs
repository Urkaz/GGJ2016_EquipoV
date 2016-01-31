using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DotPuzzle : MonoBehaviour {

    GameObject[] pointObjects;
    public GameObject[] lineRenderers;
    GameObject[] bandagesGUI;
    GameObject bandage;
    int bandageNum = 4;
    int intents = 3;
    GameObject mouseBandage;
    AudioSource bandageSound;

    public AudioClip bandageClip;
    public AudioClip meowClip;

    Vector3 startPoint;
    Vector3 endPoint;

    bool firstClick = false;
    bool puzzleSolved = false;
    float timeToSolve = 0;
    float transitionSeconds = 2.5f;

    public static int Reward;
    GameObject intentText;
    GameObject rewardText;

    GameObject tutorialImage;

	// Use this for initialization
	void Start () {

        pointObjects = GameObject.FindGameObjectsWithTag("Dot");
        bandage = GameObject.Find("BandageRenderers");
        bandagesGUI = GameObject.FindGameObjectsWithTag("Bandage");
        intentText = GameObject.Find("IntentText");
        rewardText = GameObject.Find("RewardText");
        rewardText.SetActive(false);
        mouseBandage = GameObject.Find("BandageMouse");
        bandage.SetActive(false);

        tutorialImage = GameObject.Find("Tutorial");
        tutorialImage.SetActive(false);

        startPoint = new Vector3();
        endPoint = new Vector3();

        bandageSound = GetComponent<AudioSource>();
        bandageSound.clip = bandageClip;
	}

    void Update()
    {

        if (Input.GetMouseButton(1))
        {
            tutorialImage.SetActive(true);
        }
        else tutorialImage.SetActive(false);

        timeToSolve += Time.deltaTime;

        switch (intents)
        {
            case 0:
                intentText.GetComponent<Text>().text = "";
                    break;
            case 1:
                intentText.GetComponent<Text>().text = "I";
                break;
            case 2:
                intentText.GetComponent<Text>().text = "I-I";
                break;
            case 3:
                intentText.GetComponent<Text>().text = "I-I-I";
                break;
        }

        for (int i = 0; i < pointObjects.Length; i++)
        {
            if (pointObjects[i].GetComponent<SphereCollider>().bounds.Contains(new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0)))
            {
                mouseBandage.GetComponent<Renderer>().enabled = true;
                break;
            }
            else
                mouseBandage.GetComponent<Renderer>().enabled = false;
        }

        mouseBandage.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

        if (Input.GetMouseButtonDown(0) && !puzzleSolved)
        {
            if (!firstClick)
            {
                startPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                for (int i = 0; i < pointObjects.Length; i++)
                {
                    if (pointObjects[i].GetComponent<SphereCollider>().bounds.Contains(startPoint))
                    {
                        bandage.SetActive(true);
                        pointObjects[i].GetComponent<DotObject>().dotEnabled = true;
                        bandage.transform.position = startPoint;
                        firstClick = true;
                    }
                }
            }
            else
            {
                endPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);

                for (int i = 0; i < pointObjects.Length; i++)
                {
                    if (pointObjects[i].GetComponent<SphereCollider>().bounds.Contains(endPoint))
                    {
                        bandageNum--;
                        if (bandageNum >= 0) bandagesGUI[bandageNum].SetActive(false);
                        pointObjects[i].GetComponent<DotObject>().dotEnabled = true;
                        bandageSound.Play();

                        /*RaycastHit hit;

                        Debug.DrawRay(startPoint, endPoint - startPoint, Color.red, 10);

                        if (Physics.Raycast(startPoint, endPoint - startPoint, out hit, 1000))
                        {
                            //Debug.DrawRay(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, Color.red, 10);

                            hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                            /*if (Physics.Raycast(hit.collider.gameObject.transform.position, endPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                            {
                                //Debug.DrawRay(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, Color.red, 10);

                                hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                                if (Physics.Raycast(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                                {
                                    hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;
                                }
                            }
                        }*/

                        RaycastHit[] hits = Physics.RaycastAll(startPoint, endPoint - startPoint, Vector3.Distance(startPoint, endPoint));
                        for(int j = 0; j < hits.Length; j++)
                        {
                            hits[j].collider.gameObject.GetComponent<DotObject>().dotEnabled = true;
                        }

                        if(bandageNum >= 0) lineRenderers[bandageNum].GetComponent<LineRenderer>().SetPositions(new Vector3[] { startPoint, endPoint });
                        startPoint = endPoint;
                    }
                }
            }

            if (bandageNum == 0)
            {
                float dotsFound = 0;

                for (int i = 0; i < pointObjects.Length; i++)
                {
                    if (pointObjects[i].GetComponent<DotObject>().dotEnabled && pointObjects[i].layer.Equals(LayerMask.NameToLayer("Result")))
                        dotsFound++;
                }

                if (dotsFound == 9)
                    puzzleSolved = true;

                else
                {
                    intents--;
                    if (intents >= 0) Reset();
                }
            }
        }
        else if (puzzleSolved || intents < 0)
        {
            //DO SOMETHING WHILE TRANSITIONING?
            transitionSeconds -= Time.deltaTime;
            Reward = (int)intents / 3 * 100;
            //rewardText.SetActive(true);

            //SHOW REWARD SOMEHOW
            //rewardText.GetComponent<TextMesh>().text = "+" + intents;
            //rewardImage.SetActive(true);

            if (transitionSeconds <= 0)
            {
                bandageSound.clip = meowClip;
                bandageSound.Play();
                SceneManager.LoadScene("Main");
            }
        }
    }
	

    void Reset()
    {
        bandageNum = 4;
        startPoint = new Vector3();
        endPoint = new Vector3();
        firstClick = false;

        for(int i = 0; i<lineRenderers.Length; i++)
            lineRenderers[i].GetComponent<LineRenderer>().SetPositions(new Vector3[] { startPoint, endPoint });

        for (int i = 0; i < pointObjects.Length; i++)
            pointObjects[i].GetComponent<DotObject>().dotEnabled = false;

        for (int i = 0; i < bandagesGUI.Length; i++)
            bandagesGUI[i].SetActive(true);

        bandage.SetActive(false);
    }
}
