using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Match : MonoBehaviour
{
    [SerializeField] private BotController left;
    [SerializeField] private BotController right;
    [SerializeField] private Timer timer;

    private bool hasCompleted = false;

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

        if (right.State == BotState.DEFEATED && hasCompleted == false)
        {
            hasCompleted = true;
            left.Win();
            timer.Stop();
            timer.SetText("Left wins!");

            List<Dictionary<Part, float>> damageByPartList = new List<Dictionary<Part, float>>();
            damageByPartList.Add(left.GetDamageByPart());
            damageByPartList.Add(right.GetDamageByPart());
            MatchResult result = new MatchResult(GameMaster.Artist.TITIAN, damageByPartList);
            GameMaster.Instance.AddMatchResult(result);

            StartCoroutine(waitForEnd());
        }

        if (left.State == BotState.DEFEATED && hasCompleted == false)
        {
            hasCompleted = true;
            right.Win();
            timer.Stop();
            timer.SetText("Right wins!");

            List<Dictionary<Part, float>> damageByPartList = new List<Dictionary<Part, float>>();
            damageByPartList.Add(left.GetDamageByPart());
            damageByPartList.Add(right.GetDamageByPart());
            MatchResult result = new MatchResult(GameMaster.Artist.MICHELANGELO, damageByPartList);
            GameMaster.Instance.AddMatchResult(result);
            
            StartCoroutine(waitForEnd());
        }
    }

    private IEnumerator waitForEnd()
    {
        yield return new WaitForSeconds(3.0f);

        SceneLoader.instance.LoadScene(SceneEnum.END);
        SceneLoader.instance.RemoveScene(SceneEnum.BATTLE);

        Destroy(this);
    }

}