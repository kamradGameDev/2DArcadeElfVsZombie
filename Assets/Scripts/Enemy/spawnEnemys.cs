using UnityEngine;
using UnityEngine.UI;

public class spawnEnemys : MonoBehaviour
{
    [SerializeField]private Transform[] points;
    [SerializeField]private GameObject[] enemys;
	
    //[SerializeField]private GameObject[] enemysInScene;
    [SerializeField]private GameObject enemysInSceneNull;
    public float minRandom = 0.1f;
    public float maxRandom = 1;
    private bool IsStartScene = false;
    private bool generateRandomZombieAfterSevenWave = false;
    private bool speedPlusAfteSevenWave = false;
    private int tempWare;
    private System.Random randomPos = new System.Random();
    //private float randomPos;
    //private float randomCount;
   private System.Random randomCount = new System.Random();
    //private float _randomChangeType;
    private System.Random _randomChangeType = new System.Random();
    //после 7 волны переменные шанса количества зомби, значение менять только если понадобится изменить баланс
    private int randomCount1Min = 1, randomCount1Max = 40, randomCount2Min = 41, randomCount2Max = 80, randomCount3Min = 81, randomCount3Max = 90, randomCount4Min = 91, randomCount4Max = 100;
	
    private float TimerInWaveStart;
    [SerializeField] private float TimerInWaveProcess, TimerVave;
    private int countWaveAd = 1;
    void Start()
    {
        TimerInWaveStart = TimerInWaveProcess;
        Invoke("currentWave", 0.2f);
        InvokeRepeating("timerInWave", TimerInWaveStart, TimerInWaveStart);
        InvokeRepeating("timerWave", TimerVave, TimerVave);
	}
	
    void Update()
    {
        //if(!IsStartScene)
       // {
            //generateRandomZombieAfterSevenWave = true;
            //if(!enemysInSceneNull)
           // {
                //currentWave();
			//}
           // currentWave();
            //IsStartScene = true;
		//}

        GameManager.instance.SignTable.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "WAVE: " + GameManager.instance.wave.ToString();
        GameManager.instance.SignTable.transform.GetChild(0).GetChild(2).GetComponent<Text>().text = "BEST: " + GameManager.instance.topWave.ToString() + " WAVE";
	}
	
    private void timerWave()
    {
        /*enemysInScene = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject go in enemysInScene)
        {
            if(go)
            {
                Destroy(go);
			}
		}*/
        if(GameManager.instance.wave > 1)
        {
            pauseInGame.instance.startPauseTime();
        }
        GameManager.instance.wave++;
        speedPlusAfteSevenWave = false;
        generateRandomZombieAfterSevenWave = false;
        currentWave();

        IsStartScene = false;
        GameManager.instance.startWaveMenu();
        TimerInWaveProcess = TimerInWaveStart;
        countWaveAd++;
        if(countWaveAd == 20)
        {
            AdManager.instance.startAd();   
            countWaveAd = 0;
        }
	}
	
    private void timerInWave()
    {
        //IsStartScene = false;
        //enemysInSceneNull = null;
        currentWave();
	}

    private void timePlayMusic()
    {
        GameManager.instance.audios[4].Play();
    }
	
    private void currentWave()
    {
        /*for(int i = 0; i < points.Length; i++)
        {
            float posX, posY, posZ;
            posX = points[i].transform.position.x - Random.Range(0f, 1f);
            if(points[i].transform.position.x == posX)
            {
                posX = points[i].transform.position.x - 0.2f;
                posY = points[i].transform.position.y;
                posZ = points[i].transform.position.z;
                points[i].transform.position = new Vector3(posX,posY,posZ);;
            }
            else
            {
                posY = points[i].transform.position.y;
                posZ = points[i].transform.position.z;
                points[i].transform.position = new Vector3(posX,posY,posZ);;
            }
        }*/
        //Debug.Log("tempWave_0" + tempWare);
        tempWare = PlayerPrefs.GetInt("TopWave");
        //Debug.Log("tempWave_1" + tempWare);
        if(GameManager.instance.wave > tempWare)
        {
            GameManager.instance.topWave = GameManager.instance.wave;
            if(GameManager.instance.wave > 1)
            {
                PlayerPrefs.SetInt("TopWave", GameManager.instance.topWave--);
            }
            Debug.Log("Current: " + PlayerPrefs.GetInt("TopWave"));
            //Debug.Log("savedTop" + PlayerPrefs.GetInt("TopWave"));
		}
        if(GameManager.instance.wave > 1)
        {
            //Invoke("timePlayMusic", 1.0f);
            GameManager.instance.audios[4].Play();
        }
        switch (GameManager.instance.wave)
        {
            case 1:
			wave_1();
            break;
            case 2:
			wave_2();
            break;
            case 3:
			wave_3();
            break;
            case 4:
			wave_4();
            break;
            case 5:
			wave_5();
            break;
            case 6:
			wave_6();
            break;
            case 7:
			wave_7();
            break;
		}
        if(GameManager.instance.wave > 7)
        {
            afterSevenWave();
            if(!speedPlusAfteSevenWave)
			{
				float procent_0 = enemys[0].GetComponent<enemyMotion>().speed * 0.5f / 100;
				enemys[0].GetComponent<enemyMotion>().speed += procent_0;
				
				float procent_1 = enemys[1].GetComponent<enemyMotion>().speed * 0.5f / 100;
				enemys[1].GetComponent<enemyMotion>().speed += procent_1;
				
				float procent_2 = enemys[2].GetComponent<enemyMotion>().speed * 0.5f / 100;
				enemys[2].GetComponent<enemyMotion>().speed += procent_2;
				
				float procent_3 = enemys[3].GetComponent<enemyMotion>().speed * 0.5f / 100;
				enemys[3].GetComponent<enemyMotion>().speed += procent_0;
				speedPlusAfteSevenWave = true;
			}
		}
	}
	
    private void wave_1()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 100);
        int _randomCount = randomCount.Next(0,100);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 50)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 50 & _randomPos <= 100)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
				}
			}  
		}
	}
	
    private void wave_2()
    {  
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 90);
        int _randomCount = Random.Range(1,100);
        //Debug.Log("_randomChangeType " + _randomChangeType);
        //Debug.Log("randomPos " + randomPos);
       // Debug.Log("randomCount " + randomCount);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 90f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 91f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100f;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 30)
                {
					if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 30 & _randomPos <= 60)
                {
					if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
				}
				
                if(_randomPos > 60 & _randomPos <= 90)
                {
				    if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
					    GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;   
					}
                }
				
			}  
		}
	}
	
    private void wave_3()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 90);
        int _randomCount = randomCount.Next(1,100);
       // Debug.Log("_randomChangeType " + _randomChangeType);
        //Debug.Log("randomPos " + randomPos);
       // Debug.Log("randomCount " + randomCount);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 60f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 71f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100f;
			}
             if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 30)
                {
					if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 30 & _randomPos <= 60)
                {
					if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
				}
				
                if(_randomPos > 60 & _randomPos <= 90)
                {
				    if(_randomCount >= 1 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
					    GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;   
					}
                }
				
			}  
		}
	}
	
    private void wave_4()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 90);
        int _randomCount = randomCount.Next(1,100);
        //Debug.Log("_randomChangeType " + _randomChangeType);
        //Debug.Log("randomPos " + randomPos);
       // Debug.Log("randomCount " + randomCount);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 60f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 61f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 90f;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 0;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 0;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 91;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 30)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 30 & _randomPos <= 60)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
				}
				
                if(_randomPos > 60 & _randomPos <= 90)
                {
				    if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
					    GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;   
					}
                }
			}  
		}
	}
	
    private void wave_5()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 90);
        int _randomCount = randomCount.Next(1,100);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 50f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 51f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 80f;
			}
             if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 81;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 90;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 91;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 30)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
                        Debug.Log(enemy);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 30 & _randomPos <= 60)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        //IsStartScene = true;
					}
				}
				
                if(_randomPos > 60 & _randomPos <= 90)
                {
				    if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 100)
					{
					    GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        //IsStartScene = true;   
					}
                }
			}  
		}
	}
	
    private void wave_6()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 100);
        int  _randomCount = randomCount.Next(1,100);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 40f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 41f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 70f;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 71;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 85;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 86;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos < 25)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}

                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}

                if(_randomPos >= 25 & _randomPos < 50)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos >= 50 & _randomPos <= 75)
                {
					if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos > 75 & _randomPos <= 100)
                {
				    if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                }
			}  
		}
	}
	
    private void wave_7()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 100);
        int _randomCount = randomCount.Next(1,100);
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 40f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 41f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 70f;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 71;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 85;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 86;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos <= 25)
                {
					if(_randomCount >= 0 & _randomCount <= 40)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 40 & _randomCount <= 80)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 80 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}

                if(_randomPos > 25 & _randomPos <= 50)
                {
					if(_randomCount >= 0 & _randomCount <= 40)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 40 & _randomCount <= 80)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 80 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos > 50 & _randomPos <= 75)
                {
					if(_randomCount >= 0 & _randomCount <= 40)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 40 & _randomCount <= 80)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 80 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos > 75 & _randomPos <= 100)
                {
				    if(_randomCount >= 0 & _randomCount <= 50)
					{
						GameObject enemy = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > 50 & _randomCount <= 90)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > 90 & _randomCount <= 100)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                }
			} 
		}
	}
	
    private void afterSevenWave()
    {
        int _randomChangeType = randomPos.Next(1, 100);
        int _randomPos = randomPos.Next(0, 100);
        int _randomCount = randomCount.Next(1,100);

        if(!generateRandomZombieAfterSevenWave)
        {
            if(randomCount1Max > 1)
            {
                randomCount1Max -= 1;
            }

            if(randomCount2Min > 1)
            {
                randomCount2Min -= 1;
            }

            if(randomCount2Max > 0)
            {
                randomCount2Max -= 2;
            }
            
            if(randomCount3Min >= 3)
            {
                randomCount3Min -= 2;
            }

            if(randomCount3Max > 50)
            {
                randomCount3Max -= 1;
            }
            
            if(randomCount4Min > 51)
            {
                randomCount4Min -= 1;
            }
            
            if(randomCount1Max <= 1)
            {
                randomCount1Min = 0;
            }

            if(randomCount2Max <= 1)
            {
                randomCount2Min = 0;
            }
            

            if(enemys[0].GetComponent<enemyAttribute>()._randomChangeTypeMin >= 0 || enemys[0].GetComponent<enemyAttribute>()._randomChangeTypeMax >= 0)
            {
                enemys[0].GetComponent<enemyAttribute>()._randomChangeTypeMax--;
                if(enemys[0].GetComponent<enemyAttribute>()._randomChangeTypeMax == 0)
                {
                    enemys[0].GetComponent<enemyAttribute>()._randomChangeTypeMin--;   
                }         
            }

            if(enemys[1].GetComponent<enemyAttribute>()._randomChangeTypeMax < 50)
            {
                enemys[1].GetComponent<enemyAttribute>()._randomChangeTypeMax++;
            }

            if(enemys[2].GetComponent<enemyAttribute>()._randomChangeTypeMin > 0 || enemys[2].GetComponent<enemyAttribute>()._randomChangeTypeMax > 0)
            {
                enemys[2].GetComponent<enemyAttribute>()._randomChangeTypeMax--;
                if(enemys[2].GetComponent<enemyAttribute>()._randomChangeTypeMax == 0)
                {
                    enemys[2].GetComponent<enemyAttribute>()._randomChangeTypeMin--;
                }
            }

            if(enemys[1].GetComponent<enemyAttribute>()._randomChangeTypeMax < 50)
            {
                enemys[1].GetComponent<enemyAttribute>()._randomChangeTypeMax++;
            }
		}
		
        foreach(GameObject go in enemys)
		{
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.boy)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 1f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 40f;
			}
			
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.girl)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 41f;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 70f;
			}
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.frightened)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 71;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 85;
            }
            if(go.GetComponent<enemyMotion>()._typeEnemy == typeEnemy.punk)
            {
                go.GetComponent<enemyAttribute>()._randomChangeTypeMin = 86;
                go.GetComponent<enemyAttribute>()._randomChangeTypeMax = 100;
            }
			if(_randomChangeType >= go.GetComponent<enemyAttribute>()._randomChangeTypeMin & _randomChangeType <= go.GetComponent<enemyAttribute>()._randomChangeTypeMax)
			{
				if(_randomPos >= 0 & _randomPos <= 25)
                {
					if(_randomCount > randomCount1Min & _randomCount <= randomCount1Max)
					{
						GameObject enemy = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > randomCount2Min & _randomCount <= randomCount2Max)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount3Min & _randomCount <= randomCount3Max)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount4Min & _randomCount <= randomCount4Max)
					{
						GameObject enemy_0 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[0].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[0].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}

                if(_randomPos > 25 & _randomPos <= 50)
                {
					if(_randomCount > randomCount1Min & _randomCount <= randomCount1Max)
					{
						GameObject enemy = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > randomCount2Min & _randomCount <= randomCount2Max)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount3Min & _randomCount <= randomCount3Max)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount4Min & _randomCount <= randomCount4Max)
					{
						GameObject enemy_0 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[1].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[1].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos > 50 & _randomPos <= 75)
                {
					if(_randomCount > randomCount1Min & _randomCount <= randomCount1Max)
					{
						GameObject enemy = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > randomCount2Min & _randomCount <= randomCount2Max)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount3Min & _randomCount <= randomCount3Max)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount4Min & _randomCount <= randomCount4Max)
					{
						GameObject enemy_0 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[2].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[2].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
				}
				
                if(_randomPos > 75 & _randomPos <= 100)
                {
				    if(_randomCount > randomCount1Min & _randomCount <= randomCount1Max)
					{
						GameObject enemy = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy");
                        //IsStartScene = true;
					}
					if(_randomCount > randomCount2Min & _randomCount <= randomCount2Max)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                    if(_randomCount > randomCount3Min & _randomCount <= randomCount3Max)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}

                    if(_randomCount > randomCount3Min & _randomCount <= randomCount3Max)
					{
						GameObject enemy_0 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_1 = Instantiate(go, points[3].position, Quaternion.identity);
                        GameObject enemy_2 = Instantiate(go, points[3].position, Quaternion.identity);
                         GameObject enemy_3 = Instantiate(go, points[3].position, Quaternion.identity);
						//enemysInSceneNull = GameObject.FindGameObjectWithTag("Enemy"); 
                        //IsStartScene = true; 
					}
                }
			}   
		}
	}
}
