using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TotalyNotValveAntiCheat : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenuTNVAC"; 

    void Start()
    {
        CheckUnityExplorer();
    }

    void CheckUnityExplorer()
    {   
        bool cheaterr = false;
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            string name = asm.GetName().Name;
            
            if (name.Contains("UnityExplorer") || name.Contains("UniverseLib"))
            {
                cheaterr=true;
                Debug.LogWarning("[TotalyNotValveAntiCheatLog]:using uexplorer for baldi mods in big 026 :wilted_rose:, re-fucking-think your life");
                youhavebeenvotedout();
                return;
            }
        }
        if (cheaterr==false)
        {
            Debug.Log("[TotalyNotValveAntiCheatLog]:no cheat, good boy/girl/nonbinary person/human");
        }
    }

    void youhavebeenvotedout()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}