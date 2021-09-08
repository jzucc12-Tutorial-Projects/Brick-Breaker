using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] float screenWidth = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPlay();
    }

    private void PlayerPlay()
    {
        float mousePosInUnits = Input.mousePosition.x / Screen.width * screenWidth;
        mousePosInUnits = Mathf.Clamp(GetXPos(mousePosInUnits), minX, maxX);
        Vector2 paddlePos = new Vector2(mousePosInUnits, transform.position.y);
        transform.position = paddlePos;
    }

    private float GetXPos(float mousePosition)
    {
        if(FindObjectOfType<GameStatus>().IsAutoPlayEnable())
        {
            return FindObjectOfType<Ball>().transform.position.x;
        }
        else
        {
            return mousePosition;
        }
    }
}
