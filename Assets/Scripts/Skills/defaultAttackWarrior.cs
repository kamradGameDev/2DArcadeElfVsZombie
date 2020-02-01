using UnityEngine;

public class defaultAttackWarrior : MonoBehaviour
{
    private int count;
    void OnTriggerEnter2D(Collider2D col)
    {
        count++;
        if(col.tag == "Enemy")
        {
            if(count == 1)
            {
                Debug.Log("testAttack");
                col.gameObject.GetComponent<enemyAttribute>().Health -= ControlActiveCharacter.instance.staticPlayer.GetComponent<playerAttribute>().damage;
                col.GetComponent<enemyAttribute>().dinamicHealthBar.transform.SetParent(col.GetComponent<enemyAttribute>().worldCanvas.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Enemy")
        {
            count  = 0;
        }
    }
}
