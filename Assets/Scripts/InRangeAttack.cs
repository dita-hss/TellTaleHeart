using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    abstract public bool TryAttack(GameObject ob);
    abstract public bool ObjectInRange(GameObject ob);
}

public class InRangeAttack : Attack
{

    [SerializeField]
    private float _attackRange = 2.0f;
    [SerializeField]
    private float damage = 1.0f; 


    /// <summary>
    /// Tries to attack another object
    /// </summary>
    /// <param name="ob">Object to attack</param>
    /// <returns>Whether the attack was successful</returns>
    public override bool TryAttack(GameObject ob)
    {
        HealthLogic otherHealth = ob.GetComponent<HealthLogic>();

        bool canAttack = otherHealth != null && ObjectInRange(ob);

        if (canAttack)
        {
            otherHealth.Damage((int) damage);
        }
        return canAttack;
    }

    public override bool ObjectInRange(GameObject ob)
    {
        return Vector3.Distance(ob.transform.position, transform.position) < _attackRange;
    }

}
