using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x020000DB RID: 219
public class MathGameScript : MonoBehaviour
{
	// Token: 0x060009DC RID: 2524 RVA: 0x00022834 File Offset: 0x00020A34
	public MathGameScript()
	{
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x000228C0 File Offset: 0x00020AC0
	private void Start()
	{
		if (this.gc.notebooks >= 2)
		{
			this.problemsWrong++;
			if (this.gc.mode == "story")
			{
				this.baldiScript.GetAngry(1f);
				this.baldiScript.GetTempAngry(0.25f);
				if (this.gc.mode == "endless")
				{
					this.baldiScript.GetAngry(1f);
				}
			}
		}
		if (this.gc.notebooks == 2)
		{
			this.gc.ActivateSpoopMode();
		}
		if (this.gc.spoopMode)
		{
			this.baldiFeedTransform.position = new Vector3(-1000f, -1000f, 0f);
		}
		if (this.problemsWrong <= 0 & this.gc.mode == "endless")
		{
			this.baldiScript.GetAngry(-1f);
		}
		this.gc.DeactivateLearningGame(base.gameObject);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x000229D4 File Offset: 0x00020BD4
	private void Update()
	{
		if (!this.baldiAudio.isPlaying)
		{
			if (this.audioInQueue > 0 & !this.gc.spoopMode)
			{
				this.PlayQueue();
			}
			this.baldiFeed.SetBool("talking", false);
		}
		else
		{
			this.baldiFeed.SetBool("talking", true);
		}
		if ((Input.GetKeyDown("return") || Input.GetKeyDown("enter")) & this.questionInProgress)
		{
			this.questionInProgress = false;
			this.CheckAnswer();
		}
		if (this.problem > 3)
		{
			this.endDelay -= 1f * Time.unscaledDeltaTime;
			if (this.endDelay <= 0f)
			{
				this.ExitGame();
			}
		}
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00022AB0 File Offset: 0x00020CB0
	private void NewProblem()
	{
		this.playerAnswer.text = string.Empty;
		this.problem++;
		this.playerAnswer.ActivateInputField();
		if (this.problem <= 3)
		{
			this.QueueAudio(this.bal_problems[this.problem - 1]);
			if ((this.gc.mode == "story" & (this.problem <= 2 || this.gc.notebooks <= 1)) || (this.gc.mode == "endless" & (this.problem <= 2 || this.gc.notebooks != 2)))
			{
				this.num1 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
				this.num2 = (float)Mathf.RoundToInt(UnityEngine.Random.Range(0f, 9f));
				this.sign = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num1)]);
				if (this.sign == 0)
				{
					this.solution = this.num1 + this.num2;
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n \n",
						this.num1,
						"+",
						this.num2,
						"="
					});
					this.QueueAudio(this.bal_plus);
				}
				else if (this.sign == 1)
				{
					this.solution = this.num1 - this.num2;
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n \n",
						this.num1,
						"-",
						this.num2,
						"="
					});
					this.QueueAudio(this.bal_minus);
				}
				this.QueueAudio(this.bal_numbers[Mathf.RoundToInt(this.num2)]);
				this.QueueAudio(this.bal_equals);
			}
			else
			{
				this.impossibleMode = true;
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
				this.QueueAudio(this.bal_screech);
				if (this.sign == 0)
				{
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_times);
					this.QueueAudio(this.bal_screech);
				}
				else if (this.sign == 1)
				{
					this.questionText.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
					this.QueueAudio(this.bal_divided);
					this.QueueAudio(this.bal_screech);
					this.QueueAudio(this.bal_plus);
					this.QueueAudio(this.bal_screech);
				}
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText2.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
				}
				else if (this.sign == 1)
				{
					this.questionText2.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
				}
				this.num1 = UnityEngine.Random.Range(1f, 9999f);
				this.num2 = UnityEngine.Random.Range(1f, 9999f);
				this.num3 = UnityEngine.Random.Range(1f, 9999f);
				this.sign = Mathf.RoundToInt((float)UnityEngine.Random.Range(0, 1));
				if (this.sign == 0)
				{
					this.questionText3.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n",
						this.num1,
						"+(",
						this.num2,
						"X",
						this.num3,
						"="
					});
				}
				else if (this.sign == 1)
				{
					this.questionText3.text = string.Concat(new object[]
					{
						"SOLVE MATH Q",
						this.problem,
						": \n (",
						this.num1,
						"/",
						this.num2,
						")+",
						this.num3,
						"="
					});
				}
				this.QueueAudio(this.bal_equals);
			}
			this.questionInProgress = true;
		}
		else
		{
			this.endDelay = 5f;
			if (!this.gc.spoopMode)
			{
				this.questionText.text = "WOW! YOU EXIST!";
			}
			else if (this.gc.mode == "endless" & this.problemsWrong <= 0)
			{
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.questionText.text = this.endlessHintText[num];
			}
			else if (this.gc.mode == "story" & this.problemsWrong >= 3)
			{
				this.questionText.text = "I HEAR MATH THAT BAD";
				this.questionText2.text = string.Empty;
				this.questionText3.text = string.Empty;
				this.baldiScript.Hear(this.playerPosition, 7f);
				this.gc.failedNotebooks++;
			}
			else
			{
				int num2 = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 1f));
				this.questionText.text = this.hintText[num2];
				this.questionText2.text = string.Empty;
				this.questionText3.text = string.Empty;
			}
		}
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x00023318 File Offset: 0x00021518
	public void CheckAnswer()
	{
		if (this.playerAnswer.text == "31718")
		{
			base.StartCoroutine(this.CheatText("THIS IS WHERE IT ALL BEGAN"));
			SceneManager.LoadSceneAsync("TestRoom");
		}
		else if (this.playerAnswer.text == "53045009")
		{
			base.StartCoroutine(this.CheatText("USE THESE TO STICK TO THE CEILING!"));
			this.gc.Fliparoo();
		}
		if (this.problem <= 3)
		{
			if (this.playerAnswer.text == this.solution.ToString() & !this.impossibleMode)
			{
				this.results[this.problem - 1].texture = this.correct;
				this.baldiAudio.Stop();
				this.ClearAudioQueue();
				int num = Mathf.RoundToInt(UnityEngine.Random.Range(0f, 4f));
				this.QueueAudio(this.bal_praises[num]);
				this.NewProblem();
			}
			else
			{
				this.problemsWrong++;
				this.results[this.problem - 1].texture = this.incorrect;
				if (!this.gc.spoopMode)
				{
					this.baldiFeed.SetTrigger("angry");
					this.gc.ActivateSpoopMode();
				}
				if (this.gc.mode == "story")
				{
					if (this.problem == 3)
					{
						this.baldiScript.GetAngry(1f);
					}
					else
					{
						this.baldiScript.GetTempAngry(0.25f);
					}
				}
				else
				{
					this.baldiScript.GetAngry(1f);
				}
				this.ClearAudioQueue();
				this.baldiAudio.Stop();
				this.NewProblem();
			}
		}
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00007FB5 File Offset: 0x000061B5
	private void QueueAudio(AudioClip sound)
	{
		this.audioQueue[this.audioInQueue] = sound;
		this.audioInQueue++;
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00007FD3 File Offset: 0x000061D3
	private void PlayQueue()
	{
		this.baldiAudio.PlayOneShot(this.audioQueue[0]);
		this.UnqueueAudio();
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x000234F8 File Offset: 0x000216F8
	private void UnqueueAudio()
	{
		for (int i = 1; i < this.audioInQueue; i++)
		{
			this.audioQueue[i - 1] = this.audioQueue[i];
		}
		this.audioInQueue--;
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x00007FEE File Offset: 0x000061EE
	private void ClearAudioQueue()
	{
		this.audioInQueue = 0;
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0002353C File Offset: 0x0002173C
	private void ExitGame()
	{
		if (this.problemsWrong <= 0 & this.gc.mode == "endless")
		{
			this.baldiScript.GetAngry(-1f);
		}
		this.gc.DeactivateLearningGame(base.gameObject);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x00023594 File Offset: 0x00021794
	public void ButtonPress(int value)
	{
		if (value >= 0 & value <= 9)
		{
			this.playerAnswer.text = this.playerAnswer.text + value;
		}
		else if (value == -1)
		{
			this.playerAnswer.text = this.playerAnswer.text + "-";
		}
		else
		{
			this.playerAnswer.text = string.Empty;
		}
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x00023618 File Offset: 0x00021818
	private IEnumerator CheatText(string text)
	{
		for (;;)
		{
			this.questionText.text = text;
			this.questionText2.text = string.Empty;
			this.questionText3.text = string.Empty;
			yield return new WaitForEndOfFrame();
		}
		yield break;
	}

	
    // Token: 0x0600098B RID: 2443 RVA: 0x00023E28 File Offset: 0x00022228
    public void ExitGame(VideoPlayer vp)
    {
        if (this.gc.notebooks >= 2)
        {
            if (!this.gc.spoopMode)
            {
                this.gc.ActivateSpoopMode();
            }
            this.baldiScript.GetAngry(1f);
        }
        this.gc.DeactivateLearningGame(base.gameObject);
    }

	// Token: 0x04000693 RID: 1683
	public GameControllerScript gc;

	// Token: 0x04000694 RID: 1684
	public BaldiScript baldiScript;

	// Token: 0x04000695 RID: 1685
	public Vector3 playerPosition;

	// Token: 0x04000696 RID: 1686
	public GameObject mathGame;

	// Token: 0x04000697 RID: 1687
	public RawImage[] results = new RawImage[3];

	// Token: 0x04000698 RID: 1688
	public Texture correct;

	// Token: 0x04000699 RID: 1689
	public Texture incorrect;

	// Token: 0x0400069A RID: 1690
	public TMP_InputField playerAnswer;

	// Token: 0x0400069B RID: 1691
	public TMP_Text questionText;

	// Token: 0x0400069C RID: 1692
	public TMP_Text questionText2;

	// Token: 0x0400069D RID: 1693
	public TMP_Text questionText3;

	// Token: 0x0400069E RID: 1694
	public Animator baldiFeed;

	// Token: 0x0400069F RID: 1695
	public Transform baldiFeedTransform;

	// Token: 0x040006A0 RID: 1696
	public AudioClip bal_plus;

	// Token: 0x040006A1 RID: 1697
	public AudioClip bal_minus;

	// Token: 0x040006A2 RID: 1698
	public AudioClip bal_times;

	// Token: 0x040006A3 RID: 1699
	public AudioClip bal_divided;

	// Token: 0x040006A4 RID: 1700
	public AudioClip bal_equals;

	// Token: 0x040006A5 RID: 1701
	public AudioClip bal_howto;

	// Token: 0x040006A6 RID: 1702
	public AudioClip bal_intro;

	// Token: 0x040006A7 RID: 1703
	public AudioClip bal_screech;

	// Token: 0x040006A8 RID: 1704
	public AudioClip[] bal_numbers = new AudioClip[10];

	// Token: 0x040006A9 RID: 1705
	public AudioClip[] bal_praises = new AudioClip[5];

	// Token: 0x040006AA RID: 1706
	public AudioClip[] bal_problems = new AudioClip[3];

	// Token: 0x040006AB RID: 1707
	public Button firstButton;

	// Token: 0x040006AC RID: 1708
	private float endDelay;

	// Token: 0x040006AD RID: 1709
	private int problem;

	// Token: 0x040006AE RID: 1710
	private int audioInQueue;

	// Token: 0x040006AF RID: 1711
	private float num1;

	// Token: 0x040006B0 RID: 1712
	private float num2;

	// Token: 0x040006B1 RID: 1713
	private float num3;

	// Token: 0x040006B2 RID: 1714
	private int sign;

	// Token: 0x040006B3 RID: 1715
	private float solution;

	// Token: 0x040006B4 RID: 1716
	private string[] hintText = new string[]
	{
		"I GET ANGRIER FOR EVERY PROBLEM YOU GET WRONG",
		"I HEAR EVERY DOOR YOU OPEN"
	};

	// Token: 0x040006B5 RID: 1717
	private string[] endlessHintText = new string[]
	{
		"That's more like it...",
		"Keep up the good work or see me after class..."
	};

	// Token: 0x040006B6 RID: 1718
	private bool questionInProgress;

	// Token: 0x040006B7 RID: 1719
	private bool impossibleMode;

	// Token: 0x040006B8 RID: 1720
	private bool joystickEnabled;

	// Token: 0x040006B9 RID: 1721
	private int problemsWrong;

	// Token: 0x040006BA RID: 1722
	private AudioClip[] audioQueue = new AudioClip[20];

	// Token: 0x040006BB RID: 1723
	public AudioSource baldiAudio;

	 public VideoPlayer videoPlayer;
}