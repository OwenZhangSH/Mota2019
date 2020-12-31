using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    // 移动
    public float moveSpeed = 0.3f;
    public bool isMoving = false;
    private int _offset = 1;
    public bool isTalking = false;
    enum moveDir
    {
        Left,
        Right,
        Top,
        Bottom
    }
    // 移动动画
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("MoveState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        // 判断当前是否可以移动
        if (!isTalking && !isMoving && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            Move();
        }
    }

    // 移动管理
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        // 判断是否可以向右移动
        if (x > 0 && CanMove(moveDir.Right))
        {
            isMoving = true;
            anim.SetInteger("MoveState", 2);
            transform.DOMoveX(transform.position.x + _offset, moveSpeed)
                .SetEase(Ease.Linear)
                .OnComplete(MoveEnd);
            return;
        }
        // 判断是否可以向左移动
        else if (x < 0 && CanMove(moveDir.Left))
        {
            isMoving = true;
            anim.SetInteger("MoveState", 1);
            transform.DOMoveX(transform.position.x - _offset, moveSpeed).
                SetEase(Ease.Linear).
                OnComplete(MoveEnd);
            return;
        }
        float y = Input.GetAxisRaw("Vertical");
        // 判断是否可以向前移动
        if (y > 0 && CanMove(moveDir.Top))
        {
            isMoving = true;
            anim.SetInteger("MoveState", 3);
            transform.DOMoveY(transform.position.y + _offset, moveSpeed)
                .SetEase(Ease.Linear)
                .OnComplete(MoveEnd);
            return;
        }
        // 判断是否可以向后移动
        else if (y < 0 && CanMove(moveDir.Bottom))
        {
            isMoving = true;
            anim.SetInteger("MoveState", 4);
            transform.DOMoveY(transform.position.y - _offset, moveSpeed)
                .SetEase(Ease.Linear)
                .OnComplete(MoveEnd);
            return;
        }
    }

    // 移动完成后回调函数
    void MoveEnd()
    {
        isMoving = false;
        anim.SetInteger("MoveState", 0);
    }

    // 能否移动判断
    bool CanMove(moveDir dir)
    {
        _offset = 1;
        RaycastHit2D hit;
        switch (dir)
        {
            case moveDir.Left:
                // 如果距离左边界小于0.05，认为位于左边界不可向左移动
                if (Mathf.Abs(GameManager.instance.level * 12 - 5 - transform.position.x) < 0.05f) return false;
                // 检测左侧物体，如果为wall 不可移动
                hit = Physics2D.Raycast(transform.position, Vector2.left, 1.0f);
                if (hit) return checkHitThing(hit);
                return true;
            case moveDir.Right:
                // 如果距离右边界小于0.05，认为位于右边界不可向右移动
                if (Mathf.Abs(GameManager.instance.level * 12 + 5 - transform.position.x) < 0.05f) return false;
                // 检测右侧物体，如果为wall 不可移动
                hit = Physics2D.Raycast(transform.position, Vector2.right, 1.0f);
                if (hit) return checkHitThing(hit);
                return true;
            case moveDir.Top:
                // 如果距离上边界小于0.05，认为位于上边界不可向上移动
                if (Mathf.Abs(7.5f - transform.position.y) < 0.55f) return false;
                // 检测上侧物体，如果为wall 不可移动
                hit = Physics2D.Raycast(transform.position, Vector2.up, 1.0f);
                if (hit) return checkHitThing(hit);
                return true;
            case moveDir.Bottom:
                // 如果距离下边界小于0.05，认为位于下边界不可向下移动
                if (Mathf.Abs(-2.5f - transform.position.y) < 0.55f) return false;
                // 检测下侧物体，如果为wall 不可移动
                hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f);
                if (hit) return checkHitThing(hit);
                return true;
            default:
                return false;
        }
    }

    // 检测即将交互的物品返回能否成功交互
    bool checkHitThing(RaycastHit2D hit)
    {
        switch(hit.collider.tag)
        {
            case "Wall":
                return false;
            case "Enemy":
                if (hit.collider.gameObject.GetComponent<Enemy>().CanBeDefeated())
                {
                    hit.collider.SendMessage("Action");
                    GameManager.instance.ac.PlayClip(ClipType.Battle);
                    return true;
                } else
                {
                    GameManager.instance.ui.ActiveTips("你无法打败他");
                }
                return false;
            case "Boss":
                if (hit.collider.gameObject.GetComponent<Enemy>().CanBeDefeated())
                {
                    GameManager.instance.ac.PlayClip(ClipType.Battle);
                    GameManager.instance.GameOver();
                }
                else
                {
                    GameManager.instance.ui.ActiveTips("你无法打败他");
                }
                return false;
            case "Item":
                hit.collider.SendMessage("Action");
                GameManager.instance.ac.PlayClip(ClipType.Item);
                return true;
            case "Stair":
                hit.collider.SendMessage("Action");
                GameManager.instance.ac.PlayClip(ClipType.Item);
                _offset = 0;
                return true;
            case "NPC":
                hit.collider.SendMessage("Action");
                return false;
            case "TalkedNPC":
                return false;
            case "Door":
                if (hit.collider.gameObject.GetComponent<Doors>().CanOpen())
                {
                    hit.collider.SendMessage("Action");
                    GameManager.instance.ac.PlayClip(ClipType.Door);
                    return true;
                }  
                return false;
            case "Fly":
                GameManager.instance.pa.flyShoe = true;
                GameManager.instance.ui.UpdateUI();
                GameManager.instance.ui.ActiveTips("获得传送道具，可以传送到任意楼层");
                Destroy(hit.collider.gameObject);
                return true;
            case "Book":
                GameManager.instance.pa.book = true;
                GameManager.instance.ui.UpdateUI();
                GameManager.instance.ui.ActiveTips("获得图鉴道具，可以查看怪物属性");
                Destroy(hit.collider.gameObject);
                return true;
            default:
                hit.collider.SendMessage("Action");
                return true;

        }
    }

    public void Talk()
    {
        isTalking = true;
    }

    public void TalkEnd()
    {
        isTalking = false;
    }
}
