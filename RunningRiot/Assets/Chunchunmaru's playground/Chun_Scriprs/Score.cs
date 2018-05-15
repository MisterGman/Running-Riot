using System;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour {

    public Player player;
    public Text score;
    public Text scoreRank;
    public Text lifes;

    private float timeScore = 0;
    private String rank = "";
    private int scoreLife = 1000;

    void Update () {
        timeScore += Time.deltaTime;
        ExtraLife();
        CheckRank(player.score);
        score.text = player.score.ToString("0");
        scoreRank.text = rank;
        lifes.text = "Lifes: " + player.life;
	}

    private void CheckRank(float x)
    {
        switch ((int)x)
        {
            case 200:
                rank = "D";
                break;
            case 400:
                rank = "C";
                break;
            case 600:
                rank = "B";
                break;
            case 800:
                rank = "A";
                break;
            case 1000:
                rank = "SSS";
                break;
        }
    }

    public void ExtraLife()
    {

        if(player.score == scoreLife)
        {
            player.life += 1;
            scoreLife += 1000;
        }

    
    }
}
