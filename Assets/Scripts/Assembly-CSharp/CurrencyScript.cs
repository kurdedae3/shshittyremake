using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class CurrencyScript : MonoBehaviour
{
    public GameControllerScript gc;
 
    public TextMeshProUGUI textMesh;
 
    public Transform playerTransform;
 
    double Money = 0.00;
    // Start is called before the first frame update
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        textMesh.text = Money + "$".ToString();
        if (Money >= 0.25)
        {
            Ray ray3 = Camera.main.ScreenPointToRay(new Vector3((float)(Screen.width / 2), (float)(Screen.height / 2), 0f));
			RaycastHit raycastHit3;
			if (Physics.Raycast(ray3, out raycastHit3))
				{
				if (raycastHit3.collider.name == "BSODAMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
                        if (Input.GetMouseButtonDown(0))
                        {
                            Money = Money - 0.25;
                            this.gc.ResetItem();
                            this.gc.CollectItem(4);
                        }
					}
					else if (raycastHit3.collider.name == "ZestyMachine" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
                          if (Input.GetMouseButtonDown(0))
                        {
                            Money = Money - 0.25;
                            this.gc.ResetItem();
                            this.gc.CollectItem(1);
                        }
					}
					else if (raycastHit3.collider.name == "PayPhone" & Vector3.Distance(this.playerTransform.position, raycastHit3.transform.position) <= 10f)
					{
                        Money = Money - 0.25;
                        this.gc.ResetItem();
						raycastHit3.collider.gameObject.GetComponent<TapePlayerScript>().Play();
					}
				}
        }
    }
 
    public void AddCurrency()
    {
        Money = Money + 0.25;
    }
 
}

