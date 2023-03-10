using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int width=10;
    private int height=10;
    [SerializeField] UIManager UIManager;

    [SerializeField] Transform holderTrans;
    [SerializeField] BallHolder holderPrefab;
    private int[,] holderArray;
    [SerializeField] Transform container;
    [SerializeField] Ball ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        holderArray = new int[width,height];
        CreateMap();
        UIManager.UpdateYourScore(0);
    }

    void CreateMap()
    {
        for(int i = 0; i < holderArray.GetLength(0); i++)
        {
            for(int j = 0; j < holderArray.GetLength(1); j++)
            {
                BallHolder ballHolder = Instantiate(holderPrefab, holderTrans);
                ballHolder.SetPosition(i, j);
            }
        }
    }
}
