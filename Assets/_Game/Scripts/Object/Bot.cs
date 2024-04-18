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

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
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
        return Vector3.Distance(destionation, Vector3.right * TF.position.x + Vector3.forward * TF.position.z) < 0.1f;
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
