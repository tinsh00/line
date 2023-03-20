using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    [SerializeField]Ball currentBall;
    [SerializeField] int x, y;
    [SerializeField] BallHolder parentHolder;//node truoc
    [SerializeField]int cost; // gia tri khi ball toi noi nay
    [SerializeField] int status; // status =0:chua di qua lan nao, status = 1: da di qua 1 lan: status = 2: da xet het 4 dinh ke
    [SerializeField] int amountTurn;
    [SerializeField] Vector2 fromDir = Vector2.zero;
    public Vector2 GetFromDir() => fromDir;
    public void SetCurrentBall(Ball _ball)
	{
        currentBall = _ball;
    }
    public Ball GetCurrentBall() => currentBall;
    public void SetStatus(int _status)
	{
        status= _status;
    }
    public int CheckToInCreateCost(BallHolder parrentHolder)
	{
        int value = parrentHolder.GetCost()+1;
        if (status == 0 )
		{
            SetStatus(1);
            cost = value;
            SetParentNode(parrentHolder);
            return cost;
		}
        if(cost < value)
		{
            return cost;
        }
        SetParentNode(parrentHolder);
        return cost = value;
    }
    public int GetCost() => cost;
    public int GetAmountOfTurn() => amountTurn;
    public void SetStartStatus()
    {
        if (CanHoldBall())
        {
            cost = 0;
            status = 0;
            amountTurn = 0;
            fromDir = Vector2.zero;
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
        return status !=2 && CanHoldBall();        
	}
    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, y);
    }
    public bool CanHoldBall() {
        return currentBall==null;
    } 
    public bool CanHoldBall(int x, int y) {
        if (this.x != x || this.y != y) return false;
        return CanHoldBall();
    }
    public Vector2 GetPosition()
	{
        return new Vector2(x, y);
	}


    public void ReleaseBall()
	{
        currentBall = null;
    }
    public void DestroyBall()
	{
        Destroy(currentBall.gameObject);
	}

    public bool CheckToIncreateAmountOfTurn(Vector2 currentDir,Vector2 parrentDir, Vector2 parrentPosition,int parrentAmount)
    {
        //print("currentDir " + currentDir);
        //print("parrentDir " + parrentDir);
        //print("parrentPosition " + parrentPosition);
        print("parrentAmount " + parrentAmount);
        if(fromDir==Vector2.zero && amountTurn==0)
        {
            if (parrentDir != null)
            {
                fromDir = parrentDir;
                amountTurn = parrentAmount;
            }
            else
            {
                fromDir = currentDir;
                amountTurn = 1;
            }
            return amountTurn<=4;
        }
        if (amountTurn < parrentAmount - 1) return false;
        fromDir = GetPosition() - parrentPosition;
        print("from dir " + fromDir);
        print("parrentDir " + parrentDir);
        amountTurn = fromDir == parrentDir ? parrentAmount : parrentAmount + 1;
        return amountTurn <=4;
    }
}
