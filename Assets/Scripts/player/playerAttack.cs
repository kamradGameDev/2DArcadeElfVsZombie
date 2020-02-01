using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum statusAttack
{
    isAttackOne,
    timerChangeMoreAttack,
	none
}
public class playerAttack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject arrowObj, magicDefObj;
    public bool isPointed = false;
	
    private float timerChange; 
	
    public statusAttack _statusAttack;
	
    void Update()
    {
		if(ControlActiveCharacter.instance.staticPlayer)
		{
			if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack)
			{
				if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
				{
					this.gameObject.GetComponent<Image>().fillAmount += 1 / ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().timeAttack * Time.deltaTime;
				}
				
				if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
				{
					this.gameObject.GetComponent<Image>().fillAmount += 1 / ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().timeAttack * Time.deltaTime;
				}
				
				if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Warrior)
				{
					this.gameObject.GetComponent<Image>().fillAmount += 1 / ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().timeAttack * Time.deltaTime;
				}
			}
			if(this.gameObject.GetComponent<Image>().fillAmount == 1)
			{
				ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack = false;
				this.gameObject.GetComponent<Image>().fillAmount = 0;
			}
		}
		
		if(Time.timeScale == 1)
		{
			if(_statusAttack == statusAttack.timerChangeMoreAttack)
			{
				if(ControlActiveCharacter.instance.staticPlayer)
				{
					timerChange += Time.deltaTime;
					if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
					{
						if(timerChange >= 1f)
						{
							timerChange = 0.5f;
							timer();
							isPointed = true;
						}
					}
					
					if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Warrior)
					{
						if(timerChange >= 1f)
						{
							timerChange = 0.5f;
							timer();
							isPointed = true;
						}
					}
					
					if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
					{
						if(timerChange >= 1f)
						{
							timerChange = 0.3f;
							timer();
							isPointed = true;
						}
					}
				}
			}
		}
	}
	
    public void Attack()
    {
	    if(Time.timeScale == 1)
		{
			if(ControlActiveCharacter.instance.staticPlayer)
			{
				_statusAttack = statusAttack.isAttackOne;
				if(this.gameObject.GetComponent<Image>().fillAmount == 0)
				{
					if(_statusAttack == statusAttack.isAttackOne)
					{
						ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack = true;
						if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer || ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
						{
							if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
							{
								ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().SetTrigger("Attack");
								GameManager.instance.audios[0].Play();
								GameObject obj = Instantiate(arrowObj, ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).position, Quaternion.identity);
								if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().flip)
								{
									obj.transform.rotation = Quaternion.Euler(0,180,0);
								}
								else
								{
									obj.transform.rotation = Quaternion.Euler(0,0,0);
								}
							}
							if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
							{
								ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().SetTrigger("Attack");
								Invoke("timeAtatckInstanceSkillMage",0.4f);
							}
						}
						
						if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Warrior)
						{
							GameManager.instance.audios[1].Play();
							ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().SetTrigger("Attack");
							ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).gameObject.SetActive(true);
							Invoke("timeAttackWarrior", 1.0f);
						}
					}
				}  
			}
		}
	}

	private void timeAtatckInstanceSkillMage()
	{
		GameManager.instance.audios[2].Play();
		GameObject obj = Instantiate(magicDefObj, ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).position, Quaternion.identity);
		if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().flip)
		{
			obj.transform.rotation = Quaternion.Euler(0,180,0);
		}
		else
		{
			obj.transform.rotation = Quaternion.Euler(0,0,0);
		}
	}
	
	private void timeAttackWarrior()
	{
		ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).gameObject.SetActive(false);
	}
	
    private void timer()
    {
        if(Time.timeScale == 1)
        {
            if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Archer)
			{
				if(isPointed)
				{
					GameManager.instance.audios[0].Play();
					ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack = true;
					GameObject obj = Instantiate(arrowObj, ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).position, Quaternion.identity);
					if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().flip)
					{
						obj.transform.rotation = Quaternion.Euler(0,180,0);
					}
					else
					{
						obj.transform.rotation = Quaternion.Euler(0,0,0);
					}
					ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().Play("Attack");
				} 
			}
			
			if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Mage)
			{
				if(isPointed)
				{
					GameManager.instance.audios[2].Play();
					ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack = true;
					GameObject obj = Instantiate(magicDefObj, ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).position, Quaternion.identity);
					if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().flip)
					{
						obj.transform.rotation = Quaternion.Euler(0,180,0);
					}
					else
					{
						obj.transform.rotation = Quaternion.Euler(0,0,0);
					}
					ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().Play("Attack");
				} 
			}
			
			if(ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>()._classCharacter == classCharacter.Warrior)
			{
				if(isPointed)
				{
					ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().attack = true;
					ControlActiveCharacter.instance.staticPlayer.GetComponent<Animator>().SetTrigger("Attack");
					ControlActiveCharacter.instance.staticPlayer.transform.GetChild(0).gameObject.SetActive(true);
					Invoke("timeAttackWarrior", 0.5f);
					GameManager.instance.audios[1].Play();
				}   
			}
		}
	}
	
    public void OnPointerUp(PointerEventData eventData)
    {
        isPointed = false;
	}
	
	public void OnPointerDown(PointerEventData eventData)
	{
        if(Time.timeScale == 1)
		{
			_statusAttack = statusAttack.timerChangeMoreAttack;
		}
	}
}
