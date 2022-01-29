using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningSceneManager : MonoBehaviour
{
    public Canvas gameCanvas;

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
}
