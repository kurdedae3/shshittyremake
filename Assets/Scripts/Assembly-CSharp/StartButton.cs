using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D2 RID: 210
public class StartButton : MonoBehaviour
{
	// Token: 0x060009E3 RID: 2531 RVA: 0x00026710 File Offset: 0x00024B10
	public void StartGame()
	{
		if (this.currentMode == StartButton.Mode.Story)
		{
			PlayerPrefs.SetString("CurrentMode", "story");
		}
		else if (this.currentMode == StartButton.Mode.Endless)
		{
			PlayerPrefs.SetString("CurrentMode", "endless");
		}
		else if (this.currentMode == StartButton.Mode.Fast)
		{
			PlayerPrefs.SetString("CurrentMode", "fast");
		}
		SceneManager.LoadSceneAsync("School");
	}

	// Token: 0x04000715 RID: 1813
	public StartButton.Mode currentMode;

	// Token: 0x020000D3 RID: 211
	public enum Mode
	{
		Story,
		Endless,
		Fast
	}
}
