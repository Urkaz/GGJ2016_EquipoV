using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class DotPuzzle : MonoBehaviour {

    GameObject[] pointObjects;
    public GameObject[] lineRenderers;
    GameObject[] bandagesGUI;
    GameObject bandage;
    int bandageNum = 4;
    int intents = 3;
    GameObject mouseBandage;

    Vector3 startPoint;
    Vector3 endPoint;

    bool firstClick = false;
    bool puzzleSolved = false;
    float timeToSolve = 0;
    float transitionSeconds = 3.0f;

    public static int Reward;
    GameObject intentText;
    GameObject rewardText;
    GameObject rewardImage;

	// Use this for initialization
	void Start () {

        pointObjects = GameObject.FindGameObjectsWithTag("Dot");
        bandage = GameObject.Find("BandageRenderers");
        bandagesGUI = GameObject.FindGameObjectsWithTag("Bandage");
        intentText = GameObject.Find("IntentText");
        rewardText = GameObject.Find("RewardText");
        rewardText.SetActive(false);
        rewardImage = GameObject.Find("RewardImage");
        rewardImage.SetActive(false);
        mouseBandage = GameObject.Find("BandageMouse");
        bandage.SetActive(false);

        startPoint = new Vector3();
        endPoint = new Vector3();
	}

    void Update()
    {
        timeToSolve += Time.deltaTime;
        //if (!puzzleSolved) intentText.GetComponent<TextMesh>().text = Mathf.Round(timeToSolve * 10f) / 10f + "";

        switch (intents)
        {
            case 0:
                intentText.GetComponent<TextMesh>().text = "";
                    break;
            case 1:
                intentText.GetComponent<TextMesh>().text = "I";
                break;
            case 2:
                intentText.GetComponent<TextMesh>().text = "I I";
                break;
            case 3:
                intentText.GetComponent<TextMesh>().text = "I I I";
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
    }
	
	// Update is called once per frame
	void FixedUpdate () {

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
                        if(bandageNum >= 0) bandagesGUI[bandageNum].SetActive(false);
                        pointObjects[i].GetComponent<DotObject>().dotEnabled = true;

                        RaycastHit hit;

                        //Debug.DrawLine(pointObjects[i].transform.position, startPoint, Color.red, 60);
                        //Debug.DrawRay(pointObjects[i].transform.position, startPoint - pointObjects[i].transform.position, Color.red, 60);

                        if (Physics.Raycast(pointObjects[i].transform.position, startPoint - pointObjects[i].transform.position, out hit, 1000))
                        {
                            hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                            //Debug.DrawRay(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, Color.red, 60);

                            if (Physics.Raycast(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                            {
                                hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                                //Debug.DrawRay(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, Color.red, 60);

                                if (Physics.Raycast(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                                {
                                    hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;
                                }
                            }
                        }

                        lineRenderers[bandageNum].GetComponent<LineRenderer>().SetPositions(new Vector3[] { startPoint, endPoint });
                        startPoint = endPoint;
                    }
                }
            }

            if(bandageNum == 0)
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
                    if(intents >= 0) Reset();
                }
            }
        }
        else if (puzzleSolved || intents < 0)
        {
            //DO SOMETHING WHILE TRANSITIONING?
            transitionSeconds -= Time.deltaTime;
            Reward = intents;
            rewardText.SetActive(true);
            rewardText.GetComponent<TextMesh>().text = "+" + intents;
            rewardImage.SetActive(true);

            if (transitionSeconds <= 0) SceneManager.LoadScene(2);
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
