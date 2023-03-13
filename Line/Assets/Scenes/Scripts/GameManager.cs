using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int width = 10;
    private int height = 10;
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
    [SerializeField] LineController lineController;
    List<Vector3> listPointLine;
    BallHolder currentHolder;
    List<Vector2> listDirection = new List<Vector2>() { new Vector2(1, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, -1) };
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
    public int GetMinIValue(int value1, int value2)
	{
        if (value1 <= value2) return value1;
        return value2;
	}
    BallHolder FindBallHolderByVectorPosition(Vector2 vector)
	{
        return listBallHolder.Find(x => x.GetPosition() == vector);
	}
    int BFS(Vector2 from, Vector2 to)
	{
        print(from + " " + to);
        if (to == from) return 0;
        int cost = 0;
        Queue<BallHolder> qList = new Queue<BallHolder>();
        //setup map
        foreach (BallHolder item in listBallHolder)
		{
            item.SetStartStatus();
		}
        //start at vector from
        BallHolder holder = FindBallHolderByVectorPosition(from);
        holder.SetStatus(1);
        //add in queue
        qList.Enqueue(holder);

        while (!(qList.Count == 0))
		{ 
            BallHolder frontHolder = qList.Dequeue();
            //tim dinh ke tiep : add 4 driction
            foreach(Vector2 item in listDirection)
			{
                Vector2 newVector = frontHolder.GetPosition() + item;
                BallHolder newBallHolder = FindBallHolderByVectorPosition(newVector);

                if (newBallHolder != null && newBallHolder.CanMoveOver()&&newBallHolder.GetParentNode()!=frontHolder)
				{
                    int newCost = newBallHolder.CheckToInCreateCost(frontHolder);

                    if (newVector == to)
                    {
                        if (cost == 0)
                            cost = newCost;
                        else
                            cost = GetMinIValue(cost, newCost);
                    }
					if (cost==0 || newCost <= cost)
					{
                        qList.Enqueue(newBallHolder);
					}
                }			
            }
            frontHolder.SetStatus(2);//set da di qua 4 diem
        }
		if (cost != 0)//co duong di
		{
			print("===================co duong di===============");

			listPointLine = new List<Vector3>();
			BallHolder pathding = FindBallHolderByVectorPosition(to);

            do
			{
				Vector2 position = pathding.GetPosition();
				listPointLine.Add(new Vector3(position.x, 0, position.y));
				pathding = pathding.GetParentNode();
			}
			while (pathding.GetParentNode() != null);
            Vector2 frontPosition = pathding.GetPosition();
            listPointLine.Add(new Vector3(frontPosition.x, 0, frontPosition.y));
        }
        lineController.SetupLine(listPointLine);
		return cost;
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
        if(isChoose == true && Input.GetMouseButton(0))
		{
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.tag == "BallHolder")
                {
                    BallHolder holder = hit.transform.GetComponent<BallHolder>();
                    if (currentHolder == holder) return;
                    currentHolder = holder;
                    if (!currentHolder.CanHoldBall()) return;
                    BFS(currentBall.currentBallHolder.GetPosition(), currentHolder.GetPosition());
                    
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
					if (isChoose)
					{
						BallHolder holder = hit.transform.GetComponent<BallHolder>();
						if (!holder.CanHoldBall()) return;
                        print("co duong di khong : ");
                        currentBall.MoveBallFrom(holder, currentBall.currentBallHolder);
                        lineController.ResetLine();
					}
				}
			}
            isChoose = false;
        }

	}
}

