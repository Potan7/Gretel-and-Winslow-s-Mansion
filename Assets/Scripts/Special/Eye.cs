using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public EyeManager EyeManager;
    
    public SpriteRenderer eyeImage;
    
    // 해당 눈이 답인지 판별
    public bool isAnswer = false;

    // open/close 판별. 정답 눈은 항상 이 값이 true
    public bool isOpened = false;

    // 눈 이미지
    public Sprite open, close;
    
    // 해당 눈의 변환 주기, 현재 시간
    private float cycle, time = 0.0f;
    
    void Start()
    {
        cycle = Random.Range(0.5f, 2f);
        eyeImage.sprite = open;
    }
    
    void Update()
    {
        if (!isAnswer)
        {
            time += Time.deltaTime;
            if (time > cycle)
            {
                // 정답이 아닌 눈이 시간이 지날 시 사이클 전환
                cycle = Random.Range(0.1f, 2f);
                SwapImage();
                time = 0.0f;
            }
        }
        else
        {
            if (!isOpened)
            {
                SwapImage();
            }
        }
    }

    void Interact()
    {
        EyeManager.Clicked(this);
    }

    void SwapImage()
    {
        // 스프라이트 바꿔주고 bool값 반전
        eyeImage.sprite = isOpened ? close : open;
        isOpened = !isOpened;
    }
}
