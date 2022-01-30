using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanningSceneManager : MonoBehaviour
{
    public Canvas gameCanvas;

    public BotController LeftBot;
    public BotController RightBot;

    private static PlanningSceneManager _instance;
    public static PlanningSceneManager Instance
    {
        get
        {
            if( _instance == null)
            {
                Debug.LogError("Planning Scene Manager is NULL!");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        gameCanvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        //Note need to load Battle scene first to be able to grab here.
        Scene s = SceneLoader.instance.GetScene(SceneEnum.BATTLE);
        foreach (GameObject g in s.GetRootGameObjects())
        {
            if (g.GetComponent<BotController>() != null)
            {
                if (g.name == "Left Bot")
                {
                    LeftBot = g.GetComponent<BotController>();
                }
                if (g.name == "Right Bot")
                {
                    RightBot = g.GetComponent<BotController>();
                }
            }
        }
    }

    public void Fight()
    {
        Scene s = SceneLoader.instance.GetScene(SceneEnum.BATTLE) ;
        foreach (GameObject g in s.GetRootGameObjects())
        {
            if(g.GetComponent<Camera>() != null)
            {
                g.SetActive(true);
            }
        }

        SceneLoader.instance.RemoveScene(SceneEnum.PLAN);
    }
}
