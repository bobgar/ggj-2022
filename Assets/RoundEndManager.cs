using System.Collections.Generic;
using DG.Tweening;
using State;
using UI;
using UnityEngine;
using UnityEngine.UI;

internal struct TestPartDamage
{
    public string partName;
    public int partDmg;

    public TestPartDamage(string s, int i)
    {
        partName = s;
        partDmg = i;
    }
}

public class RoundEndManager : MonoBehaviour
{
    public Text[] topRowWins;
    public Text[] bottomRowWins;

    public GameObject LedgerSummaryRoot;
    public GameObject textLinePrefab;
    public Text totalAmountText;

    private List<MatchResult> matches;

    private void Start()
    {
        WinSummarySequence();
    }

    private void WinSummarySequence()
    {
        //Grab Win Data
        matches = GameMaster.Instance.GetMatchResults();

        //Set the wins according to round history
        var winsActive = new List<GameObject>();
        var i = 0;
        foreach (var match in matches)
        {
            if (match.winner == GameMaster.Artist.MICHELANGELO)
                winsActive.Add(topRowWins[i].gameObject);
            else if (match.winner == GameMaster.Artist.TITIAN) winsActive.Add(bottomRowWins[i].gameObject);
            i++;
        }

        var winsSequence = DOTween.Sequence();
        foreach (var go in winsActive)
        {
            winsSequence.AppendCallback(() => go.SetActive(true));
            winsSequence.AppendInterval(0.25f);
        }

        winsSequence.AppendCallback(() => RoundSummarySequence());
        winsSequence.Play();
    }

    private void RoundSummarySequence()
    {
        var partDamage = new List<TestPartDamage>();
        var match = matches[matches.Count - 1];

        //Read off the Parent Object what all the damaged components are
        var partsSequence = DOTween.Sequence();
        var total = 0.0f;
        foreach (var damageDescription in match.damageByPart)
        {
            partsSequence.AppendCallback(() =>
                CreateLineEntry(damageDescription.Key.ToString(), (int)damageDescription.Value));
            partsSequence.AppendInterval(0.25f);
            total += damageDescription.Value;
        }

        partsSequence.AppendInterval(0.50f);
        partsSequence.AppendCallback(() => totalAmountText.text = ((int)total) + "c");
        partsSequence.Play();
    }

    private void CreateLineEntry(string partName, int partCost)
    {
        var newLine = Instantiate(textLinePrefab, LedgerSummaryRoot.transform);
        var newRow = newLine.GetComponent<TextRow>();
        newRow.expenseText.text = partName;
        newRow.numberText.text = partCost + "c";
    }

    public void EndRoundEnd()
    {
        SceneLoader.instance.LoadScene(SceneEnum.BATTLE);
        SceneLoader.instance.LoadScene(SceneEnum.PLAN);
        SceneLoader.instance.RemoveScene(SceneEnum.END);
    }
}