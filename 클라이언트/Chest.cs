using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Chest : MonoBehaviour
{

    public enum State
    {
        open,   
        close,
        inbetween
    }

    public AudioClip openSound;
    public AudioClip closeSound;

   
    public GameObject[] parts;  // 상자 부분 ,top이랑 전체로 나뉘어짐
    private Color[] _defaultColors; // 색 초기값

    public State state;

    public float maxDistance = 2;

    private GameObject _player;
    private Transform _myTransform;
    public bool inUse = false;
    private bool _used = false;  // 상자가 사용되었는지 여부

    public List<Item> loot = new List<Item>();
      void Start()
       {
           _myTransform = transform;
           state = Chest.State.close;


          _defaultColors = new Color[parts.Length]; 
          if (parts.Length > 0)
              for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                  _defaultColors[cnt] = parts[cnt].renderer.material.GetColor("_Color");
              }


      void Update()
      {
          if (!inUse)
              return;

          if (_player == null)
              return;

          if (Vector3.Distance(transform.position, _player.transform.position) > maxDistance)
                myGUI.chest.ForceClose();
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
                   state = Chest.State.inbetween;
                   ForceClose();
                   break;
               case State.close:
                   if (myGUI.chest != null)
                   {
                       myGUI.chest.ForceClose();
                   }
                   state = Chest.State.inbetween;
                   StartCoroutine("Open");
                   break;
           }
       }

       private IEnumerator Open()
       {
           myGUI.chest = this;

           _player = GameObject.FindGameObjectWithTag("Player");  // 상자가 플레이어 찾아서 거리재기
           inUse = true;

           animation.Play("box_open");

           audio.PlayOneShot(openSound);

           if(!_used)   // 사용되었던 상자가아니면 5개의 랜덤 아이템 생성 (디폴트값 false)
           PopulateChest(5);    

           yield return new WaitForSeconds(animation["box_open"].length);
           state = Chest.State.open;

           Messenger.Broadcast("DisplayLoot");
       }

       private void PopulateChest(int x) 
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
           animation.Play("box_close");
           audio.PlayOneShot(closeSound); 
           yield return new WaitForSeconds(animation["box_close"].length);
           state = Chest.State.close;

           if (loot.Count == 0)
               Destroy(gameObject);    // 상자에 아이템이 없으면 상자 삭제
           
       }

       public void ForceClose()
       {
           Messenger.Broadcast("CloseChest");
           StopCoroutine("Open");
           StartCoroutine("Close");
       }

       private void HighLight(bool glow)
       {
           if (glow)
           {
               if (parts.Length > 0)
                   for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                       parts[cnt].renderer.material.SetColor("_Color", Color.green);
           }
           else
           {
               if (parts.Length > 0)
                   for (int cnt = 0; cnt < _defaultColors.Length; cnt++)
                       parts[cnt].renderer.material.SetColor("_Color", _defaultColors[cnt]);
           }
       }
   }