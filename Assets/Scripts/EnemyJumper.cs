using UnityEngine;

public class EnemyJumper : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public ChasingState chasingState;
    public PatrolState patrolState;

    private GameObject player;


    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
        chasingState = new ChasingState(player, gameObject, GetComponent<Rigidbody>());
        patrolState = new PatrolState(player, gameObject, GetComponent<Rigidbody>());

        stateMachine.ChangeState(patrolState);
    }


    void Update()
    {
        stateMachine.UpdateSM();
    }

    public bool IsFacingPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - transform.forward;
        directionToPlayer.Normalize();

        float dotResult = Vector3.Dot(directionToPlayer, transform.forward);
        //Debug.
        return dotResult >= 0.8f;
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
}
