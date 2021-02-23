using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float startTime = 90.0f;
    public float timeLeft = 0.0f;
    [SerializeField] Text startText; // used for showing countdown from 3, 2, 1 

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 0)
        {
            //Do something useful or Load a new game scene depending on your use-case
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}