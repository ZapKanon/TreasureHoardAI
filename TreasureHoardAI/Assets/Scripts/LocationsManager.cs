using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationsManager : MonoBehaviour
{
    [SerializeField] public Transform hoardPoint;
    [SerializeField] public List<Transform> dragonPatrolPoints;
    [SerializeField] public List<Transform> depositPoints;
    [SerializeField] public List<Adventurer> allAdventurers;

    [SerializeField] TMP_Text blueScoreText;
    [SerializeField] TMP_Text redScoreText;
    [SerializeField] TMP_Text greenScoreText;
    [SerializeField] TMP_Text yellowScoreText;

    [SerializeField] TMP_Text gameOverText;

    [SerializeField] int blueScore = 0;
    [SerializeField] int redScore = 0;
    [SerializeField] int greenScore = 0;
    [SerializeField] int yellowScore = 0;

    [SerializeField] int pointsToWin = 5;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScores();
        gameOverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Add 1 point to a specified team's score
    public void AddToScore(Team team)
    {
        switch (team)
        {
            case Team.Blue:
                blueScore++;
                break;
            case Team.Red:
                redScore++;
                break;
            case Team.Green:
                greenScore++;
                break;
            case Team.Yellow:
                yellowScore++;
                break;
        }

        UpdateScores();
    }

    //Update all displayed score texts
    public void UpdateScores()
    {
        blueScoreText.text = blueScore.ToString();
        redScoreText.text = redScore.ToString();
        greenScoreText.text = greenScore.ToString();
        yellowScoreText.text = yellowScore.ToString();

        //If any team has reached 5 or more points, declare that team the winner
        if (blueScore >= pointsToWin)
        {
            DeclareWinner(Team.Blue, Color.blue);
        }
        else if (redScore >= pointsToWin)
        {
            DeclareWinner(Team.Red, Color.red);
        }
        else if (greenScore >= pointsToWin)
        {
            DeclareWinner(Team.Green, Color.green);
        }
        else if (yellowScore >= pointsToWin)
        {
            DeclareWinner(Team.Yellow, Color.yellow);
        }

        //If all adventurers are dead, the dragon wins
        if (CheckDeadAdventurers())
        {
            GameOver();
        }
    }

    //End the game and decalre a winning team
    public void DeclareWinner(Team winningTeam, Color winningColor)
    {
        Time.timeScale = 0;
        gameOverText.color = winningColor;
        gameOverText.text = winningTeam.ToString() + " Team Wins!";
    }

    //The game ends if all adventurers are dead
    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverText.color = new Color(1.0f, 0.65f, 0.0f);
        gameOverText.text = "All adventurers have been eaten.\nThe Dragon Wins!";
    }

    //Check each adventurer to see if they're dead
    public bool CheckDeadAdventurers()
    {
        int deadAdventurers = 0;
        foreach (Adventurer adventurer in allAdventurers)
        {
            if (adventurer.gameObject.activeInHierarchy == false)
            {
                deadAdventurers++;
            }
        }

        //Return true if they're all dead
        if (deadAdventurers >= allAdventurers.Count)
        {
            return true;
        }

        return false;
    }
}
