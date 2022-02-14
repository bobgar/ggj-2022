using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public enum Artist
    {
        MICHELANGELO,
        TITIAN
    }

    [SerializeField] private int matchesToWin = 3;

    private readonly List<MatchResult> matchResults = new();
    private int _michelangeloWins;
    private float _score;
    private int _titianWins;

    public static GameMaster Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(Instance);
        else
            Instance = this;

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
            if (result.winner == Artist.TITIAN) _titianWins++;

            if (result.winner == Artist.MICHELANGELO) _michelangeloWins++;
        }

        matchResults.Add(result);
        _score += result.GetScore();
    }
}