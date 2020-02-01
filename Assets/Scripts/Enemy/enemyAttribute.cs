using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyAttribute : MonoBehaviour
{
    public float Health, maxHealth;
    [SerializeField]private GameObject healthBar;
    public GameObject worldCanvas;
    public GameObject dinamicHealthBar;
    public float _randomChangeTypeMin;
    public float _randomChangeTypeMax;
    private bool isPlaySoundDied;
    public bool isTriggersEnemy = false;
    public int countISTriggersEnemy = 1;

    private bool isDiedStartInvoke = false;

    public bool massAttack = true;
    void Start()
    {
        worldCanvas = GameObject.Find("WorldCanvas");
        instanceHealthBar();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            if(!isTriggersEnemy)
            {
                if(countISTriggersEnemy <= 3)
                {
                    countISTriggersEnemy++;
                    isTriggersEnemy = true;

                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            if(isTriggersEnemy)
            {
                if(countISTriggersEnemy > 1)
                {
                    countISTriggersEnemy--;
                    isTriggersEnemy = false;
                }
            }
        }
    }

    void Update()
    {
        if(Health > maxHealth)
        {
            Health = maxHealth;
        }
        if(Health <= 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = 10.0f;
            if(!isPlaySoundDied)
            {
                GameManager.instance.audios[3].Play();
                isPlaySoundDied = true;
            }
            GetComponent<Animator>().Play("Dead");
            dinamicHealthBar.transform.SetParent(this.transform);
            isDiedStartInvoke = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        }
        if(dinamicHealthBar)
        {
            if(dinamicHealthBar.transform.position != this.transform.GetChild(0).position)
            {
                dinamicHealthBar.transform.position = this.transform.GetChild(0).position;
            }
            dinamicHealthBar.transform.GetChild(0).GetComponent<Image>().fillAmount = Health / maxHealth;
                
        }
        if(isDiedStartInvoke)
        {
            isDiedStartInvoke = true;
            Invoke("Died", 1.0f);
        }
    }

    private void Died()
    {
        Destroy(this.gameObject);
    }

    public void instanceHealthBar()
    {
        GameObject obj = Instantiate(healthBar, this.transform.GetChild(0).position, Quaternion.identity);
        obj.transform.SetParent(this.transform);
        dinamicHealthBar = obj;
    }
}
