using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public int keyYellow;
    public int keyBlue;
    public int keyRed;
    public bool isSpecial;
    public string tip;

    private PlayerAttributes _pa;
    private Animator anim;
    private void Start()
    {
        _pa = GameManager.instance.pa;
        anim = GetComponent<Animator>();
    }

    // 门操作操作 减少钥匙
    public void Action()
    {
        // 减少钥匙
        _pa.keyYellow -= keyYellow;
        _pa.keyRed -= keyRed;
        _pa.keyBlue -= keyBlue;
        anim.SetTrigger("Open");
        // 更新UI
        GameManager.instance.ui.UpdateUI();
    }

    public bool CanOpen()
    {
        if (isSpecial && _pa.haveSpecial) return true;
        if (!isSpecial && _pa.keyYellow >= keyYellow && _pa.keyBlue >= keyBlue && _pa.keyRed >= keyRed)
        {
            return true;
        }
        return false;
    }

    public void AfterOpen()
    {
        GameManager.instance.ui.ActiveTips(tip);
        Destroy(gameObject);
    }


}
