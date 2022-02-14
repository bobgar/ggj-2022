using Bot;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        var bot = other.transform.GetComponent<BotController>();
        if (bot != null)
        {
            Debug.Log(bot.name + " lost due to going out of bounds");
            bot.Lose();
        }
    }
}