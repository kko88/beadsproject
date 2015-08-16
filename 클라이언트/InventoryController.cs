using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 인벤토리를 관리하는 클래스 입니다.
/// 버튼을 누를때마다 아이템을 넣도록 할께요.
/// </summary>
public class InventoryController : MonoBehaviour
{

    // 지금은 DB가 없으니... 노다가로 아이템 이름을 넣어요.
    private List<string> m_lItemNames = new List<string>();
    // 새로 만들어진 아이템들을 모아둡니다.(삭제 및 수정 등을 하기위해서)
    private List<ItemInven> m_lItems = new List<ItemInven>();
    // 우리가 만든 SampleItem을 복사해서 만들기 위해 선언합니다.
    public GameObject m_gObjSampleItem;
    // 스크롤뷰를 reposition 하기위해 선언합니다.
    public UIScrollView m_scrollView;
    // 그리드를 reset position 하기위해 선언합니다.
    public UIGrid m_grid;

    private float _doubleClickTimer = 0;
    private const float DOUBLE_CLICK_TIMER_THRESHHOLD = 0.5f;
    private Item _selectedItem;

    // Use this for initialization
    void Start()
    {
        InitItems();
    }

    // Update is called once per frame
    void Update()
    {
        // I 키를 누르면 아이템이 추가됩니다.
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(); // 아이템 추가 코드
        }
        // C키를 누르면 모두 삭제합니다.
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearAll(); // 삭제코드
        }
        // R키를 눌르면 랜덤으로 하나가 삭제됩니다.
        if (Input.GetKeyDown(KeyCode.R))
        {
            ClearRand();  // 이건 삭제예정
        }
    }
    // 아이템 이름들 세팅합시다!! 노다가...복붙하세요.
    private void InitItems()
    {
        
        

        // 다 말고.. 조금만 넣을께요^^;;
        m_lItemNames.Add("단검");
        m_lItemNames.Add("고급장검");
        m_lItemNames.Add("고급단검");
        m_lItemNames.Add("장검");
        m_lItemNames.Add("메이스");  
        m_lItemNames.Add("도끼");
    }

    // 키를 누르면 아이템을 화면에 보여주는 역할을 합니다.
    // 여기에서 가장 중요한 역할을 하는 함수죠.
    private void AddItem()
    {
        PlayerCharacter.Inventory.Add(PlayerCharacter.EquipedWeapon);
        PlayerCharacter.EquipedWeapon = null;
        // 랜덤으로 만들도록 할께요.
        // 난수는 0부터 아이템이름 리스트의 갯수 - 1 만큼입니다.
  //      int nRandomIndex = Random.Range(0, m_lItemNames.Count);
        // 새로 만들어서 그리드 자식으로 넣겠습니다.         
        GameObject gObjItem = NGUITools.AddChild(m_grid.gameObject, m_gObjSampleItem);
        // SampleItem의 Active가 꺼져있으니(아까 꼈죠?) 먼저 켜도록 할께요.
        gObjItem.SetActive(true);
        // 이제 이름과 아이콘을 세팅할께요.
        // 그럴려면 먼저 아까 만든 ItemScript를 가져와야겠죠.
        // GetComponent는 해당 게임 오브젝트가 가지고 있는 컴포넌트를 가져오는 역할을 해요.
        ItemInven itemScript = gObjItem.GetComponent<ItemInven>();
    //    itemScript.SetInfo(m_lItemNames[nRandomIndex]);
        // 이제 그리드와 스크롤뷰를 재정렬 시킵시다.
        m_grid.Reposition();
        m_scrollView.ResetPosition();
        // 그리고 관리를 위해 만든걸 리스트에 넣어둡시다.
        m_lItems.Add(itemScript);

    }

    // 모두 삭제합니다.
    private void ClearAll()
    {
        // 아까 만든거 저장해둔 리스트에서 모두 삭제해줍니다.
        for (int nIndex = 0; nIndex < m_lItems.Count; nIndex++)
        {
            if (m_lItems[nIndex] != null && m_lItems[nIndex].gameObject != null)
                Destroy(m_lItems[nIndex].gameObject);
        }
        m_lItems.Clear();
        // 그리고 그리드와 스크롤뷰 재정렬 시켜줍니다.
        m_grid.Reposition();
        m_scrollView.ResetPosition();
    }

    // 하나 랜덤으로 삭제합니다.
    private void ClearRand()
    {
        // 만든 아이템이 없으면 안되겠죠?
        if (m_lItems.Count == 0) return;
        // 지울꺼 랜덤으로 찾고.
        int nRandomIndex = Random.Range(0, m_lItems.Count);
        // 게임오브젝트 없애줍니다.
        // 여기에서는 DestroyImmediate를 사용합니다. 지우고 바로 갱신을 해야하니까요.
        // 나중에 천천히 설명하겠습니다.
        DestroyImmediate(m_lItems[nRandomIndex].gameObject);
        // 리스트에서도 지워줍시다.
        m_lItems.RemoveAt(nRandomIndex);
        // 그리고 그리드와 스크롤뷰 재정렬 시켜줍니다.
        m_grid.Reposition();
        m_scrollView.ResetPosition();
    }

    // 이 게임 오브젝트가 파괴될 때 생성했던 아이템도 삭제해줍시다.
    // 메모리 정리라고 생각하세요.
    private void OnDestroy()
    {
        for (int nIndex = 0; nIndex < m_lItems.Count; nIndex++)
        {
            // 혹시 모르니까 null이 아닐 때에만 삭제하도록 해요.
            if (m_lItems[nIndex] != null && m_lItems[nIndex].gameObject != null)
                Destroy(m_lItems[nIndex].gameObject);
        }

        m_lItems.Clear();
        m_lItems = null;

    //    m_lItemNames.Clear();
    //    m_lItemNames = null;
    }
}