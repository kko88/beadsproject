using UnityEngine;
using System.Collections;
/// <summary>
/// 우측 넘버패드 4,6번 좌/우 회전
/// 우측 넘버패드 2,8번 상,하 회전
/// 우측 넘버패드 3,9번 
/// 마우스 우클릭 자유회전
/// </summary>
public class CameraTest : MonoBehaviour {

    public Transform target;
    public string playerTagName = "Character1";

    public float walkDistance;
    public float runDistance;
    public float height;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;    

    private Transform _myTransform;
    private float _x; 
    private float _y;
    private bool _camButtonDown = false;
    private bool _rotateCameraKeyPressed = false;

    void Awake()
    {
        _myTransform = transform;
    }
	void Start () {
        if (target == null)
            Debug.LogWarning("타겟이 없습니다.");
       else
        {
           CameraSetup();
        }
	}

    void Update()
    {
        if (Input.GetButtonDown("Rotate Camera Button")) {
            _camButtonDown = true;
        }
        if (Input.GetButtonUp("Rotate Camera Button"))
        {
            _x = 0;
            _y = 0;
            _camButtonDown = false;
        }

        if (Input.GetButtonDown("Rotate Camera Horizontal Button") || Input.GetButtonDown("Rotate Camera Vertical Button"))
            _rotateCameraKeyPressed = true;

        if (Input.GetButtonUp("Rotate Camera Horizontal Button") || Input.GetButtonUp("Rotate Camera Vertical Button"))
        {
            _x = 0;
            _y = 0;
            _rotateCameraKeyPressed = false;
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            if (_rotateCameraKeyPressed)
            {
                _x += Input.GetAxis("Rotate Camera Horizontal Button") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Rotate Camera Vertical Button") * ySpeed * 0.02f;

                RotateCamera(); 
            }
            else if (_camButtonDown)
            {
                _x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                _y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                RotateCamera(); 
            }
            else
            {
//                _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
//                _myTransform.LookAt(target);
     
                // Calculate the current rotation angles
                float wantedRotationAngle = target.eulerAngles.y;
                float wantedHeight = target.position.y + height;

                float currentRotationAngle = _myTransform.eulerAngles.y;
                float currentHeight = _myTransform.position.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

                // Damp the height
                currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

                // Convert the angle into a rotation
                Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                // Set the position of the camera on the x-z plane to:
                // distance meters behind the target
                _myTransform.position = target.position;
                _myTransform.position -= currentRotation * Vector3.forward * walkDistance;
                
                // Set the height of the camera
                _myTransform.position = new Vector3(_myTransform.position.x,  currentHeight, _myTransform.position.z);

                // Always look at the target
                _myTransform.LookAt(target);

            }

        }

        else
        {
            GameObject go = GameObject.FindGameObjectWithTag(playerTagName);

            if (go == null)
                return;

            target = go.transform;
        }
      
    }

    private void RotateCamera() 
    {
        Quaternion rotation = Quaternion.Euler(_y, _x, 0);
        var position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + target.position;

        _myTransform.rotation = rotation;
        _myTransform.position = position;
    }
    public void CameraSetup()
    {
            _myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
            _myTransform.LookAt(target);
    }
}
