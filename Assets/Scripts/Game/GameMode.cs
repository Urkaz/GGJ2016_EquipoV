using UnityEngine;
using System.Collections;

public class GameMode : MonoBehaviour {

    public enum GameState
    {
        Puzzle,
        Preparation,
        Combat,
        Win,
        Lose
    }

    public int bonusPoints = 500;
    public int basePoints = 200;
    private int mTotalPoints;

    public float timeWithoutLosingPoints = 5f;
    public float timeToLoseAllBonus = 10f;
    private float mPuzzleTimer = 0f;
    private bool mPuzzleFinished = false;

    public float trapPostioningFaseTime = 30f;
    private float mTrapPostioningFaseTimer = 0f;

    private GameState mState = GameState.Puzzle;

    public int mummyHealth = 50;

    // Use this for initialization
    void Start () {
        timeToLoseAllBonus += timeWithoutLosingPoints;

    }
	
	// Update is called once per frame
	void Update () {
        switch (mState)
        {
            case GameState.Puzzle:
                //Debug.Log("Time: " + mPuzzleTimer + " Points: " + CalculatePoints());
                if (mPuzzleFinished)
                {
                    CalculatePoints();
                    mState = GameState.Preparation;
                    break;
                }
                mPuzzleTimer += Time.deltaTime;

                break;
            case GameState.Preparation:
                mTrapPostioningFaseTimer += Time.deltaTime;
                if (mTrapPostioningFaseTimer >= trapPostioningFaseTime)
                    mState = GameState.Combat;
                break;
            case GameState.Combat:

                break;
        }
	}

    private int CalculatePoints()
    {
        if (mPuzzleTimer < timeWithoutLosingPoints)
            mTotalPoints = bonusPoints + basePoints;
        else if (mPuzzleTimer < timeToLoseAllBonus)
        {

            mTotalPoints = (int)((float)(bonusPoints) * (1f - (mPuzzleTimer - timeWithoutLosingPoints) / (timeToLoseAllBonus - timeWithoutLosingPoints)));
            mTotalPoints += basePoints;
        }
        else
            mTotalPoints = basePoints;
        return mTotalPoints;
    }
}
