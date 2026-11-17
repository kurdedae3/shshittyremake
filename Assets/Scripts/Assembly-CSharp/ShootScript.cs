using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ShootScript : MonoBehaviour
{
    public GameObject Baldi;
    public BaldiScript BaldiScript;
    public GameObject FirstPrize;
    public FirstPrizeScript FirstPrizeScript;
    public GameObject NormalBaldi;
    public GameObject DeadBaldi;
    public float delay;
    void Start()
    {
        Baldi = GameObject.Find("Baldi");
        NormalBaldi = GameObject.Find("BaldiSprite");
        BaldiScript = Baldi.GetComponent<BaldiScript>();
        FirstPrize = GameObject.Find("1st Prize");
        FirstPrizeScript = FirstPrize.GetComponent<FirstPrizeScript>();
        Transform deadTr = Baldi.transform.Find("DeadBaldiSprite");
        if (deadTr != null) DeadBaldi = deadTr.gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC") && (other.gameObject == Baldi || other.transform.IsChildOf(Baldi.transform)))
        {  
            Baldi.name = "BaldiDead";
            NormalBaldi.SetActive(false);
            DeadBaldi.SetActive(true);
            BaldiScript.enabled = false;
            StartCoroutine(BaldiWait15SecondsRevive());
        }
        else if (other.CompareTag("NPC") && (other.gameObject == FirstPrize || other.transform.IsChildOf(FirstPrize.transform)))
        {
            FirstPrizeScript.GoCrazy();
        }
    }

    IEnumerator BaldiWait15SecondsRevive()
    {
        yield return new WaitForSeconds(delay);
        Baldi.name = "Baldi";
        NormalBaldi.SetActive(true);
        DeadBaldi.SetActive(false);
        BaldiScript.enabled = true;
    }
}