using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	// Use this for initialization
	void Start ()
    {
        policePool = GameObject.FindGameObjectWithTag("PolicePool").GetComponent<PolicePool>();
        aimStart = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("Shoot", Random.value, shootRate);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(isActive)
        {
            Move();
        }
        transform.LookAt(player.transform);
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
        isMoving = false;
        bulletsUsed = 0;
        hp = 100;
        transform.SetPositionAndRotation(policePool.spawnPoints[0].position, policePool.spawnPoints[0].rotation);
    }

    private void Shoot()
    {
        if(PlayerInSight())
        {
            RaycastHit hit;
            Vector3 randomDifference = new Vector3();
            if (player.GetComponent<Rigidbody>().velocity.x + player.GetComponent<Rigidbody>().velocity.z == 0)
            {
                randomDifference = new Vector3(Random.Range(0, aimDifference), Random.Range(0, aimDifference), Random.Range(0, aimDifference));
            }
            else
            {
                randomDifference = new Vector3(Random.Range(0, aimDifference*2), Random.Range(0, aimDifference*2), Random.Range(0, aimDifference*2));
            }
            Physics.Raycast(aimStart.position, player.transform.position + randomDifference - aimStart.position, out hit);
            if (hit.collider.gameObject.tag=="Player")
            {
                player.GetComponent<PlayerMovement>().TakeDamage((int)CalculateDmg(hit.distance));
            }
        }
    }

    private void Move()
    {

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
}
