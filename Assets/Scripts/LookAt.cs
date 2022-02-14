using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.LookAt(target.transform);
    }
}