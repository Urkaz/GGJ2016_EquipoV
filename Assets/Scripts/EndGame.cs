using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

    GameObject derrotaImage;
    GameObject victoriaImage;
    public AudioClip victoryClip;
    public AudioClip losingClip;

    AudioSource endSound;

    void Start()
    {
        derrotaImage = GameObject.Find("DerrotaImage");
        derrotaImage.SetActive(false);
        victoriaImage = GameObject.Find("VictoriaImage");
        victoriaImage.SetActive(false);
        endSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.Instance.isWin())
        {
            victoriaImage.SetActive(true);
            if(!endSound.isPlaying || endSound.clip == losingClip)
            {
                endSound.clip = victoryClip;
                endSound.Play();
            }
        }
        else
        {
            derrotaImage.SetActive(true);
            if (!endSound.isPlaying || endSound.clip == victoryClip)
            {
                endSound.clip = losingClip;
                endSound.Play();
            }
        }
	}
}
