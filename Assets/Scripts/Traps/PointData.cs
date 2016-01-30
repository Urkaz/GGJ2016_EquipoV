using UnityEngine;
using System.Collections;

public class PointData : MonoBehaviour {

    private bool isOccuppied = false;
    private GameObject item = null;
    private int id = -1;

    public void setItem(GameObject item, int id) {
        this.item = item;
        this.id = id;
    }

    public GameObject getItem() {
        return item;
    }

    public int getID() {
        return id;
    }
}
