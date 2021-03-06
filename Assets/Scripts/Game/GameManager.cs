﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;
    public static GameManager Instance {
        get {
            return instance;
        }
    }

    private GameObject player;

    private bool gameOver;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlaceTrap>().SetMoney(DotPuzzle.Reward);
    }

    public void Awake() {
        DontDestroyOnLoad(this);
        instance = this;
        loadLevel(0);
    }

    public void loadLevel(int level) {
        switch(level) {
            case 0:
                GameObject go = Resources.Load("Prefabs/Levels/Level01") as GameObject;
                GameObject p = Instantiate(go, Vector3.zero, Quaternion.identity) as GameObject;
                p.name = "Level Test (id:0)";
                break;
        }
        
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlaceTrap>().SetMoney(DotPuzzle.Reward);
    }

    public void startGame() {

        player.GetComponent<PlaceTrap>().enabled = false;
        GUIManager.guiManager.laserText.SetActive(false);
        GUIManager.guiManager.woolText.SetActive(false);
        GUIManager.guiManager.sandText.SetActive(false);

        player.GetComponent<TrapActivator>().enabled = true;

        GameObject.Find("Spawn").GetComponent<EnemySpawn>().enabled = true;

        GetComponent<AudioSource>().enabled = true;

    }

    public void GameOver(bool win) {
        gameOver = win;
        GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("GameOver");
    }

    public bool isWin() {
        return gameOver;
    }
}
