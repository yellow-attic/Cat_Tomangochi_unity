using System.Collections;
using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private float frameDuration = 0.1f;

    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;

        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            StartCoroutine(CreateClickEffect());
        }
    }

    private IEnumerator CreateClickEffect()
    {
        for (int i = 0; i < Sprites.Length; i++)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().sprite = Sprites[i];
            yield return new WaitForSeconds(frameDuration);
        }
        GetComponent<SpriteRenderer>().enabled = false;
    }
}