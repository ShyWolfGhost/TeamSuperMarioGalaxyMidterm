using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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

    public TextMeshProUGUI coinsText, livesText;

    void Start()
    {
        cont = GetComponent<PlayerController>();
    }

    void Update()
    {
        UpdateUI();

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

    void UpdateUI()
    {
        livesText.text = "LIVES x " + mans;
        coinsText.text = "x " + coins;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy" && !invulnerable)
        {
            life--;
            /*if (life == 0)
            {
                IfLifeZero();  
            }*/
            
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
        if(life < 3)
        {
            life++;
        }
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
        if (mans == 0)
        {
            SceneManager.LoadScene( )//Load the menu scene or a You died try again scene
        }
    }
    //This is a script Taylor Can draw from
    //so ui can change either text or number of playerheads on screen
    //they aren't sure yet
    */
}
