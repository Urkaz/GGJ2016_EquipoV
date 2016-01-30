using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour {

    public void OnStartPresed()
    {
        SceneManager.LoadScene(1);
    }
}
