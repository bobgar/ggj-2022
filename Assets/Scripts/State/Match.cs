using System.Collections;
using System.Collections.Generic;
using Bot;
using UI;
using UnityEngine;
using Utils;

namespace State
{
    public class Match : MonoBehaviour
    {
        [SerializeField] private BotController left;
        [SerializeField] private BotController right;
        [SerializeField] private Timer timer;

        private bool _hasCompleted;

        private void LateUpdate()
        {
            if (_hasCompleted) return;
            
            if (timer.GetTime() <= 0 || left.State == BotState.DEFEATED && right.State == BotState.DEFEATED)
            {
                timer.Stop();
                left.Lose();
                right.Lose();
                timer.SetText("Draw!");

                var damageByPartList = new List<Dictionary<Part, float>>();
                damageByPartList.Add(left.GetDamageByPart());
                damageByPartList.Add(right.GetDamageByPart());

                var result = new MatchResult(true, damageByPartList);
                GameMaster.Instance.AddMatchResult(result);
                
                _hasCompleted = true;
                StartCoroutine(WaitForEnd());
            }

            if (right.State == BotState.DEFEATED)
            {
                timer.Stop();
                left.Win();
                right.Lose();
                timer.SetText(left.getDisplayName() + " wins!");

                var damageByPartList = new List<Dictionary<Part, float>>
                {
                    left.GetDamageByPart(),
                    right.GetDamageByPart()
                };
                var result = new MatchResult(GameMaster.Artist.MICHELANGELO, damageByPartList);
                GameMaster.Instance.AddMatchResult(result);

                _hasCompleted = true;
                StartCoroutine(WaitForEnd());
            }

            if (left.State == BotState.DEFEATED)
            {
                timer.Stop();
                right.Win();
                left.Lose();
                
                timer.SetText(right.getDisplayName() + " wins!");

                var damageByPartList = new List<Dictionary<Part, float>>();
                damageByPartList.Add(left.GetDamageByPart());
                damageByPartList.Add(right.GetDamageByPart());
                var result = new MatchResult(GameMaster.Artist.TITIAN, damageByPartList);
                GameMaster.Instance.AddMatchResult(result);

                _hasCompleted = true;
                StartCoroutine(WaitForEnd());
            }
        }

        private IEnumerator WaitForEnd()
        {
            yield return new WaitForSeconds(3.0f);

            SceneLoader.instance.LoadScene(SceneEnum.END);
            SceneLoader.instance.RemoveScene(SceneEnum.BATTLE);

            Destroy(this);
        }
    }
}