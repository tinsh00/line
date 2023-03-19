using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    [SerializeField] BallType type;

    public void InitData(BallType _type)
    {
        type = _type;
        SetColor(type);
    }

    public void SetColor(BallType type)
	{
        mesh.material = GameData.Instance.materials[(int)type];
    }
    public Material GetMaterial() => mesh.material;
    public void SetPosition(Vector2 position)
	{
        gameObject.transform.position = new Vector3(position.x, gameObject.transform.position.y, position.y);
	}
    public Vector2 GetPosition() => new Vector2(gameObject.transform.position.x, gameObject.transform.position.z);
    public BallType GetBallType() => type;
    public void MoveBallFrom(BallHolder currentBallHolder, BallHolder fromBallHolder = null)
	{

        currentBallHolder.SetCurrentBall(this);
        SetPosition(currentBallHolder.GetPosition());
        if (!fromBallHolder) return;
        fromBallHolder.ReleaseBall();
    }
}



