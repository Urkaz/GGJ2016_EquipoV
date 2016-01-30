using UnityEngine;
using System.Collections;

public class DotObject : MonoBehaviour {

    public bool dotEnabled = false;
    Color mainColor;

    void Start()
    {
        mainColor = GetComponent<MeshRenderer>().material.GetColor("_Color");
    }

    void Update()
    {
        if(dotEnabled && gameObject.layer.Equals(LayerMask.NameToLayer("Result"))) GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);
        else GetComponent<MeshRenderer>().material.SetColor("_Color", mainColor);
    }
}
