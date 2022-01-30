using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Initial Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Battle Scene", LoadSceneMode.Additive);
        SceneManager.LoadScene("Planning Scene", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
