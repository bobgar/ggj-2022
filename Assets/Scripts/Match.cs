using System.Collections.Generic;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private BotController left;
    [SerializeField] private BotController right;
    [SerializeField] private Timer timer;

    void LateUpdate()
    {
        if (timer.GetTime() <= 0 || (left.State == BotState.DEFEATED && right.State == BotState.DEFEATED))
        {
            left.Lose();
            right.Lose();
            timer.Stop();
            timer.SetText("Draw!");

            List<Dictionary<Part, float>> damageByPartList = new List<Dictionary<Part, float>>();
            damageByPartList.Add(left.GetDamageByPart());
            damageByPartList.Add(right.GetDamageByPart());
            
            MatchResult result = new MatchResult(true, damageByPartList);

            GameMaster.Instance.AddMatchResult(result);

            Destroy(this);
        }

        if (right.State == BotState.DEFEATED)
        {
            left.Win();
            timer.Stop();
            timer.SetText("Left wins!");

            List<Dictionary<Part, float>> damageByPartList = new List<Dictionary<Part, float>>();
            damageByPartList.Add(left.GetDamageByPart());
            damageByPartList.Add(right.GetDamageByPart());
            MatchResult result = new MatchResult(GameMaster.Artist.MICHELANGELO, damageByPartList);
            GameMaster.Instance.AddMatchResult(result);


            Destroy(this);
        }

        if (left.State == BotState.DEFEATED)
        {
            right.Win();
            timer.Stop();
            timer.SetText("Right wins!");

            List<Dictionary<Part, float>> damageByPartList = new List<Dictionary<Part, float>>();
            damageByPartList.Add(left.GetDamageByPart());
            damageByPartList.Add(right.GetDamageByPart());
            MatchResult result = new MatchResult(GameMaster.Artist.TITIAN, damageByPartList);
            GameMaster.Instance.AddMatchResult(result);

            Destroy(this);
        }
    }
}