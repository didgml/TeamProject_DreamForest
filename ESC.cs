using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
    public GameObject Sound;
    public GameObject Menu;

    private void Update()
    {
        // ESC Ű�� ������ ���� ó��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Sound.SetActive(false);  // Sound GameObject ��Ȱ��ȭ
            Menu.SetActive(false);   // Menu GameObject ��Ȱ��ȭ
            Debug.Log("Escape key pressed");
        }
    }
}
