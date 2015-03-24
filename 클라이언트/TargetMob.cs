using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetMob : MonoBehaviour
{
    public List<Transform> targets; // transform 리스트
    public Transform selectedTarget;
    private Transform myTransform;
    // Use this for initialization
    void Start()
    {

        targets = new List<Transform>(); // 타겟에 저장
        selectedTarget = null;
        myTransform = transform;
        AddAllEnemies();

    }

    public void AddAllEnemies()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in go)
            AddTarget(enemy.transform);
    }

    public void AddTarget(Transform enemy)
    {
        targets.Add(enemy);
    }


    private void SortTargetsByDistance()
    {
        targets.Sort(delegate(Transform t1, Transform t2)
        {
           return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position));
        });
    }
    private void TargetEnemy()
    {
        if (targets.Count == 0)
        
            AddAllEnemies();

        
        if (targets.Count > 0)
        {
                if (selectedTarget == null) // 적이없을때    
                {
                    SortTargetsByDistance();
                    selectedTarget = targets[0];
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
                    DeselectTarget();    // 선택안된 타겟 같이 보여짐(초록색)
                    selectedTarget = targets[index];
                }
                SelectTarget();    
        }
        
        
    }

    private void SelectTarget() // 선택하면 색변화(tab키)
    {
        Transform name = selectedTarget.FindChild("Name");

        if (name == null)
        {
            Debug.LogError("선택된 몹이 없습니다." + selectedTarget.name);
            return;
        }
     
   name.GetComponent<TextMesh>().text = selectedTarget.GetComponent<Mob>().Name;
        name.GetComponent<MeshRenderer>().enabled = true;
        selectedTarget.GetComponent<Mob>().DisplayHealth();

        Messenger<bool>.Broadcast("몹 체력 보기", true);
    }

    private void DeselectTarget() // 선택안된 나머지 
    {
        selectedTarget.FindChild("Name").GetComponent<MeshRenderer>().enabled = false;
        selectedTarget = null;


        Messenger<bool>.Broadcast("몹 체력 보기", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // tab키로 타겟잡기
        {
            TargetEnemy();
        }
    }
}
