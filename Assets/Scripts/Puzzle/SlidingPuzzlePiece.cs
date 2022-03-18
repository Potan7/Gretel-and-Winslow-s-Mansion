using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzlePiece : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SlidingPuzzle Puzzle;

    public SlidingPuzzlePiece[] CanChangePiece;
    //�ڽ��� �ֺ� ���� ���� (�������� �־���)

    public bool isBlank = false;
    // �� ���� ������ ����ִ� �������� (���������� �ʱ� ���� ����)

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Interact()
    {
        if (Puzzle.isCompleted)
        {
            Puzzle.OpenBox();
            return;
            //�Ϸ�� ���¿��� ������ �ڽ� ����
        }
        if (Puzzle.SelectedPiece == null)
        {
            if (isBlank) return; // �� ĭ�� ���� �Ұ���
            
            Puzzle.SelectedPiece = this;
            sprite.color = Color.yellow;
            //�ƹ��͵� ���� �ȵȻ��¸� ����
        }
        else if (Puzzle.SelectedPiece == this)
        {
            Puzzle.SelectedPiece = null;
            sprite.color = Color.white;
            //���õȰ� �� �����ϸ� ���� ���
        }
        else
        // �ٸ� ���õ� ������ �ִ� ���¿��� this�� ������
        {

            foreach (SlidingPuzzlePiece piece in CanChangePiece)
            {
                if (isBlank && Puzzle.SelectedPiece == piece)
                {
                    Puzzle.ChangePiece(this);
                    return;
                    //�ֺ� ���� ���� ������ ����
                }
            }

            // ������ �߸����� �� -> �ڵ������� ����
            Puzzle.SelectedPiece.sprite.color = Color.white;
            Puzzle.SelectedPiece = null;

        }
    }

    public bool IsCorrect()
    {
        if (sprite.sprite.name == this.gameObject.name)
            return true;
        else
            return false;
        //��������Ʈ �̸��� ������Ʈ �̸� ������ true ��ȯ
    }
}
