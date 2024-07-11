using TMPro;
using UnityEngine;

public class Time_Manager : MonoBehaviour
{
    public TextMeshProUGUI timer;
    public static bool startStop = true;
    public static Time_Manager instance;
    private static float time_;
    public float time { get { return time_; } set { if (value <= instance._timeInspector && value >= 0) time_ = value; } }
    public float _timeInspector;

    public void startTimer()
    {
        startStop = false;
        time = 120;
        time = _timeInspector;
        DisplayTime(time);
    }

    private void Start()
    {
        DisplayTime(_timeInspector);
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
       
    }
    void Update()
    {
        if (!startStop)
        {
            time -= Time.deltaTime;
            DisplayTime(time);
        }
    }

    public static float minutes;
    public static float seconds;
    void DisplayTime(float timeToDisplay)
    {
        minutes = Mathf.FloorToInt(timeToDisplay / 60);
        seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
