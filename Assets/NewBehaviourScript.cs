using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public float speed = 25.0f;
    public Transform cameraTarget;

    private Camera thisCamera;
    private Vector3 worldDefalutForward;

    private void Start()
    {
        thisCamera = GetComponent<Camera>();
        worldDefalutForward = transform.forward;
    }
    
    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        //최대 줌인
        if (thisCamera.fieldOfView <= 20.0f && scroll < 0)
        {
            thisCamera.fieldOfView = 20.0f;
        }

        //최대 줌 아웃
        else if (thisCamera.fieldOfView >= 70.0f && scroll > 0)
        {
            thisCamera.fieldOfView = 70.0f;  
		}
        //줌인 아웃 하기
        else
        {
            thisCamera.fieldOfView += scroll;
		}
        //상하좌우 화면이동
        Vector3 vec = new Vector3(
        Input.GetAxis("Vertical"),
        Input.GetAxis("Horizontal"),0);
        transform.Rotate(vec);
	}
}
