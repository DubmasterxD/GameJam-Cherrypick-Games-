﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{

    public int[] bulletsInMagazines = { 17, 0, 0 };
    public Text bulletfLeftText;
    public float shotsDelay = 1 / 3.0f;
    private float timeSinceLastShot = 0;
    public float reloadDelay = 0.5f;
    private float timeSinceReload = 0;
    public float pickAmmoRange = 10;
    public float shootRange = 300;
    public GameObject studentPool;
    private StudentPool pool;
    public Text pickUpAmmoText;
    private GameObject lastSeenMagazine;
    public Transform shootPoint;
    public Image guiMagazine1;
    public Image guiMagazine2;
    public Image guiMagazine3;
    private Animator anim;
    private Score score;
    private AudioSource gunAudioSource;
    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip pickMagazineSound;

    // Use this for initialization
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        pool = studentPool.GetComponent<StudentPool>();
        score = GameObject.FindGameObjectWithTag("Score").GetComponent<Score>();
        gunAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanShootReload())
        {
            if (bulletsInMagazines[0] == 0)
            {
                Reload();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
        }
        bulletfLeftText.text = bulletsInMagazines[0].ToString();
        if (CanPickMagazine())
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PickUpMagazine();
            }
        }
    }

    void PickUpMagazine()
    {
        gunAudioSource.clip = pickMagazineSound;
        gunAudioSource.Play();
        if (bulletsInMagazines[1] == 0)
        {
            bulletsInMagazines[1] = lastSeenMagazine.GetComponent<DropMagazineScript>().ammoLeft;
            guiMagazine2.gameObject.SetActive(true);
        }
        else
        {
            bulletsInMagazines[2] = lastSeenMagazine.GetComponent<DropMagazineScript>().ammoLeft;
            guiMagazine3.gameObject.SetActive(true);
        }
        lastSeenMagazine.GetComponent<DropMagazineScript>().PickedUp();
    }

    bool CanPickMagazine()
    {
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, pickAmmoRange);
        if (hit.collider != null && hit.collider.gameObject.layer == 11) //Ammo
        {
            if (bulletsInMagazines[2] == 0)
            {
                pickUpAmmoText.gameObject.SetActive(true);
                lastSeenMagazine = hit.collider.gameObject;
                return true;
            }
        }
        pickUpAmmoText.gameObject.SetActive(false);
        return false;
    }

    bool CanShootReload()
    {
        timeSinceLastShot += Time.deltaTime;
        timeSinceReload += Time.deltaTime;
        if (timeSinceLastShot > shotsDelay && timeSinceReload > reloadDelay)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Reload()
    {
        if (bulletsInMagazines[1] != 0)
        {
            gunAudioSource.clip = reloadSound;
            gunAudioSource.Play();
            anim.SetTrigger("reload");
            timeSinceReload = 0;
            bulletsInMagazines[0] = bulletsInMagazines[1];
            bulletsInMagazines[1] = bulletsInMagazines[2];
            bulletsInMagazines[2] = 0;
            guiMagazine3.gameObject.SetActive(false);
            if (bulletsInMagazines[1] == 0)
            {
                guiMagazine2.gameObject.SetActive(false);
            }
            guiMagazine1.gameObject.SetActive(true);
        }
    }

    void Shoot()
    {
        if (bulletsInMagazines[0] != 0)
        {
            gunAudioSource.clip = shotSound;
            gunAudioSource.Play();
            anim.SetTrigger("shot");
            timeSinceLastShot = 0;
            bulletsInMagazines[0]--;
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(shootPoint.position, shootPoint.forward, out hit, shootRange);
            if(hit.collider.gameObject.tag=="Student")
            {
                pool.studentCount--;
                pool.SetStudentsLeftText();
                score.StudentKill();
                Destroy(hit.collider.gameObject);
            }
            else if(hit.collider.gameObject.tag=="Police")
            {
                if (hit.collider.isTrigger)
                {
                    hit.collider.gameObject.GetComponent<Policeman>().Headshot();
                }
                else
                {
                    hit.collider.gameObject.GetComponent<Policeman>().TakeDamage(CalculateDmg(hit.distance));
                }
            }
            if (bulletsInMagazines[0] == 0)
            {
                guiMagazine1.gameObject.SetActive(false);
            }
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
