using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    [SerializeField] Ball currentBall;
    bool canHoldBall = false;

    public void SetCanHoldBall(bool status) => canHoldBall = status;
    public void SetPosition(int x, int y)
    {
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, y);
    }
}
