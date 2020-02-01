using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowAndMageDefault : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private int cellCount = 0;
    private GameObject[] cells;

    private int countTouch;
	
    //[SerializeField]private GameObject[] TouchEnemys = new GameObject[3];
	
    void Start()
    {
        cells = GameObject.FindGameObjectsWithTag("cell");
        rb = GetComponent<Rigidbody2D>();

        if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
        {
            Invoke("destroyThisObj", 0.3f);
        }

        if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
        {
            Invoke("destroyThisObj", 0.42f);
        }
	}
	
    private void currentCells()
    {
        foreach(GameObject go in cells)
		{
			if(go.GetComponent<cellScene>().isTouch)
			{
				go.GetComponent<cellScene>().isTouch = false;
			}
			else
			Destroy(this.gameObject);
		}
	}

    private void destroyThisObj()
    {
        Destroy(this.gameObject);
    }
	
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
        {
            if (col.tag == "Enemy")
			{
				if(col.GetComponent<enemyAttribute>().Health > 0)
				{
                    countTouch++;
                    if(countTouch == 1)
                    {
                        Destroy(this.gameObject);
					    col.GetComponent<enemyAttribute>().Health -= ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().damage;
					    col.GetComponent<enemyAttribute>().dinamicHealthBar.transform.SetParent(col.GetComponent<enemyAttribute>().worldCanvas.transform);
					    GameManager.instance.audios[6].Play();
                    }    
				}
			}
		}
		
        if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
        {
            if (col.tag == "Enemy")
			{
				if(col.GetComponent<enemyAttribute>().isTriggersEnemy)
				{
					if(col.GetComponent<enemyAttribute>().countISTriggersEnemy >= 3 || col.GetComponent<enemyAttribute>().countISTriggersEnemy == 2)
                    {
                        if(col.GetComponent<enemyAttribute>().Health > 0)
                        {
                            countTouch++;
                            if(countTouch <= 3)
						    {
                                Destroy(this.gameObject);
							    col.GetComponent<enemyAttribute>().Health -= ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().damage;
							    col.GetComponent<enemyAttribute>().dinamicHealthBar.transform.SetParent(col.GetComponent<enemyAttribute>().worldCanvas.transform);
							    GameManager.instance.audios[6].Play();
						    }
                        }
					}
				}
				
                else
                {
                    if(col.GetComponent<enemyAttribute>().Health > 0)
					{
						col.GetComponent<enemyAttribute>().Health -= ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().damage;
						col.GetComponent<enemyAttribute>().dinamicHealthBar.transform.SetParent(col.GetComponent<enemyAttribute>().worldCanvas.transform);
						currentCells();
						GameManager.instance.audios[6].Play();
						Destroy(this.gameObject);
					}
				}
			}
		}
		
        /*if (col.tag == "cell")
        {
            if(!col.GetComponent<cellScene>().isTouch)
            {
                col.GetComponent<cellScene>().isTouch = true;
                cellCount++;
                Debug.Log(cellCount);
			}
		}*/
        
	}
	
    void FixedUpdate()
    {
        rb.AddForce(transform.right * 20, ForceMode2D.Impulse);
	}
}
