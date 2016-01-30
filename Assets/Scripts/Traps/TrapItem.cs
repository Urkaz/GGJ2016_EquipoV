using UnityEngine;
using System.Collections;

public class TrapItem : MonoBehaviour {

    protected bool isEnabled = false;

    public float cooldown = 5;
    private float cdTimer = 0;

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.Z))
            ActivateTrap();

        if (cdTimer - Time.deltaTime > 0) {
            cdTimer -= Time.deltaTime;
            //Debug.Log(cdTimer);
        }
        else {
            cdTimer = 0;
        }


        if (isEnabled) {
            RunAnimation(Time.deltaTime);
        }
    }

    public void ActivateTrap() {
        if (!isEnabled) {
            if (cdTimer == 0) {
                isEnabled = true;
                cdTimer = cooldown;
            }
        }
    }

    public virtual void RunAnimation(float delta) { }

    public void enableCooldown() {
        cdTimer = cooldown;
    }
}
