using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Character
{
    private IState<Bot> currentState; 
    private Vector3 destionation;
    
    public NavMeshAgent agent;

    private void Awake()
    {
        ChangeAnim(Constant.ANIM_IDLE);
    }

    private void Update()
    {
        if (!stopped)
        {
            if (currentState != null && GameManager.Ins.IsState(GameState.Gameplay) && CheckMove(TF.position))
            {
                currentState.OnExecute(this);
            }
        }
            
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    public bool CheckDesination()
    {
        return Vector3.Distance(destionation, new Vector3(TF.position.x, 0, TF.position.z)) < 0.01f;
    }

    public void SetDestination(Vector3 pos)
    {
        agent.enabled = true;
        destionation = pos;
        destionation.y = 0;
        agent.SetDestination(pos);
    }

    public override void Stop()
    {
        base.Stop();
        agent.enabled = false;
    }
}
