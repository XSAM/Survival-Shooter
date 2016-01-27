using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;

	private float nextFire=0f;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {

        if(Time.time >= nextFire && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.instantHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
			GetComponent<NavMeshAgent>().enabled=false;
        }
    }


    void Attack ()
    {
		nextFire=timeBetweenAttacks+Time.time;

        if(playerHealth.instantHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
