using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    [SerializeField] public List<Material> materials;
}
[System.Serializable]
public enum BallType
{
    Red,
    Green,
    Blue,
    Pink,
    Yellow
}
