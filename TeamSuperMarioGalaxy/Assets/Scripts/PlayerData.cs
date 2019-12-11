using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

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
    public bool mansDecrease = false;

    public Coroutine invulnRoutine;

    public TextMeshProUGUI coinsText, livesText;

    public static int mansToStartWith = 3;

    void Start()
    {
        cont = GetComponent<PlayerController>();

        mans = mansToStartWith;
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
        if(collision.collider.tag == "Enemy" && cont.jumpState == PlayerController.JumpState.FALLING)
        {
            Destroy(collision.collider.transform.parent.parent.gameObject);
            GetCoin();
        }
        else if(collision.collider.tag == "Enemy" && !invulnerable)
        {
            life--;

            if(life <= 0)
            { 
                mansToStartWith--;
                Debug.Log("Lost a life. Current lives now: " + mansToStartWith);
                if(mansToStartWith <= 0)
                {
                    Debug.Log("No more lives remaining." + " Player loses.");
                    SceneManager.LoadScene(2);
                }
                else
                {
                    Debug.Log("Lives remaining. Reloading scene.");
                    SceneManager.LoadScene(1);
                }
            }
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
            mansToStartWith++;
            //if player gets 100 coins 1 up
        }
    }
    public void GetStarbit()
    {
        starbits++;
    }

    /*public void IfLifeZero()
    {
        //mansDecrease = false;
        /*if (mansDecrease == false)
        {
            mans= mans-1;
            mansDecrease = true;
        }
        
        if (mans <= 0)
        {
            SceneManager.LoadScene("You Lose"); //Load the menu scene or a You died try again scene
        }
    }*/

    //This is a script Taylor Can draw from
    //so ui can change either text or number of playerheads on screen
    //they aren't sure yet
    
}
