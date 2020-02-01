using UnityEngine;

public class IDEnemHealthBar : MonoBehaviour
{
    [SerializeField]private GameObject[] enemysHealthBar;
    [SerializeField]private GameObject[] enemys;
    public float ID;
    void Start()
    {
        //changeEnemysHealthBarInScene();
    }

    private void changeEnemysHealthBarInScene()
    {
        enemysHealthBar = GameObject.FindGameObjectsWithTag("enemtHealthBar");
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        if(enemysHealthBar.Length == 1)
        {
            ID = Random.Range(0, 50000000);
        }
        else
        {
            foreach(GameObject go in enemysHealthBar)
            {
                if(go)
                {
                    float tempID = Random.Range(0, 50000000);
                    if(go.GetComponent<IDEnemHealthBar>().ID != ID)
                    {
                        ID = tempID;
                    }
                }
            }
        }
    }
}
