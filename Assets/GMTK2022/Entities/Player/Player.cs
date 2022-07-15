using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void OnClick()
    {
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
