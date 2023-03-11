using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BallType type;
    public BallHolder currentBallHolder;
    public BallHolder fromBallHolder;
    public void InitData(BallType _type)
    {
        type = _type;
        SetColor(type);
    }
    public void SetFromBallHolder(BallHolder _holder)
	{
        fromBallHolder = _holder;
    }
    public BallHolder GetFromBallHolder() => fromBallHolder;
    public void SetCurrnetBallHolder(BallHolder _holder)
    {
        currentBallHolder = _holder;
    }
    public BallHolder GetCurrnetBallHolder() => currentBallHolder;
    public void SetColor(BallType type)
	{
        mesh.material = GameData.Instance.materials[(int)type];
    }
    public Material GetMaterial() => mesh.material;
    public void SetPosition(int x,int y)
	{
        gameObject.transform.position = new Vector3(x, gameObject.transform.position.y, y);
	}
    public BallType GetBallType() => type;
    public void MoveBallFrom(BallHolder currentBallHolder, BallHolder fromBallHolder = null)
	{
        SetCurrnetBallHolder(currentBallHolder);
        currentBallHolder.SetCurrentBall(this);
        if (!fromBallHolder) return;
        SetFromBallHolder(fromBallHolder);
        fromBallHolder.ReleaseBall();
    }
}



