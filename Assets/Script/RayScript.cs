using UnityEngine;
using System.Collections;

public class RayScript : MonoBehaviour
{
    private Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스 커서 포인트에 ray 발사
    private RaycastHit hit; // 충돌체에 접근할 때 사용하는 변수.
    private int layerMask = 1 << LayerMask.NameToLayer("Constellation");

    void Start()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.transform.name);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit!\n");
                Debug.Log(hit.collider);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {   // Do something with the object that was hit by the raycast.

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log(hit.transform.name);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit!\n");
                Debug.Log(hit.collider);
            }
        }
        else
        {

        }
    }
}