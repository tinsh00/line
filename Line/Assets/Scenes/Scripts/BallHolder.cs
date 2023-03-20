using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHolder : MonoBehaviour
{
    [SerializeField]Ball currentBall;
    [SerializeField] ParticleSystem eff;
    [SerializeField] int x, y;
    [SerializeField] BallHolder parentHolder;//node truoc
    [SerializeField]int cost; // gia tri khi ball toi noi nay
    [SerializeField] int status; // status =0:chua di qua lan nao, status = 1: da di qua 1 lan: status = 2: da xet het 4 dinh ke
    [SerializeField] int amountTurn=0;
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
    public int CheckToInCreateCost(BallHolder parrentHolder,Vector2 currentDir)
	{
        int value = parrentHolder.GetCost() + 1;

        int parrentAmountTurn = parrentHolder.GetAmountOfTurn();
        Vector2 parrentFromDir = parrentHolder.GetFromDir();
        Vector2 parrentPosition = parrentHolder.GetPosition();
        if (status == 0)//chua di qua lan nao => gan luon
        {
            
            if (parrentFromDir == Vector2.zero && parrentAmountTurn == 0)
            {
                fromDir = currentDir;
                amountTurn = 0;
            }
            fromDir = GetPosition() - parrentPosition;
            amountTurn = fromDir == parrentFromDir ? parrentAmountTurn : parrentAmountTurn + 1;
            SetStatus(1);
            cost = value;
            SetParentNode(parrentHolder);
            return cost;
        }
        //da di qua => da co cost va amountOfTurn
        if (amountTurn < parrentAmountTurn)
        {
            return cost;
        }
        fromDir = GetPosition() - parrentPosition;
        amountTurn = fromDir == parrentFromDir ? parrentAmountTurn : parrentAmountTurn + 1;
        print("amount Turn " + amountTurn);
        if (amountTurn < 4)
        {
            SetParentNode(parrentHolder);
            return cost = value;
        }
        else return -1;

    }
    public int GetCost() => cost;
    public int GetAmountOfTurn() => amountTurn;
    public void SetStartStatus()
    {
        cost = 0;
        status = 0;
        amountTurn = 0;
        fromDir = Vector2.zero;
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
        eff.Play();
        Destroy(currentBall.gameObject);
	}

    public int CheckToIncreateAmountOfTurn(BallHolder fromBallHolder,Vector2 currentDir)//Vector2 currentDir,Vector2 parrentFromDir, Vector2 parrentPosition,int parrentAmount)
    {
        int parrentAmountTurn = fromBallHolder.GetAmountOfTurn();
        Vector2 parrentFromDir = fromBallHolder.GetFromDir();
        Vector2 parrentPosition = fromBallHolder.GetPosition();
		if (status == 0)
		{
            if (parrentFromDir == Vector2.zero && parrentAmountTurn == 0)
            {
                fromDir = currentDir;
                return amountTurn = 0;              
            }
        }
		if (amountTurn < parrentAmountTurn)
		{
            return amountTurn;
		}
        fromDir = GetPosition() - parrentPosition;
        amountTurn = fromDir == parrentFromDir ? parrentAmountTurn : parrentAmountTurn + 1;       
        return amountTurn;
    }
}
