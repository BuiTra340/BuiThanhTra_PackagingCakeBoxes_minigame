using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    private Vector2 firstPosTouch;
    private Vector2 finalPosTouch;
    private RectTransform rectTransform;
    private bool canMove;
    private Vector2 newPos;
    [SerializeField] private Transform frameParent;
    [SerializeField] private int gridX;
    [SerializeField] private int gridY;
    private Frame frame;
    [SerializeField] private Transform giftBox;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        frame = findFrameClosest();
    }
    private void calculateAngle()
    {
        int offsetX = 0;
        int offsetY = 0;
        if (Mathf.Abs(finalPosTouch.x - firstPosTouch.x) != 0 && Mathf.Abs(finalPosTouch.y - firstPosTouch.y) != 0)
        {
            float angle = Mathf.Atan2(finalPosTouch.y - firstPosTouch.y, finalPosTouch.x - firstPosTouch.x) * Mathf.Rad2Deg;
            if (angle > -45 && angle <= 45)
            {
                for (int i = 0; i < gridX; i++)
                {
                    if (frame.frameRight == null || frame.frameRight.transform.childCount > 0)
                        continue;
                    if (giftBox != null && frame.frameRight.transform.position == giftBox.position)
                        continue;
                    frame = frame.frameRight;
                    offsetX += 200;
                }
                assignNewPosition(offsetX, offsetY);
            }
            else if (angle > 45 && angle <= 135)
            {
                for (int i = 0; i < gridY; i++)
                {
                    if (frame.frameUp == null || frame.frameUp.transform.childCount > 0)
                        continue;
                    if (giftBox != null && frame.frameUp.transform.position == giftBox.position)
                        continue;

                    frame = frame.frameUp;
                    offsetY += 200;
                }
                assignNewPosition(offsetX, offsetY);
            }
            else if (angle > 135 || angle <= -135)
            {
                for (int i = 0; i < gridX; i++)
                {
                    if (frame.frameLeft == null || frame.frameLeft.transform.childCount > 0)
                        continue;
                    if (giftBox != null && frame.frameLeft.transform.position == giftBox.position)
                        continue;

                    frame = frame.frameLeft;
                    offsetX -= 200;
                }
                assignNewPosition(offsetX, offsetY);
            }
            else
            {
                for (int i = 0; i < gridY; i++)
                {
                    if (frame.frameDown == null || frame.frameDown.transform.childCount > 0)
                        continue;
                    frame = frame.frameDown;
                    offsetY -= 200;
                }
                assignNewPosition(offsetX, offsetY);
            }
        }
    }

    private void assignNewPosition(int offsetX, int offsetY)
    {
        newPos = new Vector2(rectTransform.position.x + offsetX, rectTransform.position.y + offsetY);
        canMove = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPosTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            finalPosTouch = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            calculateAngle();
            finalPosTouch = Vector2.zero;
            firstPosTouch = Vector2.zero;
        }

        if (canMove)
        {
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, newPos, 2000 * Time.deltaTime);
            if (Vector2.Distance(rectTransform.position, newPos) < .1f)
            {
                if(transform.position == giftBox.position)
                    Destroy(gameObject);
                canMove = false;
            }
        }
        frame = findFrameClosest();
    }
    private Frame findFrameClosest()
    {
        Frame frameClosest = null;
        float distanceClosest = Mathf.Infinity;
        for (int i = 0; i < frameParent.childCount; i++)
        {
            float distanceToFrame = Vector2.Distance(transform.position, frameParent.GetChild(i).transform.position);
            if (distanceToFrame < distanceClosest)
            {
                distanceClosest = distanceToFrame;
                frameClosest = frameParent.GetChild(i).GetComponent<Frame>();
            }
        }
        return frameClosest;
    }
}
