using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int attack;
    public int defend;
    public int health;
    public int exp;
    public int gold;
    [HideInInspector]
    public int finalDamage = 0;


    // 玩家属性
    private PlayerAttributes _pa;
    private void Start()
    {
        _pa = GameManager.instance.pa;
    }

    public bool CanBeDefeated()
    {
        int damage = Mathf.Clamp(attack - _pa.defend, 0, 9999);
        int hurt = Mathf.Clamp(_pa.attack - defend, 0, 9999);
        if (hurt == 0) return false;
        int routine = (int)Mathf.Ceil(health / hurt);
        int playerHealth = _pa.health;
        finalDamage = damage * routine;
        if (playerHealth - finalDamage > 0) return true;
        return false;
    }
    // 怪物操作 战斗
    public void Action()
    {
        // 战斗完成后减少血量
        _pa.health -= finalDamage;
        // 增加经验，金币
        _pa.gold += gold;
        _pa.exp += exp;
        GameManager.instance.ui.UpdateUI();
        Die();
    }
    // 怪物死亡
    void Die()
    {
        string getString = "金钱 " + gold + "  经验 " + exp;
        GameManager.instance.ui.ActiveTips(getString);
        Destroy(gameObject);
    }
}
