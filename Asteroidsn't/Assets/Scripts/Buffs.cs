using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour {

    public enum Bonuses { Plus, x2, Destroy}; //x2 tak na prawdę nie przyspiesza podwójnie
    public Bonuses bonus;
    public float edge = 5.07f;

    private void Update()
    {
        if (transform.position.x > edge)
        {
            transform.SetPositionAndRotation(new Vector3(-edge, transform.position.y, 0), transform.rotation);
        }
        if (transform.position.x < -edge)
        {
            transform.SetPositionAndRotation(new Vector3(edge, transform.position.y, 0), transform.rotation);
        }
        if (transform.position.y > edge && transform.position.y<GameControl.instance.gameObject.GetComponent<SpawningBuffs>().waitPlace.y-1)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, -edge, 0), transform.rotation);
        }
        if(transform.position.y > GameControl.instance.gameObject.GetComponent<SpawningBuffs>().waitPlace.y - 1 && transform.position.y < GameControl.instance.gameObject.GetComponent<SpawningBuffs>().waitPlace.y)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, GameControl.instance.gameObject.GetComponent<SpawningBuffs>().waitPlace.y + 5, 0), Quaternion.identity);
        }
        if (transform.position.y < -edge)
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, edge, 0), transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            switch (bonus)
            {
                case Bonuses.Plus:
                    GameControl.instance.LifesLeft = GameControl.instance.LifesLeft + 1;
                    GameControl.instance.lifesLeftText.text = "Lifes : " + GameControl.instance.LifesLeft;
                    break;
                case Bonuses.x2:
                    GameControl.instance.player.GetComponent<Arrow>().shotsDelay /= 8 / 7f;
                    break;
                case Bonuses.Destroy:
                    for (int i = 0; i < 360; i++)
                    {
                        Instantiate(GameControl.instance.player.GetComponent<Arrow>().shot, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.Euler(0, 0, i));
                    }
                    break;
                default:
                    break;
            }
            transform.SetPositionAndRotation(GameControl.instance.gameObject.GetComponent<SpawningBuffs>().waitPlace, transform.rotation);
        }
    }
}
