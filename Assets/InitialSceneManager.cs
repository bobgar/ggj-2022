using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogSection
{
    public Speaker speaker;
    public string dialogID;
}


public class InitialSceneManager : MonoBehaviour
{
    public DialogSection[] dialogSequenceIDs;
    public DialogWindow DialogWindow;

    private int currentSequence = 0;
    private Animator sceneAnimator;
    private static InitialSceneManager _instance;
    public static InitialSceneManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Initial Scene Manager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        sceneAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentSequence = 0;
    }

    private void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            string stringID = dialogSequenceIDs[currentSequence].dialogID;
            string currentString = StringManager.Instance.GetString(stringID);
            Speaker currentSpeaker = dialogSequenceIDs[currentSequence].speaker;

            StartCoroutine(PlayTalkingAnimation(currentSpeaker, currentString.Length / 50.0f));
            DialogWindow.SetSpeaker(currentSpeaker);
            DialogWindow.SetText(currentString);

            currentSequence++;
        }
    }

    IEnumerator PlayTalkingAnimation(Speaker speaker, float time)
    {
        if (speaker == Speaker.michelangelo)
        {
            sceneAnimator.SetBool("MichealTalk", true);
        }
        else if (speaker == Speaker.titian)
        {
            sceneAnimator.SetBool("TitianTalk", true);
        }
        else
        {
            Debug.LogError("Invalid Speaker!");
        }

        yield return new WaitForSeconds(time);
        sceneAnimator.SetBool("MichealTalk", false);
        sceneAnimator.SetBool("TitianTalk", false);


    }
}
