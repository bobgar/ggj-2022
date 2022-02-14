using Bot;
using UnityEngine;

namespace UI
{
    public class PlanningSceneManager : MonoBehaviour
    {
        private static PlanningSceneManager _instance;
        public Canvas gameCanvas;

        public BotController LeftBot;
        public BotController RightBot;

        public static PlanningSceneManager Instance
        {
            get
            {
                if (_instance == null) Debug.LogError("Planning Scene Manager is NULL!");
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
            var s = SceneLoader.instance.GetScene(SceneEnum.BATTLE);
            foreach (var g in s.GetRootGameObjects())
                if (g.GetComponent<BotController>() != null)
                {
                    if (g.name == "Left Bot") LeftBot = g.GetComponent<BotController>();
                    if (g.name == "Right Bot") RightBot = g.GetComponent<BotController>();
                }
        }

        public void Fight()
        {
            var s = SceneLoader.instance.GetScene(SceneEnum.BATTLE);
            foreach (var g in s.GetRootGameObjects())
                if (g.GetComponent<Camera>() != null)
                    g.SetActive(true);

            SceneLoader.instance.RemoveScene(SceneEnum.PLAN);
        }
    }
}