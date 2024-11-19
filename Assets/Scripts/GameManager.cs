using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Data;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public TalkManager talkManager;
    [HideInInspector]
    public bool isGameOver = false;

    public int stageIndex = 0;
    [SerializeField]
    GameObject[] Stages;
    [SerializeField]
    Player player;
    [SerializeField]
    Bullet bullet;
    [SerializeField]
    private TextMeshProUGUI coinText;
    [SerializeField]
    private TextMeshProUGUI bulletText;



    public bool isTalkAction;
    [SerializeField]
    private GameObject talkPanel;
    [SerializeField]
    private TextMeshProUGUI talkText;
    private GameObject scanObject;
    public int talkIndex;

    public GameObject menuSet;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void NextStage()
    {
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
        } 
        else
        {
            Debug.Log("마지막 스테이지 입니다.");
        }


    }
    public void BeforeStage()
    {
        if(stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex--;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();
        }


    }

    void PlayerReposition()
    {
        player.transform.position = new Vector2(0,0);
    }
    public void IncreaceCoin()
    {
        player.coin += 1;
        coinText.SetText(player.coin.ToString());

    }
    public void IncreaceBulletDamage()
    {
        bullet.damage += 1;
        bulletText.SetText(bullet.damage.ToString());

    }
    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRoutine();
        }
        
        Invoke("ShowGameOverPanel", 1f);
    }

    public void Action (GameObject scanObj)
    {
        isTalkAction = true;
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        
        talkPanel.SetActive(isTalkAction);
    }

    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);

        if (talkData == null)
        {
            isTalkAction = false;
            talkIndex = 0;
            return;
        }

        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }

        isTalkAction = true;
        talkIndex++;

    }

    void Start()
    {
        GameLoad();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            if (menuSet.activeSelf)
                menuSet.SetActive(false);
            else
                menuSet.SetActive(true);


    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("Coin", player.coin);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }
    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int loadCoin = PlayerPrefs.GetInt("Coin");

        player.transform.position = new Vector2(x,y);
        player.coin = loadCoin;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
