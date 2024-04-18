using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim(Constant.ANIM_RUN);
        targetBrick = Random.Range(3, 13);
        SeekTarget(t);
    }

    public void OnExecute(Bot t)
    {
        if (t.CheckDesination())
        {
            if (t.GetBrickCount() >= targetBrick)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                SeekTarget(t);
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
