using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum Speaker
{ 
    none = 0,
    michelangelo,
    titian
}

public class DialogWindow : MonoBehaviour
{
    public Text DialogText;
    public Text SpeakerLabel;
    public GameObject MichelangeloSprite;
    public GameObject TitianSprite;

    private const string MichelangeloName = "Michelangelo";
    private const string TitianName = "Titian";

    public void SetText(string text)
    {
        DialogText.text = "";
        float time = text.Length / 50.0f;
        DialogText.DOText(text, time).SetEase(Ease.Linear);
    }
    public void SetSpeaker(Speaker speaker)
    {
        if (speaker == Speaker.michelangelo)
        {
            MichelangeloSprite.SetActive(true);
            TitianSprite.SetActive(false);
            SpeakerLabel.text = MichelangeloName;
        }
        else if (speaker == Speaker.titian)
        {
            MichelangeloSprite.SetActive(false);
            TitianSprite.SetActive(true);
            SpeakerLabel.text = TitianName;
        }
        else
        {
            Debug.LogError("Not a valid speaker!");
        }
    }
}
