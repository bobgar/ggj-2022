using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    START,
    INIT,
    PLAN,
    BATTLE,
    END
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    public Dictionary<SceneEnum, string> scenes = new Dictionary<SceneEnum, string>()
    {
        { SceneEnum.START,"Start Scene" },
        { SceneEnum.INIT,"Initial Scene" },
        { SceneEnum.PLAN ,"Planning Scene" },
        { SceneEnum.BATTLE,"Battle Scene" },        
        { SceneEnum.END,"End Scene" }
    };

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SceneManager.LoadScene(scenes[SceneEnum.START], LoadSceneMode.Additive);
    }

    public void LoadScene(SceneEnum scene)
    {
        SceneManager.LoadScene(scenes[scene], LoadSceneMode.Additive);
    }

    public void RemoveScene(SceneEnum scene)
    {
        SceneManager.UnloadSceneAsync(scenes[scene]);
    }

    public Scene GetScene(SceneEnum scene)
    {
        return SceneManager.GetSceneByName(scenes[scene]);
    }

}
