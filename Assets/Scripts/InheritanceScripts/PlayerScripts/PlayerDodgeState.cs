using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerEntity playerEntityInstance;
    private PlayerStateMachine playerState;

    public PlayerDodgeState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;

    }

    private void Dodge()
    {
        playerEntityInstance.hasExecutedDodge = true;
        playerEntityInstance.renderedCharacter.GetComponent<Renderer>().material = playerEntityInstance.dodgeMaterial;
        playerEntityInstance.fxElectricity.SetActive(true);
        playerEntityInstance.Speed = playerEntityInstance.DodgeSpeed;  //Valeur de la vitesse en mode esquive
        playerEntityInstance.Animator.SetBool("Dive", true);
        playerEntityInstance.MyCharacter.Move(playerEntityInstance.DodgeVelocity * playerEntityInstance.Speed * Time.deltaTime);

    }

    public void EnterState()
    {
        Debug.Log(GetType().Name);
    }

    public void ExitState()
    {
        playerEntityInstance.DodgeTime += Time.deltaTime;
        if (playerEntityInstance.DodgeTime >= 0.3f)
        {
            playerEntityInstance.IsDodging = false;
            playerEntityInstance.DodgeTime = 0;
            playerEntityInstance.Animator.SetBool("Dive", false);
            playerEntityInstance.Speed = playerEntityInstance.ResetSpeedValue;  //Valeur de la vitesse en mode esquive
            playerEntityInstance.renderedCharacter.GetComponent<Renderer>().material = playerEntityInstance.defaultMaterial;
            playerEntityInstance.fxElectricity.SetActive(false);
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.DefaultState);
        }
    }

    public void OnUpdate()
    {
        Dodge();

        ExitState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trap"))
        {
            playerEntityInstance.playerState.ChangeState(playerEntityInstance.KnockedState);
        }
    }
}
