using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    public void Awake() {
        DontDestroyOnLoad(this);
        instance = this;

        loadScenario();
    }

    public void loadScenario() {
        GameObject go = Resources.Load("Prefabs/Scenario") as GameObject;
        GameObject p = Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
        p.name = "PyramidScenario";
    }
}
