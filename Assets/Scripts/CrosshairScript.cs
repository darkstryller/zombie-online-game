using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
    }
    
    void Update()
    {
        Vector2 MouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MouseCursorPos;
    }
}
