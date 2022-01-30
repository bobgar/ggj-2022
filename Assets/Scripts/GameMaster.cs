using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster _gm;

    [SerializeField] private int matchesToWin = 3;
    public float score = 0;
    public int leftWins = 0;
    public int rightWins = 0;

    public static GameMaster Instance => _gm;

    void Awake()
    {
        if (_gm != null && _gm != this)
        {
            GameObject.Destroy(_gm);
        }
        else
        {
            _gm = this;
        }

        DontDestroyOnLoad(this);
    }

    public float GetScore()
    {
        return score;
    }

    public void AddScore(float newScore)
    {
        this.score += newScore;
    }

    public void IncrementLeftWins()
    {
        leftWins += 1;
    }

    public void IncrementRightWins()
    {
        rightWins += 1;
    }

    public bool IsGameOver()
    {
        return leftWins == matchesToWin || rightWins == matchesToWin;
    }
}