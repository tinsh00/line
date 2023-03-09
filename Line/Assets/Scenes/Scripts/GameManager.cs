using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager UIManager;

    [SerializeField] Transform container;
    [SerializeField] Ball ballPrefab;
    // Start is called before the first frame update
    void Start()
    {
        UIManager.UpdateYourScore(4444444);
        Ball ball = Instantiate(ballPrefab, container);
        ball.InitData(new BallData(BallType.Pink));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
