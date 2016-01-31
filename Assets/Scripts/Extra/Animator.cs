using UnityEngine;
using System.Collections;

public class Animator : MonoBehaviour {

    public Sprite[] walkFrames;
    public Sprite[] jumpFrames;

    public enum State { WALK, JUMP };
    private State currentState = State.WALK;

    public float timeBetweenFrames = 0.1f;
    private int currentIndex = 0;

    private SpriteRenderer sr;

    private float timer = 0;

    // Use this for initialization
    void Start() {
        sr = transform.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (timer > timeBetweenFrames) {
            currentIndex++;
            timer = 0;
            if (currentState == State.WALK) {
                if (currentIndex >= walkFrames.Length) {
                    currentIndex = 0;
                }
                sr.sprite = walkFrames[currentIndex];
            }
            else if (currentState == State.JUMP)
            {
                if (currentIndex >= jumpFrames.Length)
                {
                    currentIndex = 0;
                }
                sr.sprite = jumpFrames[currentIndex];
            }
        }
    }
    public void SetState(State state)
    {
        currentState = state;
    }
}
