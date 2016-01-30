using UnityEngine;
using System.Collections;

public class PointData : MonoBehaviour {

    private bool isOccuppied = false;
    private GameObject item = null;

    public void setItem(GameObject item) {
        this.item = item;
    }

    public void setOccuppied(bool occuppied) {
        isOccuppied = occuppied;
    }

    public GameObject getItem() {
        return item;
    }
}
