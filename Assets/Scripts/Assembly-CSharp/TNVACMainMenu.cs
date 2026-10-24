using System;
using UnityEngine;

public class TNVACMainMenu : MonoBehaviour
{

    void Start()
    {
        CheckUnityExplorer();
    }

    void CheckUnityExplorer()
    {
        foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
        {
            string name = asm.GetName().Name;
            
            if (name.Contains("UnityExplorer") || name.Contains("UniverseLib"))
            {
                this.YouAreBanned.SetActive(true);
            }
        }
    }
    public GameObject YouAreBanned;
}