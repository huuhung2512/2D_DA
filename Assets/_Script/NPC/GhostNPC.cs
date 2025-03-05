using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GhostNPC : NPC
{
    [SerializeField] private GameObject chatBox;
    [SerializeField] private GameObject notification;
    [SerializeField] private string[] text;
    private Transform player;
    private bool canContunies = true;
    private int currentIndex = 0;
    private enum state { idle, appear, disappear };
    private Animator anim;
    private SpriteRenderer sprite;
    private TextMeshProUGUI chatText;
    [SerializeField] private float _timeLoadText = .05f;
    private IEnumerator _coroutine;

    [System.Serializable]
    public class TargetStory
    {
        public Transform Target;
        public int TextNumberToTransformCamera;
    }
    public List<TargetStory> _listTransform;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Color currentColor = sprite.color;
        currentColor.a = 0f; // Đặt alpha thành 0 để ẩn
        sprite.color = currentColor;
        chatText = chatBox.GetComponentInChildren<TextMeshProUGUI>();
        player = GameObject.FindObjectOfType<Player>().transform;
    }

    private new void Update()
    {
        // Ẩn _ChatBox khi không trong cuộc trò chuyện
        if (chatBox != null && !isPlayerInRange)
        {
            chatBox.SetActive(false);
            notification.SetActive(false);
        }
        if (isPlayerInRange && (Input.GetKeyDown(KeyCode.E) && canContunies))
        {
            //Dảm bảo dừng Coroutine DisplayText cũ
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = null;

            // Người chơi nhấn Space, mở hộp thoại chat
            if (currentIndex >= text.Length)
            {
                CameraController.Instance.Player = player;
                notification.SetActive(false);
                chatBox.SetActive(false);
                anim.SetInteger("State", (int)state.disappear);
            }
            else
            {
                bool conversationStarted = false; // Biến để kiểm tra xem liệu cuộc trò chuyện đã được bắt đầu chưa

                for (int i = 0; i < _listTransform.Count; i++)
                {
                    if (currentIndex == _listTransform[i].TextNumberToTransformCamera)
                    {
                        StartConversation();
                        if (_listTransform[i].Target != null)
                        {
                            CameraController.Instance.Player = _listTransform[i].Target;
                        }
                        conversationStarted = true; // Đánh dấu rằng cuộc trò chuyện đã được bắt đầu
                        break; // Thoát khỏi vòng lặp sau khi bắt đầu cuộc trò chuyện
                    }
                }

                if (!conversationStarted)
                {
                    StartConversation();
                }
            }
        }
    }

    protected override void StartConversation()
    {
        canContunies = false;
        notification.SetActive(false);
        chatBox.SetActive(true);
        chatText.text = "";
        _coroutine = DisplayText(text[currentIndex]);
        StartCoroutine(_coroutine);
        currentIndex++;
        Debug.Log("Ghost: Hello Bro!!!");
        Invoke("Show_Notification", 1f);
    }

    private IEnumerator DisplayText(string displayText)
    {
        for (int i = 1; i < displayText.Length; i += 2)
        {
            chatText.text += displayText[i - 1];
            chatText.text += displayText[i];
            yield return new WaitForSeconds(_timeLoadText);
        }
    }

    private void Show_Notification()
    {
        notification.SetActive(true);
        canContunies = true;
    }
    protected new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            notification.SetActive(true);
            Color currentColor = sprite.color;
            currentColor.a = 1f;
            sprite.color = currentColor;
            isPlayerInRange = true;
            anim.SetInteger("State", (int)state.appear);
        }
    }

    private new void OnTriggerExit2D(Collider2D other)
    {
        Color currentColor = sprite.color;
        currentColor.a = 0f;
        sprite.color = currentColor;

        notification.SetActive(false);
        chatBox.SetActive(false);
    }

    private void SetIdle()
    {
        anim.SetInteger("State", (int)state.idle);
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
