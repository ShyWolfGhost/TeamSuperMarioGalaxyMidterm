﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private PlayerController cont;
    public List<Renderer> rend;
    public int life;
    public int mans;
    public int starbits;
    public int coins;

    public bool invulnerable;
    public float invulnTime;
    public float flashMultiplier;

    public Coroutine invulnRoutine;

    void Start()
    {
        cont = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (invulnerable)
        {
            // Debug.Log(Mathf.Sin(Time.time * flashMultiplier) > 0);
            if (Mathf.Sin(Time.time * flashMultiplier) > 0)
            {
                // Debug.Log("Invis");
                foreach(Renderer mesh in rend)
                {
                    mesh.enabled = false;
                }
            }
            else
            {
                // Debug.Log("Vis");
                foreach (Renderer mesh in rend)
                {
                    mesh.enabled = true;
                }
            }
        }
        else
        {
            foreach (Renderer mesh in rend)
            {
                mesh.enabled = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy" && !invulnerable)
        {
            life--;
            cont.hurtRoutine = StartCoroutine(cont.Hurt(collision.collider.transform));
            invulnRoutine = StartCoroutine(Invuln());

            collision.collider.transform.parent.GetComponent<MushroomOne>().Wait();
        }
    }

    public IEnumerator Invuln()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnTime);
        invulnerable = false;
    }

    public void GetCoin()
    {
        coins++;
        while(coins >= 100)
        {
            coins -= 100;
            mans++;
            //if player gets 100 coins 1 up
        }
    }
    public void GetStarbit()
    {
        starbits++;
    }

    /*public void IfLifeZero()
    {
        mans--;
    }
    //This is a script Taylor Can draw from
    //so ui can change either text or number of playerheads on screen
    //they aren't sure yet
    */
}
