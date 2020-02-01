using UnityEngine;
public class changeGroundPlayer : MonoBehaviour
{	
    private bool isSpeed = false;
    void Start()
    {
        Invoke("TimeChangeCollider", 0.05f);
    }

    private void TimeChangeCollider()
    {
        if(ControlActiveCharacter.instance.statusGround)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 20f;
            this.GetComponent<playerAttribute>().speed = this.GetComponent<playerAttribute>().normalSpeed;
        }

        if(!ControlActiveCharacter.instance.statusGround)
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }
	void OnTriggerStay2D(Collider2D col)
	{
		if(col.gameObject.tag == "Steps" || col.gameObject.tag == "StepsAndTrigger")
		{
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            if(!isSpeed)
            {
                this.GetComponent<playerAttribute>().speed *= 1.3f;
                isSpeed = true;
            }
            ControlActiveCharacter.instance.statusGround = false;
			this.GetComponent<Rigidbody2D>().gravityScale = 0f;
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if(isSpeed)
        {
            this.GetComponent<playerAttribute>().speed /= 1.3f;
            isSpeed = false;
        }
		this.GetComponent<Rigidbody2D>().gravityScale = 20f;
        ControlActiveCharacter.instance.statusGround = true;
	}
}

