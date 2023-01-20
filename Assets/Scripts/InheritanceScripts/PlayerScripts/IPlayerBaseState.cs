using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBaseState
{

    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void OnUpdate();
  
}
