using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public enum Artist
    {
        MICHELANGELO,
        TITIAN
    }

    private static GameMaster _gm;

    [SerializeField] private int matchesToWin = 3;
    private float _score = 0;
    private int _michelangeloWins = 0;
    private int _titianWins = 0;

    private List<MatchResult> matchResults = new List<MatchResult>();

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
        return _score;
    }

    public bool IsGameOver()
    {
        return _michelangeloWins == matchesToWin || _titianWins == matchesToWin;
    }

    public List<MatchResult> GetMatchResults()
    {
        return matchResults;
    }

    public void AddMatchResult(MatchResult result)
    {
        if (!result.isTie)
        {
            if (result.winner == Artist.TITIAN)
            {
                _titianWins++;
            }

            if (result.winner == Artist.MICHELANGELO)
            {
                _michelangeloWins++;
            }
        }

        matchResults.Add(result);
        _score += result.GetScore();
    }
}