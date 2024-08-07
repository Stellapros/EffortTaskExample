using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveStepSize = 1.0f;
    public int moveStepTreshold = 5;
    public bool trialRunning;

    public int upCounter = 0;
    public int downCounter = 0;
    public int leftCounter = 0;
    public int rightCounter = 0;

    void Update()
    {

        //if (trialRunning) {
        //    Debug.Log("Trial is running");
            // Check for key presses and update counters
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                upCounter++;

                if (upCounter >= moveStepTreshold)
                {
                    MoveCharacter(Vector2.up);
                    upCounter = 0;
                    downCounter = 0;
                    leftCounter = 0;
                    rightCounter = 0;
                    upCounter++;
                }

                Debug.Log("Up presses:" + upCounter);

            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                downCounter++;

                if (downCounter >= moveStepTreshold)
                {
                    MoveCharacter(Vector2.down);
                    upCounter = 0;
                    downCounter = 0;
                    leftCounter = 0;
                    rightCounter = 0;
                    downCounter++;
                }

                Debug.Log("Down presses:" + downCounter);

            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                leftCounter++;

                if (leftCounter >= moveStepTreshold)
                {
                    MoveCharacter(Vector2.left);
                    upCounter = 0;
                    downCounter = 0;
                    leftCounter = 0;
                    rightCounter = 0;
                    leftCounter++;
                }

                Debug.Log("Left presses:" + leftCounter);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rightCounter++;

                if (rightCounter >= moveStepTreshold)
                {
                    MoveCharacter(Vector2.right);
                    upCounter = 0;
                    downCounter = 0;
                    leftCounter = 0;
                    rightCounter = 0;
                    rightCounter++;
                }

                Debug.Log("Right presses:" + rightCounter);
            }
        //}
    }

    void MoveCharacter(Vector2 direction)
    {
        transform.position += (Vector3)direction * moveStepSize;
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(0, 0, 0);
    }

}
