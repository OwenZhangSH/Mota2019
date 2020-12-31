using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Fungus;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // GameManager单例
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    // 游戏相关属性
    public int level = 0;
    public int maxLevel = 0;
    // 人物属性
    public PlayerAttributes pa;
    public GameObject player;
    // 人物从下层到上层的位置 index = level
    public List<Vector3> upPositions = new List<Vector3>();
    // 人物从下层到上层的位置 index = level
    public List<Vector3> downPositions = new List<Vector3>();

    // UI控制
    public UIController ui;


    // 对话控制
    public DialogManager dm;

    // 声音控制
    public AudioController ac;
    public bool isMute = false;
    // Start is called before the first frame update
    void Start()
    {
        ui.initUI();
    }

    public void ChangeLevel(StairsType type, int level)
    {
        if (level > maxLevel)
        {
            maxLevel = level;
            ui.ActiveFlyFloor(level);
        }

        if (level == 21)
        {
            ac.MeetBoss();
        } else
        {
            ac.NormalLevel();
        }

        this.level = level;
        // 改变摄像机位置
        Camera.main.transform.position = new Vector3(level * 12.0f, 0.0f, -10.0f);
        // 改变玩家位置
        if (type == StairsType.Up)
        {
            player.transform.position = upPositions[level];
        } else
        {
            player.transform.position = downPositions[level];
        }
        ui.ActiveTips("当前在第" + level + "层");
    }

    public void FlyToLevel(int level)
    {
        ac.PlayClip(ClipType.Fly);
        ChangeLevel(StairsType.Up, level);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }
}
