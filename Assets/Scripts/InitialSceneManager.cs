using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public CanvasGroup ContinuePanel;

    private int currentSequence = 0;
    private Animator sceneAnimator;
    private bool sceneStart = false;

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
        StartCoroutine( OpeningSequence() );
    }

    IEnumerator OpeningSequence()
    {
        yield return new WaitForSeconds(1.0f);
        TweenParams tParms = new TweenParams().SetLoops(-1).SetEase(Ease.Linear);
        ContinuePanel.DOFade(1.0f, 1.5f).OnComplete(() =>
            ContinuePanel.DOFade(0.75f, 2.0f).SetAs(tParms)
        );
        sceneStart = true;
    }

    private void Update()
    {
        if( Input.GetMouseButtonDown(0) && sceneStart)
        {
            ContinuePanel.DOKill();
            ContinuePanel.alpha = 0.0f;

            if (DialogWindow.GetComponent<CanvasGroup>().alpha == 0.0f )
            {
                DialogWindow.GetComponent<CanvasGroup>().DOFade(1.0f, 0.25f).SetEase(Ease.OutQuad);
            }

            if (currentSequence >= dialogSequenceIDs.Length)
            {
                SceneLoader.instance.LoadScene(SceneEnum.BATTLE);
                SceneLoader.instance.LoadScene(SceneEnum.PLAN);
                SceneLoader.instance.RemoveScene(SceneEnum.DIALOG);
            }
            else
            {
                TriggerNextDialog();
            }
        }
    }

    private void TriggerNextDialog()
    {
        //Check if our sequence is out of bounds.  If so return.
        if(currentSequence >= dialogSequenceIDs.Length)
        {
            return;
        }

        string stringID = dialogSequenceIDs[currentSequence].dialogID;
        string currentString = StringManager.Instance.GetString(stringID);
        Speaker currentSpeaker = dialogSequenceIDs[currentSequence].speaker;

        StartCoroutine(PlayTalkingAnimation(currentSpeaker, currentString.Length / 50.0f));
        DialogWindow.SetSpeaker(currentSpeaker);
        DialogWindow.SetText(currentString);

        currentSequence++;        
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

        TweenParams tParms = new TweenParams().SetLoops(-1).SetEase(Ease.Linear);
        ContinuePanel.DOFade(1.0f, 1.5f).OnComplete(() =>
            ContinuePanel.DOFade(0.75f, 2.0f).SetAs(tParms)
        );
    }
}
