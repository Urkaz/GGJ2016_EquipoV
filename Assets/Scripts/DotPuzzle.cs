using UnityEngine;
using System.Collections;


public class DotPuzzle : MonoBehaviour {

    GameObject[] pointObjects;
    public GameObject[] lineRenderers;
    GameObject[] bandagesGUI;
    GameObject bandage;
    int bandageNum = 4;
    Quaternion initRot;

    Vector3 startPoint;
    Vector3 endPoint;

    bool firstClick = false;
    bool puzzleSolved = false;
    float timeToSolve = 0;

    public static int Reward;
    GameObject timeText;

	// Use this for initialization
	void Start () {

        pointObjects = GameObject.FindGameObjectsWithTag("Dot");
        bandage = GameObject.Find("BandageRenderers");
        bandagesGUI = GameObject.FindGameObjectsWithTag("Bandage");
        timeText = GameObject.Find("DeltaTimeText");
        initRot = bandage.transform.rotation;
        bandage.SetActive(false);

        startPoint = new Vector3();
        endPoint = new Vector3();
	}

    void Update()
    {
        timeToSolve += Time.deltaTime;
        if (!puzzleSolved) timeText.GetComponent<TextMesh>().text = Mathf.Round(timeToSolve * 100f) / 100f + "";
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
                        bandagesGUI[bandageNum].SetActive(false);
                        pointObjects[i].GetComponent<DotObject>().dotEnabled = true;

                        RaycastHit hit;

                        Debug.DrawLine(pointObjects[i].transform.position, startPoint, Color.red, 60);

                        if (Physics.Raycast(pointObjects[i].transform.position, startPoint - pointObjects[i].transform.position, out hit, 1000))
                        {
                            hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                            if (Physics.Raycast(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                            {
                                hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;

                                if (Physics.Raycast(hit.collider.gameObject.transform.position, startPoint - hit.collider.gameObject.transform.position, out hit, 1000))
                                {
                                    hit.collider.gameObject.GetComponent<DotObject>().dotEnabled = true;
                                }
                            }
                        }
                    }
                }

                lineRenderers[bandageNum].GetComponent<LineRenderer>().SetPositions(new Vector3[] { startPoint, endPoint });
                startPoint = endPoint;
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
                {
                    puzzleSolved = true;
                    //reward = something;
                }
                else Reset();
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
