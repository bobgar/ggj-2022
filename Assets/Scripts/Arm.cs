using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Arm : BodyPart
{
    [SerializeField] private Weapon _weapon;

    //Characteristics of Arms.  What do arms have?
    
    private bool _isFirstFrame;


    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        _isFirstFrame = true;
    }

    private void Update()
    {
        // We need this to prevent a race condition. If we start the coroutine
        // in start, it's possible that a bot takes damage before it's summed it's
        // total health using it's body parts.
        if (_isFirstFrame)
        {
            StartCoroutine(Attack());
            _isFirstFrame = false;
        }

        if (state == BodyPartState.DESTROYED)
        {
            StopCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (hitpoints > 0)
        {
            if (_weapon)
            {
                _weapon.Fire(botController.target);
                yield return new WaitForSeconds(_weapon.attackRate);
            }
            else
            {
                // Without this default wait time the coroutine will cause
                // unity to hang
                yield return new WaitForSeconds(1);
            }
        }
    }
}