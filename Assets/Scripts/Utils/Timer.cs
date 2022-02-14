using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class Timer : MonoBehaviour
    {
        public float timeRemaining = 10;
        public bool timerIsRunning;
        public Text timeText;

        private void Start()
        {
            timerIsRunning = true;
        }

        private void Update()
        {
            if (!timerIsRunning) return;
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }

        public float GetTime()
        {
            return timeRemaining;
        }

        public void SetText(string text)
        {
            timeText.text = text;
        }

        public void Stop()
        {
            timerIsRunning = false;
        }

        private void DisplayTime(float timeToDisplay)
        {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}