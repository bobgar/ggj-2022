using UnityEngine;
using UnityEngine.UI;

public class BattleSceneManager : MonoBehaviour
{
    public CameraSwoop cameraSwoop;
    public GameObject HUD;

    public BotController LeftBot;
    public BotController RightBot;

    public Slider leftHealth;
    public Slider rightHealth;

    // Update is called once per frame
    private void Update()
    {
        leftHealth.value = LeftBot.GetHealthPercentage();
        rightHealth.value = RightBot.GetHealthPercentage();
    }

    public void StartCamera()
    {
        cameraSwoop.gameObject.SetActive(true);
    }

    public void StartBattle()
    {
        HUD.SetActive(true);
        LeftBot.StartBattle();
        RightBot.StartBattle();
    }
}