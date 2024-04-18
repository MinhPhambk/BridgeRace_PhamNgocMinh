using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.SetDestination(LevelManager.Ins.GetCurrentLevel().GetEndPosition());
    }

    public void OnExecute(Bot t)
    {
        if (t.GetBrickCount() == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {
    }
}
