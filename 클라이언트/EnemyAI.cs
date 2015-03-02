using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
    public int maxDistance;

	private Transform myTransform;
	void Awake() {
		myTransform = transform;
		}

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag ("Player"); // 플레이어 찾기

		target = go.transform;

        maxDistance = 2;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine (target.position, myTransform.position, Color.red);  // 플레이어,적 표시선

		//타겟 시선
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);

        if(Vector3.Distance(target.position, myTransform.position) > maxDistance) {

		//타겟쪽 이동
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
	}
}