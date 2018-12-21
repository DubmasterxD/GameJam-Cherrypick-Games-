using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMagazineScript : MonoBehaviour
{
    public int ammoLeft = 17;
    private PolicePool policePool;
    public bool canPick = false;

    private void Start()
    {
        policePool = GameObject.FindGameObjectWithTag("PolicePool").GetComponent<PolicePool>();
    }

    public void PickedUp()
    {
        transform.SetPositionAndRotation(policePool.spawnPoints[0].position, policePool.spawnPoints[0].rotation);
        canPick = false;
    }
}
	
