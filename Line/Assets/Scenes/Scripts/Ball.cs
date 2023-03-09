using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    [SerializeField] MeshRenderer mesh;
    BallData data;
    public void InitData(BallData _data)
    {
        data = _data;
        mesh.material = GameData.Instance.materials[(int)data.ballType];
    }

}
public class BallData
{
    public BallType ballType;

    public BallData(BallType ballType)
    {
        this.ballType = ballType;
    }
}


