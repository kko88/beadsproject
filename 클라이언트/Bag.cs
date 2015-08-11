using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Bag : MonoBehaviour
{

    public enum State
    {
        open,
        close,
        inbetween
    }

    public AudioClip openSound;
    public string openAnimName;

    public GameObject part;  // 상자 부분 ,top이랑 전체로 나뉘어짐
    private Color[] _defaultColors; // 색 초기값

    private State state;

    public float maxDistance = 4;

    private GameObject _player;
    private Transform _myTransform;
    private bool inUse = false;
    private bool _used = false;  // 상자가 사용되었는지 여부

    public List<Item> loot = new List<Item>();

    public static float defaultLifeTimer = 100;
    private float _lifeTimer = 0;

    void Start()
    {
        _myTransform = transform;
        state = Bag.State.close;


        _defaultColors = new Color[2];
            for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                _defaultColors[cnt] = part.renderer.material.GetColor("_Color");
    }


    void Update()
    {
        _lifeTimer += Time.deltaTime;

        if (_lifeTimer > defaultLifeTimer && state == Bag.State.close)
            DestroyBag();

        if (!inUse)
            return;

        if (_player == null)
            return;

        if (Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
            myGUI.bag.ForceClose();
    }
    public void OnMouseEnter()
    {

        HighLight(true);
    }

    public void OnMouseExit()
    {

        HighLight(false);
    }

    public void OnMouseUp()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");

        if (go == null)
            return;
        if (Vector3.Distance(_myTransform.position, go.transform.position) > maxDistance && !inUse)
            return;

        switch (state)
        {

            case State.open:
                state = Bag.State.inbetween;
                ForceClose();
                break;
            case State.close:
                if (myGUI.bag != null)
                {
                    myGUI.bag.ForceClose();
                }
                state = Bag.State.inbetween;
                StartCoroutine("Open");
                break;
        }
    }

    private void Open()
    {
        myGUI.bag = this;

        _player = GameObject.FindGameObjectWithTag("Player");  // 상자가 플레이어 찾아서 거리재기
        inUse = true;

        audio.PlayOneShot(openSound);

        if (!_used)   // 사용되었던 주머니가 아니면 1개의 랜덤 아이템 생성 (디폴트값 false)
            PopulateBag(1);

        state = Bag.State.open;

        Messenger.Broadcast("DisplayItemLoot");

    }

    private void PopulateBag(int x)
    {
        for (int cnt = 0; cnt < x; cnt++)
        {
            loot.Add(ItemGenerator.CreateItem());
        }

        _used = true;
    }

    private IEnumerator Close()
    {
        _player = null;
        inUse = false;


        float tempTimer = 0;


        yield return new WaitForSeconds(tempTimer);
        state = Bag.State.close;

        if (loot.Count == 0)
            DestroyBag();   // 주머니에 아이템이 없으면 주머니 삭제

    }


    private void DestroyBag()
    {
        loot = null;
        Destroy(gameObject);
    }
    public void ForceClose()
    {
        Messenger.Broadcast("CloseBag");
        StopCoroutine("Open");
        StartCoroutine("Close");
    }

    private void HighLight(bool glow)
    {
        if (glow)
        {
                for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                    part.renderer.material.SetColor("_Color", Color.green);
        }
        else
        {
                for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                    part.renderer.material.SetColor("_Color", _defaultColors[cnt]);
        }
    }
}