using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;


public class RadialAiNotes : MonoBehaviour
{

    public GameObject healthPanel;
    
    public Image healthPanelImage;

    public GameObject player;
    //public int healthNumb;
    public TextMeshProUGUI displayHealthNumb;
    //public Animation state3;
    //public Animation state2;
    //public Animation state1;
    public Animator healthAnimator;
    //public Image playerHead;
    public TextMeshProUGUI displayMansNumb;


    private int lastCheckHealth;
    
    /*
     * player.GetComponent<PlayerData>().life = the pannel
     * player.GetComponent<PlayerData>().mans = the player heads
     * 
     */
    
    
    
    
    
    
    
    void Start()
    {
        player.GetComponent<PlayerData>();
        lastCheckHealth = player.GetComponent<PlayerData>().life;
        //Image healthPanelImage = GetComponent<Image>();
        
        //state1 = GetComponent<Animation>();
        //state2 = GetComponent<Animation>();
        //state3 = GetComponent<Animation>();
        
    }
    void Update()
    {
//displayMansNumb.text= "Lives X" + player.GetComponent<PlayerData>().mans.ToString();
//The Mans text isn't updating when placed here
        
        //ON player collide with enemy WHICH I WON'T BE ABLE TO ACCESS HEALTH GOES DOWN 1
        
        //displayHealthNumb.text = healthNumb.ToString();
        if (player.GetComponent<PlayerData>().life == 3 && player.GetComponent<PlayerData>().life != lastCheckHealth)
                 {
                     //code
                     //healthPanel.fill
                     //Fill amount = 1
                     //healthPanelImage.image.fuck
                     healthAnimator.SetTrigger("GoToState3");
                     displayHealthNumb.text = "3";

                 }
        if (player.GetComponent<PlayerData>().life == 2 && player.GetComponent<PlayerData>().life != lastCheckHealth)
        {
            //code
            //0.66666666666
            //healthPanelImage.image.fi
            healthAnimator.SetTrigger("GoToState2");
            displayHealthNumb.text = "2";
            //displayMansNumb.text= "Lives X" + player.GetComponent<PlayerData>().mans.ToString();

        }
        if (player.GetComponent<PlayerData>().life == 1 && player.GetComponent<PlayerData>().life != lastCheckHealth)
        {
            //code
            //0.3333333333333
            //Pulse animation
            healthAnimator.SetTrigger("GoToState1");
            displayHealthNumb.text = "1";
        }
        if (player.GetComponent<PlayerData>().life == 0)
        {
            //code
            //reset the value to 3 than cause a loss of mario.
            //player.GetComponent<PlayerData>().IfLifeZero();
            //displayMansNumb.text= "Lives X" + player.GetComponent<PlayerData>().mans.ToString();
        }
        lastCheckHealth = player.GetComponent<PlayerData>().life;
    }

    //Radial Ui Life stuff
    //    USE THE UI
    //     Use a pannel
    //     Make A filled Image
    //     The Filled image than becomes radial
    //     Eventually what will happenIN CODE
    //         when mario Looses health A third of the Circle Dissappears
    //         There is a number 3,2,1 printed on circle
    //        For every hit mario takes, The number of health decreases
    //         The Color Will change when health gets to 1
    //         Health pulsates at 1 (maybe at 2 as well)
    //        Once Health gets to zero, Mario, looses a life and the circle resets

    /*
     * public Int health;
     * void UiOnPlayerLossOf health()
     * {
     * health= health-1;
     * if(health <=0)
     * {
     * }
     */
}
