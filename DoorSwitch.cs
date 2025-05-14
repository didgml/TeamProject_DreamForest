using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSwitch : MonoBehaviour
{
    [SerializeField] private PadDoor[] doors; // ���� ����
    private bool isPressed = false; // ����ġ�� ���ȴ��� Ȯ���ϴ� ����
    public string TagColor; // �±װ�

    private AudioSource switchEff;
    public AudioClip switchClip;
    public AudioClip doorClip;

    private bool inTrigger = false; // �� ��ó�� �ִ��� Ȯ���ϴ� ����
    public GameObject nextScene;

    // ���丮 ���� ����
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
            // ����ġ�� ������ �� ���� ����, ���带 ���
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
            // ����ġ���� ����� ���� �ݰ�
            foreach (var door in doors)
            {
                door.SetOpen(false);
            }
            isPressed = false;
        }
    }

    void Update()
    {
        // ���丮 ���� üũ
        Story();

        if (inTrigger && Input.GetKeyDown(KeyCode.F)) // F Ű�� �� ����
        {
            if (Door.doorKey)
            {
                gameObject.SetActive(false); // ���� ��Ȱ��ȭ�Ͽ� �������
                nextScene.SetActive(true);   // ���� �� Ȱ��ȭ
                StartStory = true;
                Door.doorKey = false;        // ���踦 ����� �� ���� ��Ȱ��ȭ
            }
            else
            {
                Debug.Log("���谡 �ʿ��մϴ�."); // ���谡 ������ �޽��� ���
            }
        }
    }

    void Story()
    {
        if (IsCheck && StartStory)
        {
            SceneManager.LoadScene(cinemaName); // ���丮 ������� �̵�
        }
    }
}
