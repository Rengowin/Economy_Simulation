using UnityEngine;
[DefaultExecutionOrder(-1)]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField]
    int currentTimeScale = 1;
    [SerializeField]
    int maxTimeScale = 10;
    [SerializeField]
    int minTimeScale = 1;
    bool isPaused = false;
    int timeScaleBeforePause;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = currentTimeScale;
    }

    public void ChangeTimeScale(int change)
    {
        if(isPaused)
        {
            return;
        }
        currentTimeScale += change;
        currentTimeScale = Mathf.Clamp(currentTimeScale, minTimeScale, maxTimeScale);
        Time.timeScale = currentTimeScale;
    }

    public void PauseGame()
    {
        if(isPaused)
        {
            isPaused = false;
            Time.timeScale = timeScaleBeforePause;
        }
        else
        {
            isPaused = true;
            timeScaleBeforePause = currentTimeScale;
            Time.timeScale = 0;
        }
    }
}
