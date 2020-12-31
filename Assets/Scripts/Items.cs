using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Start is called before the first frame update
    public int attack;
    public int defend;
    public int health;
    public int exp;
    public int gold;
    public int level;
    public string tip;

    private PlayerAttributes _pa;
    private void Start()
    {
        _pa = GameManager.instance.pa;
    }

    // 物品操作 拾取
    public void Action()
    {
        // 增加攻击
        _pa.attack += attack;
        // 增加经验，金币
        _pa.gold += gold;
        _pa.exp += exp;
        _pa.defend += defend;
        _pa.health += health;
        _pa.level += level;
        GameManager.instance.ui.UpdateUI();
        GameManager.instance.ui.ActiveTips(tip);
        Destroy(gameObject);
    }
}
