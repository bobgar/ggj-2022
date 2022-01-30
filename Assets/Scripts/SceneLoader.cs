using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneEnum
{
    START,
    DIALOG,
    PLAN,
    BATTLE,
    END
}

public class SceneLoader : MonoBehaviour
{
    public AudioSource menuMusic;
    public AudioSource battleMusic;

    public static SceneLoader instance;

    public Dictionary<SceneEnum, string> scenes = new Dictionary<SceneEnum, string>()
    {
        { SceneEnum.START,"Start Scene" },
        { SceneEnum.DIALOG,"Dialog Scene" },
        { SceneEnum.PLAN ,"Planning Scene" },
        { SceneEnum.BATTLE,"Battle Scene" },        
        { SceneEnum.END,"Round End Scene" }
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

        if(scene == SceneEnum.DIALOG || scene == SceneEnum.END)
        {
            menuMusic.Play();
            battleMusic.Stop();
        }
    }

    public void RemoveScene(SceneEnum scene)
    {
        SceneManager.UnloadSceneAsync(scenes[scene]);
        if (scene == SceneEnum.PLAN)
        {
            menuMusic.Stop();
            battleMusic.Play();
        }
    }

    public Scene GetScene(SceneEnum scene)
    {
        return SceneManager.GetSceneByName(scenes[scene]);
    }

}
