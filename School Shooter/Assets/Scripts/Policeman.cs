using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Policeman : MonoBehaviour {

    public bool canMove = false;
    public bool isActive = false;
    private bool isMoving = false;
    private int hp = 100;
    public float shootRate = 1;
    private int bulletsUsed = 0;
    public float aimDifference = 5;
    private PolicePool policePool;
    private Transform aimStart;
    private GameObject player;
    private Animator anim;
    private NavMeshAgent navAgent;
    private AudioSource gunSource;

	// Use this for initialization
	void Awake ()
    {
        policePool = GameObject.FindGameObjectWithTag("PolicePool").GetComponent<PolicePool>();
        aimStart = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        gunSource = GetComponent<AudioSource>();
        InvokeRepeating("Shoot", Random.value, shootRate);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isMoving)
        {
            transform.LookAt(player.transform);
        }
	}

    public void TakeDamage(float dmg)
    {
        hp -= (int)dmg;
        if(hp<=0)
        {
            Die();
        }
    }

    public void Headshot()
    {
        player.GetComponent<Headshot>().ActivateHeadshot();
        Die();
    }

    private void Die()
    {
        policePool.SpawnMagazine(17 - bulletsUsed % 17, gameObject.transform.position, gameObject.transform.rotation);
        if (!canMove)
        {
            Destroy(gameObject);
        }
        else
        {
            MovingKilled();
        }
    }

    private void MovingKilled()
    {
        isActive = false;
        bulletsUsed = 0;
        hp = 100;
    }

    private void Shoot()
    {
        if (PlayerInSight())
        {
            gunSource.Play();
            bulletsUsed++;
        }
        RaycastHit hit;
        Vector3 randomDifference = new Vector3();
        if (player.GetComponent<Rigidbody>().velocity.x + player.GetComponent<Rigidbody>().velocity.z == 0)
        {
            randomDifference = new Vector3(Random.Range(0, aimDifference), Random.Range(0, aimDifference), Random.Range(0, aimDifference));
        }
        else
        {
            randomDifference = new Vector3(Random.Range(0, aimDifference * 2), Random.Range(0, aimDifference * 2), Random.Range(0, aimDifference * 2));
        }
        Physics.Raycast(aimStart.position, player.transform.position + randomDifference - aimStart.position, out hit);
        if (hit.collider.gameObject.tag == "Player")
        {
            gunSource.Play();
            player.GetComponent<PlayerMovement>().TakeDamage((int)CalculateDmg(hit.distance));
        }

    }

    private IEnumerator Move()
    {
        while (true)
        {
            if (isActive)
            {
                if (PlayerInSight())
                {
                    isMoving = false;
                    anim.SetBool("isMoving", isMoving);
                    navAgent.isStopped = true;
                }
                else
                {
                    isMoving = true;
                    anim.SetBool("isMoving", isMoving);
                    navAgent.isStopped = false;
                    navAgent.SetDestination(player.transform.position);
                }
            }
            else
            {
                isMoving = false;
                anim.SetBool("isMoving", isMoving);
                navAgent.isStopped = true;
                transform.SetPositionAndRotation(policePool.spawnPoints[0].position + new Vector3(0,-100,0), policePool.spawnPoints[0].rotation);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    bool PlayerInSight()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, player.transform.position - transform.position, out hit);
        if (hit.collider.gameObject.tag == "Player")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    float CalculateDmg(float distance)
    {
        float dmgTaken = 0;
        if (distance < 50)
        {
            dmgTaken = 100 - distance;
        }
        else if (distance < 100)
        {
            dmgTaken = 90 - 0.8f * distance;
        }
        else
        {
            dmgTaken = 10;
        }
        return dmgTaken;
    }

    public void ActivateMovable()
    {
        canMove = true;
        StartCoroutine(Move());
    }
}
