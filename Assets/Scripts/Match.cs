using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private BotController left;
    [SerializeField] private BotController right;
    [SerializeField] private Timer timer;
    private bool isActive;

    private void Awake()
    {
        isActive = true;
    }

    void LateUpdate()
    {
        if (timer.GetTime() <= 0 || (left.State == BotState.DEFEATED && right.State == BotState.DEFEATED))
        {
            left.Lose();
            right.Lose();
            isActive = false;
            timer.Stop();
            timer.SetText("Draw!");
        }
        if (right.State == BotState.DEFEATED)
        {
            isActive = false;
            left.Win();
            timer.Stop();
            timer.SetText("Left wins!");
        }
        if (left.State == BotState.DEFEATED)
        {
            isActive = false;
            right.Win();
            timer.Stop();
            timer.SetText("Right wins!");
        }
    }
}