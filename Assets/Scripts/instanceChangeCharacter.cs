using UnityEngine;
using UnityEngine.UI;

public class instanceChangeCharacter : MonoBehaviour
{
    private changeCharacter _changeCharacter;
    [SerializeField]private GameObject characterPlayer;
    [SerializeField]private GameObject[] anotherCharactersButton;

    public static instanceChangeCharacter instance;
    private playerAttack  _playerAttack;

    void Awake()
    {
        _playerAttack = GameObject.FindObjectOfType<playerAttack>();
        if(!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        _changeCharacter = GameObject.FindObjectOfType<changeCharacter>();
    }

    public void OnMouseDown()
    {
        if(_changeCharacter.countPlayerInScene == 0)
        {
            GameObject player = Instantiate(characterPlayer, _changeCharacter.spawnPosition.position, Quaternion.identity);
            player.transform.position += new Vector3(0,0,0);
            _changeCharacter.countPlayerInScene++;
            ControlActiveCharacter.instance.staticPlayer = player;
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
            GameManager.instance.SignTable.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "HERO: " + characterPlayer.name;
        }
        else
        {
            if(ControlActiveCharacter.instance.staticPlayer)
            {
                _changeCharacter.selectionPointPosition = ControlActiveCharacter.instance.staticPlayer.transform.position;
                float posX = ControlActiveCharacter.instance.staticPlayer.transform.position.x;
                float posY = ControlActiveCharacter.instance.staticPlayer.transform.position.y;
                ControlActiveCharacter.instance.staticPlayer.transform.position = new Vector3(posX,posY,0);
                GameManager.instance.SignTable.transform.GetChild(0).GetChild(1).GetComponent<Text>().text = "HERO: " + characterPlayer.name;
                Invoke("timeInstance", 0.5f);
            }
        }
    }

    private void timeInstance()
    {
        Destroy(ControlActiveCharacter.instance.staticPlayer);
        foreach(GameObject go in anotherCharactersButton)
        {
            go.transform.GetChild(0).gameObject.SetActive(false);
            go.transform.GetChild(1).gameObject.SetActive(false);
        }

        GameObject player = Instantiate(characterPlayer, _changeCharacter.selectionPointPosition, Quaternion.identity);
        float posX = ControlActiveCharacter.instance.staticPlayer.transform.position.x;
        float posY = ControlActiveCharacter.instance.staticPlayer.transform.position.y;
        ControlActiveCharacter.instance.staticPlayer.transform.position = new Vector3(posX,posY,0);
        _changeCharacter.countPlayerInScene++;
        _playerAttack._statusAttack = statusAttack.none;
        _playerAttack.gameObject.GetComponent<Image>().fillAmount = 0;
        ControlActiveCharacter.instance.staticPlayer = player;
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }
}
