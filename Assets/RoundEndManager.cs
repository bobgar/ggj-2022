using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

struct TestPartDamage
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
        List<GameObject> winsActive = new List<GameObject>();
        int i = 0;
        foreach(MatchResult match in matches)
        {
            if (match.winner == GameMaster.Artist.MICHELANGELO)
            {
                winsActive.Add(topRowWins[i].gameObject);
            }
            else if (match.winner == GameMaster.Artist.TITIAN)
            {
                winsActive.Add(bottomRowWins[i].gameObject);
            }
            i++;
        }

        Sequence winsSequence = DOTween.Sequence();
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
        List<TestPartDamage> partDamage = new List<TestPartDamage>();
        MatchResult match = matches[matches.Count - 1];

        //Read off the Parent Object what all the damaged components are
        Sequence partsSequence = DOTween.Sequence();
        float total = 0.0f;
        foreach(var damageDescription in match.damageByPart)
        {
            partsSequence.AppendCallback(() => CreateLineEntry(damageDescription.Key.ToString(), (int)damageDescription.Value));
            partsSequence.AppendInterval(0.25f);
            total += damageDescription.Value;
        }
        partsSequence.AppendInterval(0.50f);
        partsSequence.AppendCallback(() => totalAmountText.text = ((int)total).ToString() + "c");
        partsSequence.Play();
    }

    void CreateLineEntry(string partName, int partCost)
    {
        GameObject newLine = Instantiate(textLinePrefab, LedgerSummaryRoot.transform);
        TextRow newRow = newLine.GetComponent<TextRow>();
        newRow.expenseText.text = partName;
        newRow.numberText.text = partCost.ToString() + "c";
    }

    public void EndRoundEnd()
    {
        SceneLoader.instance.LoadScene(SceneEnum.BATTLE);
        SceneLoader.instance.LoadScene(SceneEnum.PLAN);
        SceneLoader.instance.RemoveScene(SceneEnum.END);
    }
}
