using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    private GameObject player;

    public void Awake() {
        DontDestroyOnLoad(this);
        instance = this;
        loadLevel(0);
    }

    public void loadLevel(int level) {
        switch(level) {
            case 0:
                GameObject go = Resources.Load("Prefabs/LevelTest") as GameObject;
                GameObject p = Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
                p.name = "Level Test (id:0)";
                break;
        }

        player = GameObject.FindWithTag("Player");
    }

    public void startGame() {
        player.GetComponent<PlaceTrap>().enabled = false;
        player.GetComponent<TrapActivator>().enabled = true;

        GameObject.Find("Spawn").GetComponent<EnemySpawn>().enabled = true;

    }
}
