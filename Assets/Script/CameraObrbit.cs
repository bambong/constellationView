using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CameraObrbit : MonoBehaviour
{
    public float MinDistance = -1000f;
    public float MaxDistance = 130f;
    float distance = 1000;
    float distanceTarget;
    Vector2 mouse;
    Vector2 mouseOnDown;
    Vector2 rotation;
    Vector2 target = new Vector2(Mathf.PI * 3 / 2, Mathf.PI / 6);
    Vector2 targetOnDown;

    public float speed = 25.0f;
    public Transform cameraTarget;
    private Camera thisCamera;
    private Vector3 worldDefalutForward;


    // Use this for initialization
    float time = 0;
    float fades = 0.0f;
    

    void Start () {
        distanceTarget = transform.position.magnitude;
        thisCamera = GetComponent<Camera>();
        worldDefalutForward = transform.forward;
        
    }
    bool down = false;
    bool focusOn = false; 
    
    // Update is called once per frame

    void ZoomInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * speed;
    
        if (thisCamera.fieldOfView <= 20.0f && scroll < 0) return; //최대 줌인
        if (thisCamera.fieldOfView >= 70.0f && scroll > 0) return;//최대 줌 아웃

        //줌인 아웃 하기
        thisCamera.fieldOfView += scroll;
    } // 줌인 줌아웃 input


    void FocusCheck()
    {

        if (!focusOn)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            int layerMask = 1 << LayerMask.NameToLayer("Constellation");

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {

                Debug.Log("FocusOn");
                StartCoroutine(FocusConstellation(hit));
                focusOn = true;
            }
           

        } 

    }
    IEnumerator FocusConstellation(RaycastHit focustarget)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
        int layerMask = 1 << LayerMask.NameToLayer("Constellation");
        Constellation temp = focustarget.collider.GetComponent<Constellation>();
        temp.FadeStart();


        while (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        { 
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);       
            yield return null;
                      
        }

        Debug.Log("FocusOff");
        temp.FadeReset();
        focusOn = false;

    }
    void Update()
    {

#if UNITY_ANDROID || UNITY_IPHONE

        if (Input.touchCount > 0) // 모바일입력
        {
            if(Input.touches[0].phase== TouchPhase.Began)
            {
                down = true;
                mouseOnDown.x = Input.touches[0].position.x;
                mouseOnDown.y = -Input.touches[0].position.y;

                targetOnDown.x = target.x;
                targetOnDown.y = target.y;
            }
            else if(Input.touches[0].phase== TouchPhase.Canceled||
                Input.touches[0].phase== TouchPhase.Ended)
            {
                down = false;
            }
        }


      

#else

        if (Input.GetMouseButtonDown(0)) 
            {
                down = true;
                mouseOnDown.x = Input.mousePosition.x;
                mouseOnDown.y = -Input.mousePosition.y;

                targetOnDown.x = target.x;
                targetOnDown.y = target.y;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                down = false;
            }


#endif

        if (down) // 둘중하나 input 을 받는다면 
        {
            if (Input.touchCount > 0)
            {
                mouse.x = Input.touches[0].position.x;
                mouse.y = -Input.touches[0].position.y;
            }
            else
            {
                mouse.x = Input.mousePosition.x;
                mouse.y = -Input.mousePosition.y;
            }
            float zoomDamp = distance / 1;

            target.x = targetOnDown.x + (mouse.x - mouseOnDown.x) * 0.005f * zoomDamp;
            target.y = targetOnDown.y + (mouse.y - mouseOnDown.y) * 0.005f * zoomDamp;

            target.y = Mathf.Clamp(target.y, -Mathf.PI / 2 + 0.01f, Mathf.PI / 2 - 0.01f);
        }


        ZoomInput();


        distanceTarget -= Input.GetAxis("Mouse ScrollWheel");
        distanceTarget = Mathf.Clamp(distanceTarget, MinDistance, MaxDistance);

        rotation.x += (target.x - rotation.x) * 0.1f;
        rotation.y += (target.y - rotation.y) * 0.1f;
        distance += (distanceTarget - distance) * 0.3f;
        Vector3 position;
        position.x = distance * Mathf.Sin(rotation.x) * Mathf.Cos(rotation.y);
        position.y = distance * Mathf.Sin(rotation.y);
        position.z = distance * Mathf.Cos(rotation.x) * Mathf.Cos(rotation.y);
        transform.position = position;
        transform.LookAt(Vector3.zero);


        int layerMask = 1 << LayerMask.NameToLayer("Constellation");
        SpriteRenderer render;



        //RaycastHit hit;
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        ////if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        ////{



        ////    Debug.Log("hit");
        ////    time += Time.deltaTime;
        ////    render = hit.transform.GetComponent<SpriteRenderer>();
        ////    if (fades < 1.0f && time >= 0.1f)
        ////    {
        ////        fades += 0.04f;
        ////        time = 0;
        ////        render.color = new Color(1, 1, 1, fades);
        ////    }
        ////}
        ////else
        ////{
        ////    time = 0;
        ////    fades = 0;
        ////}
        ///

        FocusCheck();

    }
}
