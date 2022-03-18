using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public EyeManager EyeManager;
    
    public SpriteRenderer eyeImage;
    
    // �ش� ���� ������ �Ǻ�
    public bool isAnswer = false;

    // open/close �Ǻ�. ���� ���� �׻� �� ���� true
    public bool isOpened = false;

    // �� �̹���
    public Sprite open, close;
    
    // �ش� ���� ��ȯ �ֱ�, ���� �ð�
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
                // ������ �ƴ� ���� �ð��� ���� �� ����Ŭ ��ȯ
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
        // ��������Ʈ �ٲ��ְ� bool�� ����
        eyeImage.sprite = isOpened ? close : open;
        isOpened = !isOpened;
    }
}
