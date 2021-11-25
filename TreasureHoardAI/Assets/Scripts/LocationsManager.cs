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

    [SerializeField] TMP_Text blueScoreText;
    [SerializeField] TMP_Text redScoreText;
    [SerializeField] TMP_Text greenScoreText;
    [SerializeField] TMP_Text yellowScoreText;

    [SerializeField] int blueScore = 0;
    [SerializeField] int redScore = 0;
    [SerializeField] int greenScore = 0;
    [SerializeField] int yellowScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        UpdateScores();
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
    }
}
