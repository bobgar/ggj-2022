using System.Collections.Generic;

public class MatchResult
{
    public bool isTie = false;
    public GameMaster.Artist winner;
    public Dictionary<Part, float> damageByPart;

    public MatchResult(bool isTie, List<Dictionary<Part, float>> damageByParts)
    {
        this.isTie = isTie;
        damageByPart = new Dictionary<Part, float>();
        foreach (Dictionary<Part, float> current in damageByParts)
        {
            foreach (KeyValuePair<Part, float> entry in current)
            {
                if (damageByPart.ContainsKey(entry.Key))
                {
                    damageByPart[entry.Key] += entry.Value;
                }
                else
                {
                    damageByPart[entry.Key] = entry.Value;
                }
            }
        }
    }

    public MatchResult(GameMaster.Artist winner, List<Dictionary<Part, float>> damageByParts)
    {
        this.winner = winner;
        damageByPart = new Dictionary<Part, float>();
        foreach (Dictionary<Part, float> current in damageByParts)
        {
            foreach (KeyValuePair<Part, float> entry in current)
            {
                if (damageByPart.ContainsKey(entry.Key))
                {
                    damageByPart[entry.Key] += entry.Value;
                }
                else
                {
                    damageByPart[entry.Key] = entry.Value;
                }
            }
        }
    }

    public float GetScore()
    {
        float score = 0;
        foreach (KeyValuePair<Part, float> entry in damageByPart)
        {
            score += entry.Value;
        }
        return score;
    }
}