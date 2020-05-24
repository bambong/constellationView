using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlpha : MonoBehaviour
{
    SpriteRenderer render;

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        render.color = new Color(1, 1, 1, 1); //색상은 흰색에 Alpa 값은 1(화면에 나타남.)
    }
}
