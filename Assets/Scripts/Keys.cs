using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public int keyYellow;
    public int keyBlue;
    public int keyRed;
    public string tip;

    private PlayerAttributes _pa;
    private void Start()
    {
        _pa = GameManager.instance.pa;
    }

    // 物品操作 拾取
    public void Action()
    {
        // 增加钥匙
        _pa.keyYellow += keyYellow;
        _pa.keyRed += keyRed;
        _pa.keyBlue += keyBlue;
        GameManager.instance.ui.UpdateUI();
        GameManager.instance.ui.ActiveTips(tip);
        Destroy(gameObject);
    }
}
