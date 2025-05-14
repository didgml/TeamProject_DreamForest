using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private PadDoor[] doors; // 열릴 문들
    private bool isPressed = false; // 스위치가 눌렸는지 확인하는 변수
    public string TagColor; // 태그값

    private AudioSource switchEff;
    public AudioClip switchClip;
    public AudioClip doorClip;

    private bool inTrigger = false; // 문 근처에 있는지 확인하는 변수
    public GameObject nextScene;

    // 스토리 관련 변수
    public bool IsCheck;
    bool StartStory = false;
    public string cinemaName;

    void Start()
    {
        switchEff = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagColor) && !isPressed)
        {
            // 스위치가 눌렸을 때 문을 열고, 사운드를 재생
            foreach (var door in doors)
            {
                door.SetOpen(true);
            }
            switchEff.PlayOneShot(switchClip);
            switchEff.PlayOneShot(doorClip);
            isPressed = true;
            StartStory = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagColor) && isPressed)
        {
            // 스위치에서 벗어나면 문을 닫고
            foreach (var door in doors)
            {
                door.SetOpen(false);
            }
            isPressed = false;
        }
    }

    void Update()
    {
        // 스토리 진행 체크
        Story();

        if (inTrigger && Input.GetKeyDown(KeyCode.F)) // F 키로 문 열기
        {
            if (Door.doorKey)
            {
                gameObject.SetActive(false); // 문을 비활성화하여 사라지게
                nextScene.SetActive(true);   // 다음 씬 활성화
                StartStory = true;
                Door.doorKey = false;        // 열쇠를 사용한 후 열쇠 비활성화
            }
            else
            {
                Debug.Log("열쇠가 필요합니다."); // 열쇠가 없으면 메시지 출력
            }
        }
    }

    void Story()
    {
        if (IsCheck && StartStory)
        {
            SceneManager.LoadScene(cinemaName); // 스토리 장면으로 이동
        }
    }
}
