using UnityEngine;
using System.Collections;

public class GizmoDrawer : MonoBehaviour {

    public Color color = Color.yellow;
    public float size = 0.1f;

    void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, size);
    }
}
