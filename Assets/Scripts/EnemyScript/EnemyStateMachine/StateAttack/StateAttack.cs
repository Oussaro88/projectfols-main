using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : EnemyState
{
    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        return this;
    }
}
