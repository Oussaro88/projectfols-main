using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    //State Machine Exemple from Sebastian Graves and his Video "A.I State Machine Made Easy (Unity)".
    public abstract EnemyState RunState(EnemyBehaviour enemyBehaviour); 
}
