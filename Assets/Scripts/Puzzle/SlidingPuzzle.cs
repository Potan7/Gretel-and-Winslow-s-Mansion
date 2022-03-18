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
        // 자식 오브젝트들 전부 배열에 모음
    }

    public void ChangePiece(SlidingPuzzlePiece target)
    {
        // 스프라이트 교환
        SelectedPiece.sprite.color = Color.white;
        Sprite tmp;
        tmp = SelectedPiece.sprite.sprite;
        SelectedPiece.sprite.sprite = target.sprite.sprite;
        target.sprite.sprite = tmp;
        
        // 비어있는 표시 교환 (isBlank가 교환되면서 해당 조각 번호를 따라가도록)
        bool tmp2;
        tmp2 = SelectedPiece.isBlank;
        SelectedPiece.isBlank = target.isBlank;
        target.isBlank = tmp2;
        
        SelectedPiece = null;
        
        CheckIsComplete();
        //target과 현재 선택된 오브젝트의 스프라이트를 서로 변경
    }

    public void OpenBox()
    {
        this.gameObject.SetActive(false);
        //퍼즐 완료후 상자 열기
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
        //모든 조각을 살펴보며 맞는지 확인
        //맞는게 없다면 함수 취소

        isCompleted = true;
        anim.SetBool("Complete", true);
        SoundManager.Instance.PlaySound("SlidingPuzzleBoxOpen", true);
        //전부다 맞다면 애니메이션 재생, 소리 재생
    }
}
