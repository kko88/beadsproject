using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Targetting : MonoBehaviour {
   
    public List<Transform> targets; // transform 리스트
    public Transform selectedTarget;
    private Transform myTransform;
    bool attackAct;
	

	void Start () {

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
        targets.Sort(delegate(Transform t1, Transform t2) {
            return Vector3.Distance(t1.position, myTransform.position).CompareTo(Vector3.Distance(t2.position, myTransform.position)); 
        });
    }
    private void TargetEnemy()
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

    private void SelectTarget() // 선택하면 색변화(tab키)

    {
        selectedTarget.renderer.material.color = Color.red;
        PlayerAttack pa = (PlayerAttack)GetComponent("PlayerAttack");
    //    pa.target = selectedTarget.gameObject; 
    }

    private void DeselectTarget() // 선택안된 나머지 
    {
        selectedTarget.renderer.material.color = Color.green;
        selectedTarget = null;
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab)) // tab키로 타겟잡기
        {
            TargetEnemy();
        }
	}
}
