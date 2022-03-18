using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPuzzlePiece : MonoBehaviour
{
    public SpriteRenderer sprite;
    public SlidingPuzzle Puzzle;

    public SlidingPuzzlePiece[] CanChangePiece;
    //자신의 주변 퍼즐 조각 (수동으로 넣어줌)

    public bool isBlank = false;
    // 이 퍼즐 조각이 비어있는 조각인지 (마찬가지로 초기 수동 설정)

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
            //완료된 상태에서 누르면 박스 열기
        }
        if (Puzzle.SelectedPiece == null)
        {
            if (isBlank) return; // 빈 칸은 선택 불가능
            
            Puzzle.SelectedPiece = this;
            sprite.color = Color.yellow;
            //아무것도 선택 안된상태면 선택
        }
        else if (Puzzle.SelectedPiece == this)
        {
            Puzzle.SelectedPiece = null;
            sprite.color = Color.white;
            //선택된것 또 선택하면 선택 취소
        }
        else
        // 다른 선택된 조각이 있는 상태에서 this를 눌렀다
        {

            foreach (SlidingPuzzlePiece piece in CanChangePiece)
            {
                if (isBlank && Puzzle.SelectedPiece == piece)
                {
                    Puzzle.ChangePiece(this);
                    return;
                    //주변 퍼즐 조각 누르면 변경
                }
            }

            // 선택을 잘못했을 시 -> 자동적으로 해제
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
        //스프라이트 이름과 오브젝트 이름 같으면 true 반환
    }
}
