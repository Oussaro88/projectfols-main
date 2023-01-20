using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathState : MonoBehaviour, IPlayerBaseState
{
    private GameManager gameManager;

    private PlayerStateMachine playerState;
    private PlayerEntity playerEntityInstance;

    private float timer = 0f;

    public PlayerDeathState(PlayerEntity playerEntity, PlayerStateMachine stateMachine)
    {
        gameManager = GameManager.instance;
        this.playerEntityInstance = playerEntity;
        this.playerState = stateMachine;
    }

    private void PlayerDeath()
    {
        playerEntityInstance.OnDeath();
        ExitState();
    }


    public void EnterState()
    {
        Debug.Log(GetType().Name);
        PlayerDeath();
    }

    public void ExitState()
    {
        timer += Time.deltaTime;
        if(timer > 3f)
        {
            DataPersistenceManager.instance.newSceneLoading = true;
            DataPersistenceManager.instance.gateOpened = false;
            gameManager.levelChanger.GetComponent<LoadScene>().BtnLoadScene("RealHub");
            timer = 0f;
        }
    }

    public void OnUpdate()
    {
        ExitState();
    }


}
