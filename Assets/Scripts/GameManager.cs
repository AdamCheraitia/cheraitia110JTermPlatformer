using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int EnemyCount;
    public static int Score;
    public TMP_Text Timer;
    public TMP_Text EnemyText;
    public float TimerTime;
    // Start is called before the first frame update
    void Start()
    {
        EnemyCount = 0;
        Score = 0;
    }

    //Here in Update, we control a 3 minute timer. I also just realised that the timer doesn't do anythng when it ends... oops
    void Update()
    {
        TimerTime -= Time.deltaTime;
        Timer.text = Mathf.RoundToInt(TimerTime).ToString();
        if( EnemyCount == 20)
        {
            SceneManager.LoadScene(3);
        }
        EnemyText.text = EnemyCount.ToString() + "/20";
        if(TimerTime <= 0 && EnemyCount != 20)
        {
            SceneManager.LoadScene(2);
        }
    }
}
