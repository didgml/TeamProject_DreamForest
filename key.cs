using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPlayerInTrigger = false;

    // 트리거 안에 들어왔을 때
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
        }
    }

    // 트리거 밖으로 나갔을 때
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
        }
    }

    void Update()
    {
        // 플레이어가 트리거 안에 있을 때 F 키를 눌러 키를 획득
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Door.doorKey = true;  // 문이 열릴 수 있도록 키 설정
            Destroy(gameObject);  // 키 오브젝트 삭제
        }
    }
}
