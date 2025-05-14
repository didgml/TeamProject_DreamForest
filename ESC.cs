using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
    public GameObject Sound;
    public GameObject Menu;

    private void Update()
    {
        // ESC 키를 눌렀을 때의 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Sound.SetActive(false);  // Sound GameObject 비활성화
            Menu.SetActive(false);   // Menu GameObject 비활성화
            Debug.Log("Escape key pressed");
        }
    }
}
