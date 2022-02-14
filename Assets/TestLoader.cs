using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoader : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        SceneManager.LoadScene("Test Battle Scene", LoadSceneMode.Additive);
    }
}