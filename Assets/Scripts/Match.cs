using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private BotController left;
    [SerializeField] private BotController right;
    [SerializeField] private Timer timer;
    private float _score = 0.0f;

    void LateUpdate()
    {
        if (timer.GetTime() <= 0 || (left.State == BotState.DEFEATED && right.State == BotState.DEFEATED))
        {
            left.Lose();
            right.Lose();
            timer.Stop();
            timer.SetText("Draw!");
            _score = left.GetDamage() + right.GetDamage();
            GameMaster.Instance.score += _score;
            Destroy(this);
        }
        if (right.State == BotState.DEFEATED)
        {
            left.Win();
            timer.Stop();
            timer.SetText("Left wins!");
            _score = left.GetDamage() + right.GetDamage();
            GameMaster.Instance.score += _score;
            GameMaster.Instance.IncrementLeftWins();
            Destroy(this);
        }
        if (left.State == BotState.DEFEATED)
        {
            right.Win();
            timer.Stop();
            timer.SetText("Right wins!");
            _score = left.GetDamage() + right.GetDamage();
            GameMaster.Instance.IncrementRightWins();
            GameMaster.Instance.score += _score;
            Destroy(this);
        }
    }
}