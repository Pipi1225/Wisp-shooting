using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public int dotsNumber;
    [SerializeField] GameObject dotsParent;
    [SerializeField] GameObject dotsPerfab;
    [SerializeField] float dotSpacing;
    [SerializeField] [Range(0.1f, 0.4f)] float dotMinScale;
    [SerializeField] [Range(0.4f, 1f)] float dotMaxScale;
    [SerializeField] Vector3 ballPos;

    Transform[] dotsList;

    Vector2 pos;
    float timeStamp;

    public int rangeNumber;
    [SerializeField] float radius;
    [SerializeField] GameObject rangeParent;
    [SerializeField] GameObject rangePerfab;

    Transform[] rangeList;

    private void Start()
    {
        Hide();
        prepareDots();
    }

    void prepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotsPerfab.transform.localScale = Vector3.one * dotMaxScale * 10;

        float scale = dotMaxScale;
        float scalefactor = scale / dotsNumber;

        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotsPerfab, null).transform;
            dotsList[i].parent = dotsParent.transform;

            dotsList[i].localScale = Vector3.one * scale;

            if (scale > dotMinScale)
            {
                scale -= scalefactor;
            }
        }

        rangeList = new Transform[rangeNumber];
        rangePerfab.transform.localScale = Vector3.one * 0.25f;

        for (int i = 0; i < rangeNumber; i++)
        {
            float angle = i * Mathf.PI * 2 / rangeNumber;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector2 posTemp = transform.position + new Vector3(x, y, 0);

            rangeList[i] = Instantiate(rangePerfab, posTemp, transform.rotation).transform;
            rangeList[i].parent = rangeParent.transform;
        }
    }

    public void UpdateDots(Vector2 forceApplied)
    {
        timeStamp = dotSpacing;

        for (int i = 0; i < dotsNumber; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show(Vector3 tempPos)
    {
        ballPos = tempPos;

        dotsParent.SetActive(true);

        rangeParent.transform.position = tempPos;
        rangeParent.SetActive(true);
    }

    public void Hide()
    {
        dotsParent.SetActive(false);
        rangeParent.SetActive(false);
    }

    public void CollisionHide(Vector3 tempPos)
    {
        if (tempPos == ballPos)
        {
            dotsParent.SetActive(false);
            rangeParent.SetActive(false);
        }
    }
}
