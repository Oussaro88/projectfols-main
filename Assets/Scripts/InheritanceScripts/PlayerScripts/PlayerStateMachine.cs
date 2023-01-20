using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class PlayerStateMachine
{

    private IPlayerBaseState currentState;

    public PlayerStateMachine(IPlayerBaseState startingState)
    {
        currentState = startingState; //Set the starting state
        currentState.EnterState();
    }

    public void ChangeState(IPlayerBaseState newState)
    {
        //if (currentState != null)
        //{
        //    currentState.ExitState(); // Resetting the state before any change
        //}

        currentState = newState; // Setting the new state

        currentState.EnterState(); // Entering the new set state
    }


    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }


}
