using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStart : EnemyState
{
    public Material defaultMat; 
    public GameObject character;
    public GameObject weapon;
    public GameObject itemSmall;
    public GameObject itemLarge;
    public GameObject itemMask;
    public Material dissolveMat;
    public float cutoffValueChar = 0;
    public float cutoffValueWep = 0;
    public float cutoffValueSmall = 0;
    public float cutoffValueLarge = 0;
    public float cutoffValueMask = 0;

    public GameObject spawnVFXInstant;
    public GameObject spawnVFX;
    public float timer;
    public bool once;
    public bool onceI;
    public bool onceP;
    public bool start;

    public StateWaiting stateWaiting;
    public StateWander stateWander;
    public StatePursue statePursue;
    public StateKnocked stateKnocked;
    public StateDeath stateDeath;

    public override EnemyState RunState(EnemyBehaviour enemyBehaviour)
    {
        //Code to Initialize once, done in children

        return stateWander;
    }
}
