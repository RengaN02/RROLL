using UnityEngine;
using UnityEngine.AI; // NavMesh

public class Enemy : RenBehaviour
{
    [Header("Saldırı Ayarları")]
    public float attackRange = 2f;
    public int damage = 10;
    public float timeBetweenAttacks = 1.5f;
    
    NavMeshAgent agent;
    Transform player;
    float nextAttackTime = 0f;
    

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }

    }

    public override void Tick(float deltaTime)
    {

        if (player == null) return;

        if(!agent.enabled) return;

        float distance = Vector3.Distance(transform.position, player.position);


        if (distance > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
        else
        {
            agent.isStopped = true;
            
            FacePlayer(deltaTime);

            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + timeBetweenAttacks;
            }
        }
    }

    void FacePlayer(float deltaTime)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, deltaTime * 5f);
    }

    void Attack()
    {
        Debug.Log(name + " oyuncuya vurdu!");

        Health playerHealth = player.GetComponent<Health>();
        
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    public void Die()
    {
        GameManager.instance.meshAgents.Remove(agent);
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}