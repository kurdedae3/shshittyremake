using UnityEngine;

public class Discord : MonoBehaviour

{

    [SerializeField]

    private string dcUrl = "https://discord.gg/5ubW2sUZdz";

    // Change @YourChannel to your actual channel's URL.
    public void OpenDiscord() // This defines a Function to open Discord

    {

        Application.OpenURL(dcUrl); // This opens the URL string in the          // defualt browser.

    }

}
