using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    bool canHoldBall = true;
    [SerializeField] int x, y;
    [SerializeField] BallHolder parentHolder;//node truoc
    [SerializeField]int cost; // gia tri khi ball toi noi nay
    [SerializeField] int status; // status =0:chua di qua lan nao, status = 1: da di qua 1 lan: status = 2: da xet het 4 dinh ke
    public void SetCanHoldBall(bool status) => canHoldBall = status;
    public void SetStatus(int _status)
	{
        status= _status;
    }
    public int CheckToInCreateCost(BallHolder parrentHolder)
	{
        int value = parrentHolder.GetCost()+1;
        print("value " + value);
        if (status == 0 )
		{
            SetStatus(1);
            cost = value;
            SetParentNode(parentHolder);
            return cost;
		}
        if(cost < value)
		{
            return cost;
        }
        SetParentNode(parentHolder);
        return cost = value;
    }
    public int GetCost() => cost;
    public void SetStartStatus()
    {
        if (canHoldBall)
        {
            cost = 0;
            status = 0;
        };
        parentHolder = null;
    }
    public void SetParentNode(BallHolder _parentHolder)
	{
        parentHolder = _parentHolder;
    }
    public BallHolder GetParentNode() => parentHolder;
    public bool HasParentNode() => !(parentHolder == null);
    public bool CanMoveOver()
	{
        return status !=2 && canHoldBall;        
	}
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
        return canHoldBall;
    }
    public Vector2 GetPosition()
	{
        return new Vector2(x, y);
	}
    public void SetHoldBall()
	{
        canHoldBall = false;
	}

    public void ReleaseBall()
	{
        SetCanHoldBall(true);
    }
}
