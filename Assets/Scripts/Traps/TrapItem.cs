using UnityEngine;
using System.Collections;

public class TrapItem : MonoBehaviour {

    protected bool isEnabled = false;

    // Update is called once per frame
    void Update() {
        if (isEnabled) {
            RunAnimation(Time.deltaTime);
        }
    }

    public void ActivateTrap() {
        if (!isEnabled) {
            isEnabled = true;
        }
    }

    public virtual void RunAnimation(float delta) { }
}
