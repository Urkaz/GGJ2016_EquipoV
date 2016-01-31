using UnityEngine;
using System.Collections;

public class DotObject : MonoBehaviour {

    public bool dotEnabled = false;
    public GameObject escarabajo;
    public Texture enabledTexture;
    Texture mainTexture;

    void Start()
    {
        mainTexture = escarabajo.GetComponent<MeshRenderer>().material.GetTexture("_MainTex");
    }

    void Update()
    {
        if(dotEnabled && gameObject.layer.Equals(LayerMask.NameToLayer("Result"))) escarabajo.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", enabledTexture);
        else escarabajo.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", mainTexture);
    }
}
