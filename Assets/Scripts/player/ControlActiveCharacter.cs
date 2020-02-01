using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CnControls;

public class ControlActiveCharacter : MonoBehaviour
{
    private Vector3 position;
    public GameObject staticPlayer;
    public static ControlActiveCharacter instance;
	
	public bool statusGround = false;
	
    void Awake()
    {
        staticPlayer = GameObject.FindGameObjectWithTag("Player");
        if(!instance)
		instance = this;
	}
	
    void Update()
    {
		
		if(staticPlayer)
		{
			if(staticPlayer.GetComponent<playerAttribute>().status)
			{
				if(position.y > 0 || position.y < 0)
				{
					staticPlayer.GetComponent<Animator>().SetTrigger("Run");
				}
				if(position.x < 0)
				{
					staticPlayer.transform.rotation = Quaternion.Euler(0,180,0);
					staticPlayer.GetComponent<playerAttribute>().flip = true;
					staticPlayer.GetComponent<Animator>().Play("Run");
				}
				else if(position.x > 0)
				{
					staticPlayer.transform.rotation = Quaternion.Euler(0,0,0);
					staticPlayer.GetComponent<playerAttribute>().flip = false;
					staticPlayer.GetComponent<Animator>().Play("Run");
				}
				else
				{

					if(!staticPlayer.GetComponent<playerAttribute>().attack)
					staticPlayer.GetComponent<Animator>().Play("Idle");
				}
				//staticPlayer.GetComponent<playerAttribute>().speed = staticPlayer.GetComponent<playerAttribute>().normalSpeed;
				position = new Vector3 (CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"), 0f);
				//staticPlayer.GetComponent<Rigidbody2D>().AddForce(position.normalized * staticPlayer.GetComponent<playerAttribute>().speed);
				staticPlayer.transform.position += position * Time.deltaTime * staticPlayer.GetComponent<playerAttribute>().speed;
			}
		}
	}
}
