using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPlayerInTrigger = false;

    // Ʈ���� �ȿ� ������ ��
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    // Ʈ���� ������ ������ ��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        // �÷��̾ Ʈ���� �ȿ� ���� �� F Ű�� ���� Ű�� ȹ��
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Door.doorKey = true;  // ���� ���� �� �ֵ��� Ű ����
            Destroy(gameObject);  // Ű ������Ʈ ����
        }
    }
}
