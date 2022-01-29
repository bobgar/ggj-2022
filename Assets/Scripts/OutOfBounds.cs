using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        BotController bot = other.transform.GetComponent<BotController>();
        if (bot != null)
        {
            Debug.Log(bot.name + " lost due to going out of bounds");
            bot.Lose();
        }
    }
}
