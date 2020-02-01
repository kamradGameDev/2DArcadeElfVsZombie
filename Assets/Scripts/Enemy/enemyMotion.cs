using UnityEngine;

public enum typeEnemy
{
    boy,
    girl,
    frightened,
    punk
}

public class enemyMotion : MonoBehaviour
{
    //private Rigidbody2D rb;
    public bool run = true;
    public bool startWave = false;
	
    System.Random rnd = new System.Random();
    private double isStartRun;
    
    public typeEnemy _typeEnemy;
	
    public float speed;
    private bool isTrack = true;
	
    private bool ISstartRun = false;
	
    private bool isStartSteps = false;
	
    private float sizetX, posX, distX;

    private float directionSteps = 526.12f;
	
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        isStartRun = Random.Range(0.1f,2);
        Invoke("timeStartRun", (float)isStartRun);

        if(_typeEnemy == typeEnemy.girl)
        {
            transform.position += new Vector3(0,0.5f,0);
        }

        if(_typeEnemy == typeEnemy.punk)
        {
            transform.position += new Vector3(0,1.5f,0);
        }

        if(_typeEnemy == typeEnemy.frightened)
        {
            transform.position += new Vector3(0,1f,0);
        }
	}
	
    private void timeStartRun()
    {
        ISstartRun = true;
        //this.GetComponent<BoxCollider2D>().isTrigger = true;
	}
	
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "StepsAndTrigger")
        {
            sizetX = col.GetComponent<BoxCollider2D>().size.x * 20;
            posX = transform.position.x;
            distX = posX - sizetX;
            isStartSteps = true;
            //timeIsTrack();
		}
        if(col.name == "TombStone")
        {
            GameManager.instance.PlayerHealth--;
            Destroy(this.gameObject);
		}
		
        if(col.name == "fallingToTheGrave")
        {
            run = false;
            isTrack = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 1f;
            this.gameObject.GetComponent<Animator>().Play("fallingToTheGrave");
		}
	}

   /*void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Ground")
        {
            //rb.gravityScale = 0;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }*/
	
    private void timeIsTrack()
    {
        isTrack = false;
	}
	
    private void destroyOnTime()
    {
        Destroy(this.gameObject);
	}
	
    void Update()
    {
		if(Time.timeScale == 1)
        {
            if(this.gameObject.GetComponent<enemyAttribute>().Health > 0)
			{
				if(!startWave)
				{
					if(ISstartRun)
					{
						if(this.GetComponent<enemyMotion>().run)
						{
							//rb.velocity = new Vector2(-speed,0);
							Vector2 pos = transform.position;
                            pos.x -= Mathf.Sign(directionSteps) * speed * Time.deltaTime;
                            transform.position = pos;
							
							this.gameObject.GetComponent<Animator>().Play("Run");
						}
					}
					
					if(isStartSteps)
					{
						run = false;
						ISstartRun = false;
						if(transform.position.x > distX)
						{
                            Vector2 pos = transform.position;
                            pos.x -= Mathf.Sign(directionSteps) * speed * Time.deltaTime;
                            transform.position = pos;
							//rb.velocity = new Vector2(-speed,0);
							this.gameObject.GetComponent<Animator>().Play("Run");
						}
						
						else
						{
							//rb.velocity = new Vector2(0,-speed);
                            Vector2 pos = transform.position;
                            pos.y -= Mathf.Sign(directionSteps) * speed * Time.deltaTime;
                            transform.position = pos;
							//this.gameObject.GetComponent<Animator>().Play("Run");
						}
					}
				}
			}
		}
	}
}
