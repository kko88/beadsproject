using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {


    public float Speed;
    public CharacterController Controller;
    private Vector3 Position;

    public AnimationClip Run;
    public AnimationClip Idle;

    public static bool Attack;
    public static bool Die;

	void Start () 
    {
        Position = transform.position;
	}


    void Update()
    {
        if (!Attack && !Die)
        {
            if (Input.GetMouseButton(0))
            {
                LocatePosition();
            }

            moveToPosition();
        }
        else
        {

        }
    }

    void LocatePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 1000))
        {
            if (hit.collider.tag != "Player" && hit.collider.tag != "Enemy")
            {
                Position = hit.point;
            }
        }
    }

    void moveToPosition()
    {
        if(Vector3.Distance(transform.position, Position) > 1)
        {
        Quaternion newRotation = Quaternion.LookRotation(Position - transform.position, Vector3.forward);

        newRotation.x = 0f;
        newRotation.z = 0f;


        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10);
        Controller.SimpleMove(transform.forward * Speed);

        animation.CrossFade(Run.name);
        }
        else
        {
            animation.CrossFade(Idle.name);
        }
    }
}
