using System;
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
    List<BallHolder> listBallHolder;
    private int[,] holderArray;
    [SerializeField] Transform ballContainer;
    [SerializeField] Ball ballPrefab;
    [SerializeField] List<Ball> listBallPrediction = new List<Ball>();
    List<BallType> listBallTypePrediction = new List<BallType>();
    System.Random rd = new System.Random();
    bool canSpawn = true;


    [Header("gameplay")]
    public bool isChoose = false;
    Ball currentBall;
    // Start is called before the first frame update
    void Start()
    {
        holderArray = new int[width,height];
        listBallHolder = new List<BallHolder>();
        CreateMap();
        UIManager.UpdateYourScore(0);
        Predition3TypeBall();
    }

    void CreateMap()
    {
        for(int i = 0; i < holderArray.GetLength(0); i++)
        {
            for(int j = 0; j < holderArray.GetLength(1); j++)
            {
                BallHolder ballHolder = Instantiate(holderPrefab, holderTrans);
                ballHolder.SetPosition(i, j);
                listBallHolder.Add(ballHolder);
            }
        }
    }
    public BallHolder GetBallHolderByXY(Vector2 xy)
	{
        foreach (BallHolder item in listBallHolder)
		{
            if (item.GetPosition() == xy) return item;
		}
        return null;
    }
    public void CreateBall(Vector2 xy, BallType type)
	{
        Ball ball = Instantiate(ballPrefab, ballContainer);
        BallHolder ballHolder = GetBallHolderByXY(xy);
        ball.InitData(type);
        ball.MoveBallFrom(ballHolder);
    }

    public bool RandomBall()
	{
        int value = 0;
        List<Vector2> listxy = new List<Vector2>();

        foreach(BallHolder holder in listBallHolder)
		{
            if (holder.CanHoldBall()) 
            {
                value++;
                Vector2 position = holder.GetPosition();
                listxy.Add(position);
            };
		}
        print(value);
        if (value < 3) return false;
        for(int i = 0; i < 3; i++)
		{
            int index = rd.Next(0, listxy.Count - 1);
            CreateBall(listxy[index], listBallTypePrediction[i]);
            listxy.RemoveAt(index);
		}
        return true;
    }
    public bool GetCanSpawn() => canSpawn;
	public void testRandomBall()
	{
		if(canSpawn)
		{
            canSpawn = RandomBall();
            Predition3TypeBall();
        }
        else
		{
            //game Over
		}

	}
    public void Predition3TypeBall()
    {
            listBallTypePrediction.Clear();

        for (int i = 0; i < 3; i++)
        {
            int type = rd.Next(0, Enum.GetNames(typeof(BallType)).Length);
            listBallTypePrediction.Add((BallType)type);
            listBallPrediction[i].SetColor((BallType)type);
        }
    }
    //===================================Update==================================
    private void Update()
    {
		if (isChoose == false && Input.GetMouseButton(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100f))
			{
				print(hit.transform.name);
				if (hit.collider.tag == "Ball")
				{
					currentBall = hit.transform.GetComponent<Ball>();
					isChoose = true;
				}

			}
		}
		if (Input.GetMouseButtonUp(0))
		{
            
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 100f))
			{
				if (hit.collider.tag == "BallHolder")
				{
					print(hit.transform.name);
					if (isChoose)
					{
						BallHolder holder = hit.transform.GetComponent<BallHolder>();
						if (!holder.CanHoldBall()) return;
						currentBall.MoveBallFrom(holder, currentBall.currentBallHolder);
					}
				}
			}
            isChoose = false;
        }

	}
}

