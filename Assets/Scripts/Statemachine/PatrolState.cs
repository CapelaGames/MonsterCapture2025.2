using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IState
{
    EnemyJumper owner;

    GameObject player;
    GameObject ai;
    Rigidbody rb;

    public PatrolState(GameObject player, GameObject ai, Rigidbody rb)
    {
        this.player = player;
        this.ai = ai;
        owner = ai.GetComponent<EnemyJumper>();
        this.rb = rb;
    }

    public void Enter()
    {
        Debug.Log("Entering Patrol State");
    }

    public void Execute()
    {
        ai.transform.rotation *= Quaternion.Euler(0f,50f * Time.deltaTime, 0f);

        if (owner.IsFacingPlayer())
        {
            // Transition to chase state
            owner.stateMachine.ChangeState(owner.chasingState);
        }
    }

    public void Exit()
    {
        Debug.Log("Exiting Patrol State");
    }
}
