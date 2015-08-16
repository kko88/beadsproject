using System.Timers;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(AudioSource))]

public class PlayerAttack : MonoBehaviour
{
    // 타겟팅+어택

    public List<Transform> targets; // transform 리스트
    public Transform selectedTarget;
    public Transform myTransform;

    //  public float attackTimer; // 공격시간
    //  public float coolDown;  // 쿨다운
    public AnimationClip attackClip;
    public AnimationClip sAttackClip;  // 스턴공격
    public AnimationClip Dieclip;
    public AnimationClip rAttackClip; // 불 공격

    public AnimationClip lightClip; // 번개 공격
    public AnimationClip waterClip; // 물 회복마법
    public AnimationClip windClip; // 바람 이속증가 마법



    public AudioClip attackSound;
    public AudioClip rAttackSound;  // 불공격 사운드
    public AudioClip dieSound;
    public AudioClip lightSound; // 번개 사운드
    public AudioClip waterSound; // 물회복마법 사운드
    public AudioClip windSound; //바람 이속증가 사운드
    public AudioClip HealSound; // 물약사운드



    private float attackTime;

    public int Damage;
    public int maxHealth;
    public int Health;
    private int wasteMana = 0;
    public int maxMp;
    public int curMp;


    public int targetMaxHP;
    public int targetHP;



    private double ImpactLength;
    public double ImpactTime;
    public bool Impacted;
    public bool RImpacted; // 불 범위공격
    public bool LImpacted; // 번개 범위공격

    public bool inAction; // 일반
    public bool inSAction; // 스턴
    public bool inRAction; // 불 범위공격
    public bool inLAction; // 번개 범위공격

    public GameObject sparticleEffect; // 스턴
    public GameObject rparticleEffect; // 불 범위
    public GameObject lparticleEffect; // 번개 범위
    public GameObject waterparticleEffect; // 물 마나회복
    public GameObject windparticleEffect; // 바람 이속증가
    public GameObject healparticleEffect;


    public GameObject Attack;

    public int sAttackdamage = 40;  //데미지
    public int rAttackdamage = 40;
    public int lAttackdamage = 80;


    public int sAttackmp = 10;  // 소모마력
    public int rAttackmp = 20;
    public int lAttackmp = 60;
    public int wAttackmp = 40;
    public int buffmp = 50;

    public int regenHp = 60;  // 물 마나회복마법
    public int regenMp = 60;  //힐링포션 

    public float windBuffTime = 10.0f;


    bool Started;
    bool Ended;

    public float combatEscapeTime;

    public float countDown;

    public bool powerAttack;

    public GUIText notEnoughMp;

    private bool nAttackYN;
    private bool sAttackYN;
    private bool fireYN;
    private bool lightningYN;
    private bool windYN;
    private bool waterYN;
    private bool hpYN;

    public int range = 15;

    public void Awake()
    {
     //   DontDestroyOnLoad(this);

        nAttackYN = false;
        sAttackYN = false;
        fireYN = false;
        lightningYN = false;
        windYN = false;
        waterYN = false;
        hpYN = false;
    }

    void Start()
    {
        targets = new List<Transform>(); // 타겟에 저장
        selectedTarget = null;
        myTransform = transform;
        AddAllEnemies();

        Health = maxHealth;
        curMp = maxMp;
        ImpactLength = (animation[attackClip.name].length * ImpactTime);

        //      GameObject.Find("MP").GetComponent<UILabel>().text = curMp.ToString();
        //       GameObject.Find("HP").GetComponent<UILabel>().text = Health.ToString();
    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab)) // tab키로 타겟잡기
        {
            TargetEnemy();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(nAttackYN == false)
            inAction = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (sAttackYN == false)
            {
                if (curMp > sAttackmp)
                    inSAction = true;

                else
                    GameObject.Find("HUDText").SendMessage("notEnoughtMp", notEnoughMp);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (fireYN == false)
            {
                if (curMp > rAttackmp)
                    inRAction = true;

                else
                    GameObject.Find("HUDText").SendMessage("notEnoughtMp", notEnoughMp);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (lightningYN == false)
            {
                if (curMp > lAttackmp)
                    inLAction = true;

                else
                    GameObject.Find("HUDText").SendMessage("notEnoughtMp", notEnoughMp);
            }
        }

        if (inAction)
        {
            if (AttackAct(0, 1, KeyCode.Alpha1, null))
            {
            }
            else
            {
                inAction = false;
            }
        }
        if (inSAction)
        {
            if (AttackAct(5, 1, KeyCode.Alpha2, sparticleEffect))
            {

            }
            else
            {
                inSAction = false;
            }
        }
        if (inRAction)
        {
            if (AttackAct(0, 1, KeyCode.Alpha3, rparticleEffect))
            {

            }
            else
            {
                inRAction = false;
            }
        }

        if (inLAction)
        {
            if (AttackAct(4, 2, KeyCode.Alpha4, lparticleEffect))
            {

            }

            else
            {
                inLAction = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (windYN == false)
            {
                if (curMp > buffmp)
                {
                    Wind();
                    GameObject.Find("BuffTime").SendMessage("Visible");

                }
                else
                    GameObject.Find("HUDText").SendMessage("notEnoughtMp", notEnoughMp);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if(waterYN == false)
            Water();
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if(hpYN == false)
            HealingPosition();
        }

        Die();

    }


    public void AddAllEnemies()  // 모든적 타겟에 넣기
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in go)  // foreach문 go 배열에 enemy 만큼 Addtarget 함수에 넣어서 실행
            AddTarget(enemy.transform);
    }

    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);             // 좌표 형태 enmey 받아서 리스트에 더해준다.
    }

    private void SortTargetsByDistance()        // 거리 비교 후 정렬
    {
        targets.Sort(delegate(Transform t1, Transform t2)
        {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });

    }
    public void TargetClear()
    {
        targets.Clear();
        selectedTarget = null;
    }


    public void TargetEnemy()
    {


        if (targets.Count == 0)
            AddAllEnemies();

        if (targets.Count > 0)
        {
            if (selectedTarget == null) // 선택된 적이없을때    
            {
                SortTargetsByDistance();  // 거리재기 함수
                selectedTarget = targets[0]; // 거리순 가장 근접한 적 선택
            }

            else
            {
                int index = targets.IndexOf(selectedTarget);

                if (index < targets.Count - 1)
                {
                    index++;
                }
                else
                {
                    index = 0;
                }
                DeselectTarget();    // 선택안된 타겟 같이 보여짐
                selectedTarget = targets[index];

            }
            SelectTarget();
            GameObject.Find("GUI").SendMessage("targeting", selectedTarget);

        }
    }





    public void SelectTarget() // 선택
    {
        Transform name = selectedTarget.FindChild("Name");
        if (name == null)
        {
            return;
        }

        name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().name;
        name.GetComponent<MeshRenderer>().enabled = true;
//        selectedTarget.GetComponent<Mob>().DisplayHealth();


    }


    private void DeselectTarget() // 선택안된 나머지 
    {
        selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
        selectedTarget = null;
    }

    public bool AttackAct(int stunSeconds, double scaleDamage, KeyCode key, GameObject particleEffect)
    {
        if (key == KeyCode.Alpha1)
        {
            animation.Play(attackClip.name);
            if (animation[attackClip.name].time > 0.9 * animation[attackClip.name].length)
            {
                Impacted = false;
                return false;
            }

            Impact(stunSeconds, scaleDamage, particleEffect, attackClip.name);
            nAttackYN = true;
            Invoke("Ndelay", 2.0f);
        }
        if (key == KeyCode.Alpha2)
        {
            animation.Play(sAttackClip.name);

            if (animation[sAttackClip.name].time > 0.9 * animation[sAttackClip.name].length)
            {
                Impacted = false;
                return false;
            }

            Impact(stunSeconds, scaleDamage, particleEffect, sAttackClip.name);
            sAttackYN = true;
            Invoke("Sdelay", 2.0f);
        }
        if (key == KeyCode.Alpha3)
        {
            animation.Play(rAttackClip.name);

            if (animation[rAttackClip.name].time > 0.9 * animation[rAttackClip.name].length)
            {
                RImpacted = false;
                return false;
            }

            RImpact(stunSeconds, scaleDamage, particleEffect, rAttackClip.name);
            fireYN = true;
            Invoke("Fdelay", 2.0f);
        }
        if (key == KeyCode.Alpha4)
        {
            animation.Play(lightClip.name);

            if (animation[lightClip.name].time > 0.9 * animation[lightClip.name].length)
            {
                LImpacted = false;
                return false;
            }
            Lightning(stunSeconds, scaleDamage, particleEffect, lightClip.name);
            lightningYN = true;
            Invoke("Ldelay", 2.0f);
            
        }
        return true;
    }

    public void ResetAttackAct()
    {
        Impacted = false;
        RImpacted = false;
        LImpacted = false;
        animation.Stop(attackClip.name);
    }


    void Impact(int stunSeconds, double scaleDamage, GameObject particleEffect, string aniName)
    {
        if (selectedTarget == null) return;
        Transform target = selectedTarget.FindChild("Name");
        if (selectedTarget != null && animation.IsPlaying(aniName) && !Impacted)
        {
            if ((animation[aniName].time) > ImpactLength && (animation[aniName].time < 0.9 * animation[aniName].length))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);
                selectedTarget.GetComponent<MobMove>().getHit(Damage * scaleDamage);
                selectedTarget.GetComponent<MobMove>().getStun(stunSeconds);
                selectedTarget.GetComponent<MobMove>().SendMessage("getStunEffect");
                audio.PlayOneShot(attackSound);
                wasteMana = sAttackmp;
                wasteMP();
                if (particleEffect != null)
                {
                    Instantiate(particleEffect, new Vector3(selectedTarget.transform.position.x, selectedTarget.transform.position.y + 4f, selectedTarget.transform.position.z), Quaternion.identity);
                }
                Impacted = true;
            }
        }
    }

    void RImpact(int stunSeconds, double scaleDamage, GameObject particleEffect, string aniName) // 불
    {

        if (animation.IsPlaying(aniName) && !RImpacted)
        {
            if ((animation[aniName].time) > ImpactLength && (animation[aniName].time < 0.9 * animation[aniName].length))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);
                audio.PlayOneShot(rAttackSound);
                wasteMana = rAttackmp;
                wasteMP();
                if (particleEffect != null)
                {
                    GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

                    foreach (GameObject enemy in go)
                    {
                        if (Vector3.Distance(enemy.transform.position, transform.position) < range)
                        {
                            enemy.SendMessage("getHit", rAttackdamage);
                        }
                    }
                    Instantiate(particleEffect, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.identity);
                }
                RImpacted = true;
            }
        }
    }

    void Lightning(int stunSeconds, double scaleDamage, GameObject particleEffect, string aniName)
    {
        if (animation.IsPlaying(aniName) && !LImpacted)
        {
            if ((animation[aniName].time) > ImpactLength && (animation[aniName].time < 0.9 * animation[aniName].length))
            {
                countDown = combatEscapeTime + 2;
                CancelInvoke("combatEscapeCountDown");
                InvokeRepeating("combatEscapeCountDown", 0, 1);
                audio.PlayOneShot(rAttackSound);
                wasteMana = lAttackmp;
                wasteMP();
                if (particleEffect != null)
                {
                    GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

                    foreach (GameObject enemy in go)
                    {
                        if (Vector3.Distance(enemy.transform.position, transform.position) < range)
                        {
                            enemy.SendMessage("getHit", lAttackdamage);
                            enemy.SendMessage("getLightningEffect");
                        }
                    }
                    Instantiate(particleEffect, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.identity);
                }
                LImpacted = true;
            }
        }
    }

    void combatEscapeCountDown()
    {
        countDown = countDown - 1;
        if (countDown == 0)
        {
            CancelInvoke("combatEscapeCountDown");
        }
    }

    public void getHit(int Damage)
    {
        Health = Health - Damage;
        float healthPercent = Health * 100 / maxHealth;
        GameObject.Find("hpBar").SendMessage("hpBarCutoff", healthPercent);
        GameObject.Find("GetDamageText").SendMessage("pcGetDamage", Damage);
   //     GameObject.Find("HP").GetComponent<UILabel>().text = Health.ToString();
        
        if (Health < 0)
        {
            Health = 0;
            audio.PlayOneShot(dieSound);
            animation.Play(Dieclip.name);
            myTransform.tag = "Chest";
        }
    }

    public void wasteMP()
    {
        curMp = curMp - wasteMana;
        float mpPercent = curMp * 100 / maxMp;
        GameObject.Find("mpBar").SendMessage("mpBarCutoff", mpPercent);
        GameObject.Find("HUDText").SendMessage("pcMpWaste", wasteMana);  
//        GameObject.Find("MP").GetComponent<UILabel>().text = curMp.ToString();
    
        if (curMp < 0)
        {
            curMp = 0;
        }
    }


    public int getMaxHP()
    {
        return maxHealth;
    }

    public int getHP()
    {
        return Health;
    }

    public bool IsDead()
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void Die()
    {
        if (IsDead() && !Ended)
        {
            if (!Started)
            {
                Started = true;
            }

            if (Started && !animation.IsPlaying(Dieclip.name))
            {
                animation.Play(Dieclip.name);
                Ended = true;
                Started = false;
                Debug.Log("GameOver");
            }
        }
    }



    public void Water()
    {
        curMp += regenMp;
        float mpPercent = curMp * 100 / maxMp;
        GameObject.Find("HUDText").SendMessage("MpRegen", regenMp);
        GameObject.Find("mpBar").SendMessage("mpBarCutoff", mpPercent);
   //     if (mpPercent < 100 && curMp > 0)
   //     {
   //         GameObject.Find("MP").GetComponent<UILabel>().text = curMp.ToString();
   //     }
        audio.PlayOneShot(waterSound);
        animation.Play(waterClip.name);

        if (waterparticleEffect != null)
        {
            Instantiate(waterparticleEffect, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.identity);
        }

        waterYN = true;
        Invoke("Wdelay", 2.0f);
    }


    public void HealingPosition()
    {
        Health += regenHp;
        float healthPercent = Health * 100 / maxHealth;
        GameObject.Find("HUDText").SendMessage("HpRegen", regenHp);
        GameObject.Find("hpBar").SendMessage("hpBarCutoff", healthPercent);
  //      if (healthPercent < 100 && Health > 0)
  //      {
  //          GameObject.Find("HP").GetComponent<UILabel>().text = Health.ToString();
  //      }
        audio.PlayOneShot(HealSound);

        if (healparticleEffect != null)
        {
            Instantiate(healparticleEffect, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), Quaternion.identity);
        }
        hpYN = true;
        Invoke("Hpdelay", 2.0f);
    }

    void Hpdelay()
    {
        hpYN = false;
    }
    void Ndelay()
    {
        nAttackYN = false;
    }
    void Sdelay()
    {
        sAttackYN = false;
    }
    void Fdelay()
    {
        fireYN = false;
    }
    void Ldelay()
    {
        lightningYN = false;
    }

    void Wdelay()
    {
        waterYN = false;
    }


    public void Wind()
    {   
        audio.PlayOneShot(windSound);
        wasteMana = buffmp;
        wasteMP();
        myTransform.GetComponent<AdvancedMovement>().runAnimName = "sprintSword";
        myTransform.GetComponent<AdvancedMovement>().runMultiplier = 6;
        StartCoroutine(WindBuff(0.01f));
        windYN = true;
        if (windparticleEffect != null)
        {
            Instantiate(windparticleEffect, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), Quaternion.identity);
        }
    }

       

    IEnumerator WindBuff(float waittime)
    {
        yield return new WaitForSeconds(windBuffTime);
        myTransform.GetComponent<AdvancedMovement>().runAnimName = "run";
        myTransform.GetComponent<AdvancedMovement>().runMultiplier = 2;
        GameObject.Find("BuffTime").SendMessage("Start");
        windYN = false;
    }


}
