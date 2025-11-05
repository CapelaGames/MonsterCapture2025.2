using UnityEngine;

public class AttackState : IState
{
    EnemyJumper owner;
    
    GameObject player;
    GameObject ai;
    Rigidbody rb;

    private float expire;

    public AttackState(GameObject player, GameObject ai, Rigidbody rb)
    {
        this.player = player;
        this.ai = ai;  
        owner = ai.GetComponent<EnemyJumper>();
        this.rb = rb; 
    }
    public void Enter()
    {
        Debug.Log("Entering Attack State");

        expire = Time.time + 3f;
        
        ai.transform.localScale = new Vector3(ai.transform.localScale.x * 0.4f,
            ai.transform.localScale.y * 0.4f,
            ai.transform.localScale.z * 3);
        Vector3 direction = player.transform.position - ai.transform.position;
        rb.AddForce(direction.normalized * 100f);
    }

    public void Execute()
    {
        if (Time.time > expire)
        {
            owner.stateMachine.ChangeState(owner.patrolState);
        }
    }

    public void Exit()
    {
        ai.transform.localScale = Vector3.one;
        Debug.Log("Entering Attack State");
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            Vector3 hitDirection = player.transform.position - other.contacts[0].point;
            playerRb.AddForce(hitDirection.normalized * 100f * rb.linearVelocity.magnitude);
            
            ai.transform.localScale = Vector3.one;
        }
    }
}
