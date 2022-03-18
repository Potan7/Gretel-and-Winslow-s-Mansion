using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzle : MonoBehaviour
{
    public bool isCompleted = false;

    SlidingPuzzlePiece[] puzzlepieces;
    Animator anim;

    public SlidingPuzzlePiece SelectedPiece;

    private void Start()
    {
        anim = GetComponent<Animator>();

        puzzlepieces = new SlidingPuzzlePiece[this.transform.childCount];

        for (int i = 0; i < this.transform.childCount; i++)
        {
            puzzlepieces[i] = this.transform.GetChild(i).GetComponent<SlidingPuzzlePiece>();
        }
        // �ڽ� ������Ʈ�� ���� �迭�� ����
    }

    public void ChangePiece(SlidingPuzzlePiece target)
    {
        // ��������Ʈ ��ȯ
        SelectedPiece.sprite.color = Color.white;
        Sprite tmp;
        tmp = SelectedPiece.sprite.sprite;
        SelectedPiece.sprite.sprite = target.sprite.sprite;
        target.sprite.sprite = tmp;
        
        // ����ִ� ǥ�� ��ȯ (isBlank�� ��ȯ�Ǹ鼭 �ش� ���� ��ȣ�� ���󰡵���)
        bool tmp2;
        tmp2 = SelectedPiece.isBlank;
        SelectedPiece.isBlank = target.isBlank;
        target.isBlank = tmp2;
        
        SelectedPiece = null;
        
        CheckIsComplete();
        //target�� ���� ���õ� ������Ʈ�� ��������Ʈ�� ���� ����
    }

    public void OpenBox()
    {
        this.gameObject.SetActive(false);
        //���� �Ϸ��� ���� ����
    }

    void CheckIsComplete()
    {
        foreach (SlidingPuzzlePiece piece in puzzlepieces)
        {
            if (!piece.IsCorrect())
            {
                return;
            }
        }
        //��� ������ ���캸�� �´��� Ȯ��
        //�´°� ���ٸ� �Լ� ���

        isCompleted = true;
        anim.SetBool("Complete", true);
        SoundManager.Instance.PlaySound("SlidingPuzzleBoxOpen", true);
        //���δ� �´ٸ� �ִϸ��̼� ���, �Ҹ� ���
    }
}
