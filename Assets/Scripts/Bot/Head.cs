using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : BodyPart
{
    [SerializeField] private Weapon _weapon;

    private bool _isFirstFrame;

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

        if (state == BodyPartState.DESTROYED || state == BodyPartState.GAME_OVER)
        {
            StopCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        while (hitpoints > 0 && _weapon && state != BodyPartState.DESTROYED && state != BodyPartState.GAME_OVER)
        {
            _weapon.Fire(this, botController.target, botController.childrenColliders);
            yield return new WaitForSeconds(_weapon.attackRate);
        }
    }
}
