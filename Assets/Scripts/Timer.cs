using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float startTime = 90.0f;
    public float timeLeft = 0.0f;
    public Text startText; // used for showing countdown from 3, 2, 1 
    public HealthBar player1;
    public HealthBar player2;

    public GameObject player1Obj;
    public GameObject player2Obj;
    private float time_taken_reward = -0.003f;
    private float oldTime;

    void Start()
    {
        timeLeft = startTime;
        oldTime = timeLeft;
    }

    public void Reset()
    {
        timeLeft = startTime;
        oldTime = timeLeft;
    }
    public float getTime() {
        return timeLeft;
    }
    void Update()
    {
        
        timeLeft -= Time.deltaTime;
        if (oldTime - timeLeft > 1) {
            player1Obj.GetComponent<FighterAIAgent>().giveReward(time_taken_reward);
            player2Obj.GetComponent<FighterAIAgent>().giveReward(time_taken_reward);
            oldTime = timeLeft;
        }
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //Do something useful or Load a new game scene depending on your use-case
            if (player1.GetHealth() > player2.GetHealth()) {
                Debug.Log("Player 1 WINS");
                player1Obj.GetComponent<FighterAIAgent>().SetReward(+1);
                player2Obj.GetComponent<FighterAIAgent>().SetReward(-1f);
            } else if (player1.GetHealth() < player2.GetHealth()) {
                Debug.Log("Player 2 WINS");
                player2Obj.GetComponent<FighterAIAgent>().SetReward(+1f);
                player1Obj.GetComponent<FighterAIAgent>().SetReward(-1f);
            } else {
                Debug.Log("DRAW");
                player1Obj.GetComponent<FighterAIAgent>().SetReward(-0.25f);
                player2Obj.GetComponent<FighterAIAgent>().SetReward(-0.25f);
            }
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Reset();
            player2Obj.GetComponent<FighterAIAgent>().EndEpisode();
            player1Obj.GetComponent<FighterAIAgent>().EndEpisode();
        }
    }
}