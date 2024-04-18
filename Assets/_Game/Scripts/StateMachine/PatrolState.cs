using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;

    public void OnEnter(Bot t)
    {
        targetBrick = Random.Range(1, 12);
        t.ChangeAnim(Constant.ANIM_RUN);
        SeekTarget(t);
    }

    public void OnExecute(Bot t)
    {
        if (t.CheckDesination())
        {
            if (t.GetBrickCount() < targetBrick)
            {
                SeekTarget(t);
            }
            else
            {
                t.ChangeState(new AttackState());
            }
        }
    }

    public void OnExit(Bot t)
    {
    }

    private void SeekTarget(Bot t)
    {
        if (t.GetStage() != null)
        {
            Brick brick = t.GetStage().GetBrickByColor(t.color);

            if (brick == null)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                t.SetDestination(brick.TF.position);
            }
        }
        else
        {
            t.SetDestination(t.TF.position);
        }
    }

}
