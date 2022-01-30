using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneManager : MonoBehaviour
{
    public CameraSwoop cameraSwoop;
    public GameObject HUD;

    public BotController LeftBot;
    public BotController RightBot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCamera()
    {
        cameraSwoop.gameObject.SetActive(true);
    }

    public void StartBattle()
    {
        HUD.SetActive(true);
    }
}
