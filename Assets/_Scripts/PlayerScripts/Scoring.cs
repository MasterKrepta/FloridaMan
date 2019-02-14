using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    [SerializeField]public static int CurrentScore;
    public static int HighScore;

    private void Awake() {
        //PlayerPrefs.DeleteAll();
    }
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey("HighScore")) {
            HighScore = PlayerPrefs.GetInt("HighScore", HighScore);
        }
        else {
            PlayerPrefs.SetInt("HighScore", HighScore);
        }

        GameEvents.OnGooseDied += IncreaseScore;
        GameEvents.OnPlayerDied += CheckHighScore;
    }


    void IncreaseScore(Unit u) {
        CurrentScore += u.PointsToGive;
        Debug.Log(CurrentScore + " points ");
        Debug.Log(HighScore + " High Score");
    }

    void CheckHighScore() {
        if (HighScore < CurrentScore) {
            HighScore = CurrentScore;

            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
}
