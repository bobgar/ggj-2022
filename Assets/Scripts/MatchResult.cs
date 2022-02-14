﻿using System.Collections.Generic;

public class MatchResult
{
    public Dictionary<Part, float> damageByPart;
    public bool isTie;
    public GameMaster.Artist winner;

    public MatchResult(bool isTie, List<Dictionary<Part, float>> damageByParts)
    {
        this.isTie = isTie;
        damageByPart = new Dictionary<Part, float>();
        foreach (var current in damageByParts)
        foreach (var entry in current)
            if (damageByPart.ContainsKey(entry.Key))
                damageByPart[entry.Key] += entry.Value;
            else
                damageByPart[entry.Key] = entry.Value;
    }

    public MatchResult(GameMaster.Artist winner, List<Dictionary<Part, float>> damageByParts)
    {
        this.winner = winner;
        damageByPart = new Dictionary<Part, float>();
        foreach (var current in damageByParts)
        foreach (var entry in current)
            if (damageByPart.ContainsKey(entry.Key))
                damageByPart[entry.Key] += entry.Value;
            else
                damageByPart[entry.Key] = entry.Value;
    }

    public float GetScore()
    {
        float score = 0;
        foreach (var entry in damageByPart) score += entry.Value;

        return score;
    }
}