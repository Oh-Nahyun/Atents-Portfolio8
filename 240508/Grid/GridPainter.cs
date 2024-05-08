using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridPainter : MonoBehaviour
{
    public GameObject linePrefab;
    public GameObject letterPrefab;
    const int gridLineCount = 11;

    private void Awake()
    {
        DrawGridLines();
        DrawGridLetter();
    }

    void DrawGridLines()
    {
        // 세로선 그리기
        for (int i = 0; i < gridLineCount; i++)
        {
            GameObject line = Instantiate(linePrefab, transform); ///// 마지막에 transform을 넣는 이유 : 부모 설정
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(i, 0, 1));
            lineRenderer.SetPosition(1, new Vector3(i, 0, 1 - gridLineCount));
        }

        // 가로선 그리기
        for (int i = 0; i < gridLineCount; i++)
        {
            GameObject line = Instantiate(linePrefab, transform);
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, new Vector3(-1, 0, -i));
            lineRenderer.SetPosition(1, new Vector3(gridLineCount - 1, 0, -i));
        }
    }

    void DrawGridLetter()
    {
        // 가로로 알파벳 찍기
        for (int i = 1; i < gridLineCount; i++)
        {
            GameObject letter = Instantiate(letterPrefab, transform);
            letter.transform.position = new Vector3(i - 0.5f, 0, 0.5f);
            TextMeshPro text = letter.GetComponent<TextMeshPro>();
            char alphabet = (char)('A' + i - 1);
            text.text = alphabet.ToString();
        }

        // 세로로 숫자 찍기
        for (int i = 1; i < gridLineCount; i++)
        {
            GameObject letter = Instantiate(letterPrefab, transform);
            letter.transform.position = new Vector3(-0.5f, 0, 0.5f - i);
            TextMeshPro text = letter.GetComponent<TextMeshPro>();
            text.text = i.ToString();
            if (i > 9)
            {
                text.fontSize = 8;
            }
        }
    }
}

/// 실습_240425
/// GridPainter 구현하기

/* // 내가 작성해본 코드
LineRenderer lineRenderer;

private void Awake()
{
    lineRenderer = GetComponent<LineRenderer>();
}

private void Start()
{
    GetLine();
}

void GetLine()
{
    for (int x = 0; x < 11; x++)
    {
        //lineRenderer.SetPosition(x, new Vector3());
        line.transform.position = new Vector3(x, 0.0f, 0.0f);
        line.transform.Rotate(Vector3.zero);
        Instantiate(line);
        ResetLine();
    }

    for (int z = 0; z < 11; z++)
    {
        line.transform.position = new Vector3(0.0f, 0.0f, z);
        line.transform.Rotate(0.0f, 90.0f, 0.0f);
        Instantiate(line);
        ResetLine();
    }
}

void ResetLine()
{
    line.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    line.transform.Rotate(Vector3.zero);
}

void GetLetter()
{

}
*/
