using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public Text playerLevel;
    public Text gold;
    public Text exp;
    public Text health;
    public Text attack;
    public Text defend;
    public Text level;
    public Text yellowKeyNum;
    public Text blueKeyNum;
    public Text redKeyNum;

    public Image book;
    public Image fly;

    // Tips
    public GameObject tips;
    public Text infoText;
    public float tipTime = 0.3f;
    public float timer = 0.0f;
    private PlayerAttributes _pa;
    private void Start()
    {
        _pa = GameManager.instance.pa;
    }

    // fly
    public List<GameObject> floors = new List<GameObject>();
    public GameObject flyWindow;

    // book
    public GameObject bookWindow;
    public GameObject enemyInfoParent;
    public GameObject enemyInfoPrefab;
    public GameObject NoneEnemy;
    public GameObject Title;
    // Menu
    public GameObject menuWindow;
    // audio
    public Image audioButton;
    public Sprite normalAudioSprite;
    public Sprite muteAudioSprite;
    public void initUI()
    {
        // 初始化文本
        playerLevel.text = "1";
        gold.text = "0";
        exp.text = "0";
        health.text = "1000";
        attack.text = "10";
        defend.text = "10";
        level.text = "第 0 层";
        yellowKeyNum.text = "0";
        blueKeyNum.text = "0";
        redKeyNum.text = "0";
        // 初始化图片
        book.color = Color.gray;
        fly.color = Color.gray;
        // 初始化FlyUI
        flyWindow.SetActive(false);
        for (int i =0;i<floors.Count;i++)
        {
            floors[i].SetActive(false);
        }
        floors[0].SetActive(true);
        // 初始化BookUI
        CloseBookWindow();
        // 初始化Mute
        audioButton.sprite = GameManager.instance.isMute ? muteAudioSprite : normalAudioSprite;
        menuWindow.SetActive(false);
    }

    public void UpdateUI()
    {
        // 更新文本
        playerLevel.text = _pa.level + "";
        gold.text = _pa.gold + "";
        exp.text = _pa.exp + "";
        health.text = _pa.health + "";
        attack.text = _pa.attack + "";
        defend.text = _pa.defend + "";
        level.text = "第 " + GameManager.instance.level + " 层";
        yellowKeyNum.text = _pa.keyYellow + "";
        blueKeyNum.text = _pa.keyBlue + "";
        redKeyNum.text = _pa.keyRed + "";
        // 更新图片
        if (_pa.book)
        {
            book.color = Color.white;
        } else
        {
            book.color = Color.gray;
        }
        if (_pa.flyShoe)
        {
            fly.color = Color.white;
        }
        else
        {
            fly.color = Color.gray;
        }
    }

    public void ActiveTips(string tip)
    {
        if (tip != "")
        {
            if (timer == 0)
            {
                tips.SetActive(true);
            }
            timer = tipTime;
            infoText.text = tip;
        }
    }

    public void DeactiveTips()
    {
        tips.SetActive(false);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            DeactiveTips();
            timer = 0.0f;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public void ActiveFlyFloor(int floor)
    {
        floors[floor].SetActive(true);
    }

    public void CloseFlyWindow()
    {
        flyWindow.SetActive(false);
    }

    public void OpenFlyWindow()
    {
        if (GameManager.instance.pa.flyShoe)
        {
            flyWindow.SetActive(true);
        }
    }

    public void CloseBookWindow()
    {
        // 清空info GameObjects
        foreach(Transform child in enemyInfoParent.transform)
        {
            Destroy(child.gameObject);
        }
        Title.SetActive(false);
        NoneEnemy.SetActive(false);
        bookWindow.SetActive(false);
    }

    public void OpenBookWindow()
    {
        if (GameManager.instance.pa.book)
        {
            // 获取当前楼层
            int level = GameManager.instance.level;
            // 获取当前楼层的怪物信息
            Transform levelGO = GameObject.Find("Map").GetComponent<Transform>().Find("Floor" + level);
            Debug.Log(levelGO);
            Transform enemies = levelGO.Find("Enemies");
            Debug.Log(enemies);
            // 生成怪物信息UI
            int offset = -135;
            Dictionary<string, int> enemiesCount = new Dictionary<string, int>();
            foreach (Transform child in enemies.transform)
            {
                Debug.Log(123);
                string name = child.name.Split(' ')[0];
                if (!enemiesCount.ContainsKey(name))
                {
                    enemiesCount[name] = 1;
                    Enemy info = child.GetComponent<Enemy>();
                    GameObject enemyInfo = Instantiate(enemyInfoPrefab, enemyInfoParent.transform);
                    // 设置offset
                    enemyInfo.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0, offset, 0);
                    // 修改图片
                    enemyInfo.transform.Find("Enemy").GetComponent<Image>().sprite = child.GetComponent<SpriteRenderer>().sprite;
                    // 更新文本
                    enemyInfo.transform.Find("Health").GetComponent<Text>().text = info.health + "";
                    enemyInfo.transform.Find("Attack").GetComponent<Text>().text = info.attack + "";
                    enemyInfo.transform.Find("Defend").GetComponent<Text>().text = info.defend + "";
                    enemyInfo.transform.Find("Gold").GetComponent<Text>().text = info.gold + "";
                    enemyInfo.transform.Find("Exp").GetComponent<Text>().text = info.exp + "";
                    if (info.CanBeDefeated())
                    {
                        enemyInfo.transform.Find("Damage").GetComponent<Text>().text = info.finalDamage + "";
                    } else
                    {
                        enemyInfo.transform.Find("Damage").GetComponent<Text>().text = "打不过";
                    }
                    // 更新偏移量
                    offset -= 75;
                }
            }
            // 如果没有怪物 显示没有的窗口
            if (enemiesCount.Count == 0)
            {
                NoneEnemy.SetActive(true);
            } else
            {
                Title.SetActive(true);
            }
            // 打开窗口
            bookWindow.SetActive(true);
        }
    }

    public void ChangeAudioState()
    {
        GameManager.instance.isMute = !GameManager.instance.isMute;
        GameManager.instance.ac.audioSource.mute = GameManager.instance.isMute;
        // UI 替换
        audioButton.sprite = GameManager.instance.isMute ? muteAudioSprite : normalAudioSprite;
    }

    public void  ReturnGame()
    {
        menuWindow.SetActive(false);
    }

    public void OpenMenuWindow()
    {
        menuWindow.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }
}
