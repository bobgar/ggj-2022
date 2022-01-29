using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Arm : BodyPart
{
    [SerializeField] private Weapon _weapon;
    //Characteristics of Arms.  What do arms have?


    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        StartCoroutine(Attack());
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