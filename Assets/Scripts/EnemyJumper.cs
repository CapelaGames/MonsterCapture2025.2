using System;
using UnityEngine;

public class EnemyJumper : MonoBehaviour, ITrappable
{
    public StateMachine stateMachine = new StateMachine();

    public ChasingState chasingState;
    public PatrolState patrolState;
    public AttackState attackState;
    public CaptureState captureState;

    private GameObject player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
        chasingState = new ChasingState(player, gameObject, GetComponent<Rigidbody>());
        patrolState = new PatrolState(player, gameObject, GetComponent<Rigidbody>());
        attackState = new AttackState(player, gameObject, GetComponent<Rigidbody>());
        captureState = new CaptureState();

        stateMachine.ChangeState(patrolState);
    }


    void Update()
    {
        stateMachine.UpdateSM();
    }

    public bool IsFacingPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        directionToPlayer.Normalize();

        float dotResult = Vector3.Dot(directionToPlayer, transform.forward);
        Debug.Log(dotResult);
        return dotResult >= 0f;
        /*
        if(dotResult >= 0.95f)
        {
            return true;
        }
        else
        {
            return false;
        }
        */
    }

    private void OnCollisionEnter(Collision other)
    {
        stateMachine.currentState.OnCollisionEnter(other);
    }

    private bool _beingCaptured = false;
    public bool isBeingCaptured { get => _beingCaptured; set => _beingCaptured = value; }

    public bool CaptureAnimation(GameObject trap)
    {
        float shrink = Mathf.Lerp(transform.localScale.x, 0, Time.deltaTime * 2); //Time.time * 20f) * 0.1f + scale;
        transform.localScale = new Vector3(shrink, shrink, shrink);

        transform.position = Vector3.MoveTowards(transform.position, trap.transform.position, 0.003f);

        GetComponent<Rigidbody>().isKinematic = true;

        if (shrink < 0.05f)
            return false;

        return true;
    }

    public int PointValue()
    {
        stateMachine.ChangeState(captureState);
        return 1;
    }
}
