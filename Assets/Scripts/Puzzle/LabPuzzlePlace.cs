using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabPuzzlePlace : MonoBehaviour
{
    LabPuzzle puzzle;
    GameObject Effect;

    public Sprite[] PotionEffectSprites;

    SpriteRenderer PotionEffect;

    public int ID;
    public bool hasEffect = false;
    public bool isRight = false;

    private void Start()
    {
        puzzle = FindObjectOfType<LabPuzzle>();
        PotionEffect = transform.GetChild(0).GetComponent<SpriteRenderer>();

        Effect = puzzle.bottleParent;
    }

    void Interact()
    {
        if (puzzle.selected == null) return;
        if (hasEffect) return;

        hasEffect = true;
        if (puzzle.selected.ID == ID) isRight = true;
        StartCoroutine(UsePotion());
    }

    IEnumerator UsePotion()
    {
        puzzle.bottleEvent[1].color = SetColor(true);

        yield return new WaitForSeconds(0.4f);

        puzzle.bottleEvent[1].color = SetColor(false);
        Effect.SetActive(false);
        PotionEffect.sprite = PotionEffectSprites[puzzle.selected.ID];
        PotionEffect.color = SetColor(true);
        OnMouseExit();
        puzzle.SelecetBottle(puzzle.selected);
        puzzle.CheckPotion();
    }

    public void EffectReset()
    {
        hasEffect = false;
        isRight = false;
        PotionEffect.color = SetColor(false);
    }

    private void OnMouseEnter()
        //마우스 들어옴
    {
        if (puzzle.selected == null) return;
        if (hasEffect) return;

        Effect.transform.position = transform.position;
        Effect.SetActive(true);
        puzzle.selected.gameObject.SetActive(false);
    }

    private void OnMouseExit()
        //마우스 나감
    {
        if (puzzle.selected == null) return;

        Effect.SetActive(false);
        puzzle.selected.gameObject.SetActive(true);
    }

    Color SetColor(bool OnOff)
    {
        int value = OnOff ? 1 : 0;
        return new Color(255, 255, 255, value);
    }
}
