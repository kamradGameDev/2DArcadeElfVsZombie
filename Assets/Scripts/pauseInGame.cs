using UnityEngine;
using System.Collections;

public class pauseInGame : MonoBehaviour
{
    public static pauseInGame instance;

    void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
    }

    public void startPauseTime()
    {
        StartCoroutine(timeGame(1));
    }
    private IEnumerator timeGame(float PauseTime)
    {
        
        Time.timeScale = 0;
        float pauseEndTime = Time.realtimeSinceStartup + PauseTime;
        while(Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
    }
}
