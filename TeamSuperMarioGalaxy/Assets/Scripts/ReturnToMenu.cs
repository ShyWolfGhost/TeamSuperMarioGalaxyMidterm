using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Return;

    public AudioSource Sourceend;

    public AudioClip Clippy;

    public float soundTimer;

    public bool startedPlaying;
    // Start is called before the first frame update
    void Start()
    {
        soundTimer = 1.7f;
        Button playButton = Return.GetComponent<Button>();
        playButton.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (startedPlaying == true && Sourceend.isPlaying == false)
        {
            SceneManager.LoadScene("MenuScreen");
        } 
    }

    void TaskOnClick()
    {
        if (Sourceend.isPlaying == false)
        {
            Sourceend.PlayOneShot(Clippy);
            startedPlaying = true;
            //StartCoroutine(DelaySceneLoad());

            /*IEnumerator DelaySceneLoad()
            {
                yield return new WaitForSeconds(1.0f);
                SceneManager.LoadScene("USEABLE STARTING WORLD");
            }*/
            //InvokeRepeating("SoundTimerCountDown", Time.deltaTime, Time.deltaTime);
            Debug.Log("soundTimer: " + soundTimer);
            /*if (soundTimer<0)
            {
                SceneManager.LoadScene("USEABLE STARTING WORLD");
    
            }*/

            Debug.Log("Play Sound; Disable the physical object; Change Scene");
        }
        if(Sourceend.isPlaying== true)
        {
            Debug.Log("You Thought....LMFAO");
        }
    }
    /*void SoundTimerCountDown()
    {
        soundTimer -= Time.deltaTime;
    }*/
}

