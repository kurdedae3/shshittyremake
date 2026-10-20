using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
 
public class StartRoundTimer : MonoBehaviour
{
    float timeLeft = 22.75f; //casz (czas)/time
    //float timeLeft2 = 15.0f;
    
    public TMP_Text text; //https://gamebanana.com/tuts/13205
    public GameObject borderobject; //the thing that freezes you at start of a round and stops after 15 seconds
    public AudioClip aud_aftersong; //da song aka. the song from legacy csgo that played after you move from spawn if your a CT
    public GameObject songobject; // the song that plays on start of round from legacy csgo, also al of them are just bit crushed so valve wont sue the fuck out of me/ guthib wont delete my repo
    public GameObject overlayobject; //grayoverlay
    public AudioSource audioDevice; //the thing that plays it
    public GameObject characters; //they appear after this ends
    public GameObject thetimer;
    public GameObject theCountdown;
    public bool hasPlayed = false;
    //jezeli ktos powie ze to z vibe codowalem (vibe coded/uzycie ai), zycze ci zeby towja rodzina zostala potracona przez tira, a ciebie zebys sie zaksztusil tym powietrzem ktore wdychasz
    void Update()
    {
        timeLeft -= Time.deltaTime;
        text.text = "00:" + Mathf.CeilToInt(timeLeft);
        if(timeLeft < 0)
        {
            this.borderobject.SetActive(false); //this is the thingy that freezes you
            this.overlayobject.SetActive(false);
            this.theCountdown.SetActive(false);
            this.thetimer.SetActive(true);
            if (!hasPlayed && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
			{
                audioDevice.PlayOneShot(aud_aftersong);
                hasPlayed = true; 
                this.songobject.SetActive(false);
                this.characters.SetActive(true);
			}
        }
    }
}