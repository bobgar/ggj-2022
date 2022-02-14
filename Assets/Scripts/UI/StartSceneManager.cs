using UnityEngine;

namespace UI
{
    public class StartSceneManager : MonoBehaviour
    {
        private void Start()
        {
        }

        public void OnStartButton()
        {
            Debug.Log("Start Button Pressed");
            SceneLoader.instance.LoadScene(SceneEnum.DIALOG);
            SceneLoader.instance.RemoveScene(SceneEnum.START);
        }

        public void OnExitButton()
        {
            Debug.Log("Exit Button Pressed");
            Application.Quit();
        }
    }
}