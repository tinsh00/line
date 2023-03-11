using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    [SerializeField] Ball currentBall;
    bool canHoldBall = true;
    int x, y;
    public void SetCanHoldBall(bool status) => canHoldBall = status;
    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, y);
    }
    public bool CanHoldBall() {
        return canHoldBall;
    } 
    public bool CanHoldBall(int x, int y) {
        if (this.x != x || this.y != y) return false;
        return CheckChangeStatus();
    }
    public Vector2 GetPosition()
	{
        return new Vector2(x, y);
	}
    public void SetCurrentBall(Ball _ball)
    {      
        currentBall = _ball;
        currentBall.SetPosition(x, y);
        canHoldBall = false;
    } 
    public Ball GetCurrnetBall() => currentBall;
    public void DestroyBall()
	{
        Destroy(currentBall.gameObject);
        canHoldBall = true;
    }
    public bool CheckChangeStatus()
	{
        return canHoldBall = currentBall == null;
	}
    public void ReleaseBall()
	{
        SetCanHoldBall(true);
        currentBall = null;
    }
}
