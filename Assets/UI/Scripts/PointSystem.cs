using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Threading;
using System;

public class PointSystem : MonoBehaviour {

    public static int score = 0;

    private static Text score_value;

    void Start()
    {
        score_value = GetComponentInChildren<Text>();
    } 

    public static void SetScore(int score)
    {
        PointSystem.score = score;
        score_value.text = "$" + PointSystem.score;
    }

    public static void AddToScore(int amountToAdd)
    {
        PointSystem.score += amountToAdd;
        score_value.text = "$" + PointSystem.score;
    }

    private void updateScoreUI()
    {
        score_value.text = "$" + score;
    }

    public static void SubtractFromScore(int amountToRemove)
    {
        PointSystem.score -= amountToRemove;
        score_value.text = "$" + PointSystem.score;
    }

}
