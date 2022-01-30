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

    private void Start()
    {
        WinSummarySequence();
    }
    
    private void WinSummarySequence()
    {
        //TestLoader Data
        bool[] mWins = { true, true, false, false, true };
        bool[] tWins = { false, false, true, true, false };

        //Set the wins according to round history
        List<GameObject> winsActive = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            if (mWins[i] == false && tWins[i] == false) continue;
            if (mWins[i])
            {
                winsActive.Add(topRowWins[i].gameObject);
            }
            else if (tWins[i])
            {
                winsActive.Add(bottomRowWins[i].gameObject);
            }
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
        //Test Data
        List<TestPartDamage> partDamage = new List<TestPartDamage>();
        partDamage.Add(new TestPartDamage("Mech Arm", 175));
        partDamage.Add(new TestPartDamage("Mech Arm", 132));
        partDamage.Add(new TestPartDamage("Mech Head", 81));
        partDamage.Add(new TestPartDamage("Core", 35));
        partDamage.Add(new TestPartDamage("Mech Arm", 55));
        partDamage.Add(new TestPartDamage("Mech Arm", 232));
        partDamage.Add(new TestPartDamage("Mech Head", 151));
        partDamage.Add(new TestPartDamage("Core", 62));
        int total = 0;

        //Read off the Parent Object what all the damaged components are
        Sequence partsSequence = DOTween.Sequence();
        foreach(TestPartDamage part in partDamage)
        {
            partsSequence.AppendCallback(() => CreateLineEntry(part.partName, part.partDmg));
            partsSequence.AppendInterval(0.25f);
            total += part.partDmg;
        }
        partsSequence.AppendInterval(0.50f);
        partsSequence.AppendCallback(() => totalAmountText.text = total.ToString() + "c");
        partsSequence.Play();
    }

    void CreateLineEntry(string partName, int partCost)
    {
        GameObject newLine = Instantiate(textLinePrefab, LedgerSummaryRoot.transform);
        TextRow newRow = newLine.GetComponent<TextRow>();
        newRow.expenseText.text = partName;
        newRow.numberText.text = partCost.ToString() + "c";
    }
}
