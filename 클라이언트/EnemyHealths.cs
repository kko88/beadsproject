using UnityEngine;
using System.Collections;

public class EnemyHealths : MonoBehaviour
{
    public PlayerAttack player;
    public Texture2D frame;
    public Rect framePosition;


    public Transform mob;
    public int mobHP;
    public int mobMaxHP;
    public float horizontalDistance;
    public float verticalDistance;
    public float width;
    public float height;

    public Texture2D healthBar;
    public Rect healthBarPosition;

    public MobMove target; ///수정
    public float healthPercentage;
    void Start()
    {
        mob = null;
    }

    void Update()
    {
        if (mob != null)
        {
            mobHP = mob.GetComponent<MobMove>().getHP();
            mobMaxHP = mob.GetComponent<MobMove>().getMaxHP();
//            Debug.Log("몹 최대체력" + mobMaxHP + "몹현재 체력" + mobHP);
        }

        if (player.transform != null)
        {
            target = player.transform.GetComponent<MobMove>(); //수정
            //            healthPercentage = (float)target.Health / (float)target.maxHealth;
        }
        else
        {
            target = null;
            healthPercentage = 0;
        }
        OnGUI();
    }
    void OnGUI()
    {
        if (target != null && player.countDown > 0)
        {
            drawFrame();
            drawBar();
        }
    }
    void drawFrame()
    {
        framePosition.x = (Screen.width - framePosition.width) / 2;
        framePosition.width = Screen.width * 0.39f;
        framePosition.height = Screen.height * 0.0625f;
        GUI.DrawTexture(framePosition, frame);
    }

    void drawBar()
    {
        healthBarPosition.x = framePosition.x + framePosition.width * horizontalDistance;
        healthBarPosition.y = framePosition.y + framePosition.height * verticalDistance;
        healthBarPosition.width = framePosition.width * width * healthPercentage;
        healthBarPosition.height = framePosition.height * height;

        GUI.DrawTexture(healthBarPosition, healthBar);
    }
    void targeting(Transform target)
    {
        mob = target;
    }

}