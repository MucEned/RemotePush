using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Debug
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
    const int NODES_IN_ROW = 9;
    const int SUPER_BIG_INT = 999;
    public Node[,] nodes = new Node[NODES_IN_ROW,NODES_IN_ROW];
    public NormalBall selectingBall = null;
    public Node target = null;
    public static Board mainBoard;
    private int maxBallCanSpawn = 3;

    private bool isClassic = true;

    public GameObject nextButton;
    public GameObject sceneChanger;
    // Start is called before the first frame update
    void Awake()
    {
        mainBoard = this;
    }
    void Start()
    {
        initNodes();
        isClassic = (PlayerPrefs.GetString("GameMode") == "Classic");
        SetSpawnQueue();
        SpawnBalls();
        SetSpawnQueue();
    }
    void Loose()
    {
        DataController.datacontroller.CheckScore();
        //Debug
        sceneChanger.GetComponent<MainMenu>().LoadSceneIndex(2);
    }
    public void SetSpawnQueue()
    {
        int looseConditionCount = 0;
        Color[] randCSet = GetRandom3Colors();
        for(int i = 0 ; i < 3; i++)
        {
            Node randomNode = ChooseRandomIdleNode();
            if(randomNode)
            {
                randomNode.status = Node.STATUS.WillSpawn;
                randomNode.SetNextSpawnBall(randCSet[i]);
                looseConditionCount++;
            }
        }
        // maxBallCanSpawn = looseConditionCount;
        if(looseConditionCount == 0)
        {
            Loose();
        }

    }
    private Color[] GetRandom3Colors()
    {
        Color[] res = new Color[3];
        for(int i = 0; i < 3; i++)
        {
            int randC = (int)UnityEngine.Random.Range(0f, GameController.gamecontroller.maxNumberOfColor - 0.001f);
            res[i] = ColorDefine.ColorSet[randC];
        }
        return res;
    }
    public void SpawnBalls()
    {
        int spawnCount = 0;
        Node[] spawnNodes = new Node[0];
        foreach(Node node in nodes)
        {
            if(node.status == Node.STATUS.WillSpawn)
            {
                Array.Resize(ref spawnNodes, spawnNodes.Length + 1);
                spawnNodes[spawnNodes.Length - 1] = node;
                node.SpawnBall();
                spawnCount ++;
            }
            node.SetToDefaultSign();
        }
        if (spawnCount < 3)
        {
            Node randomSubNode = ChooseRandomIdleNode();
            if (!randomSubNode)
            {
                Loose();
                return;
            }
            randomSubNode.status = Node.STATUS.WillSpawn;
            randomSubNode.SetNextSpawnBall(GetRandom3Colors()[0]);
            Array.Resize(ref spawnNodes, spawnNodes.Length + 1);
            spawnNodes[spawnNodes.Length - 1] = randomSubNode;
            randomSubNode.SpawnBall();
        }
        foreach(Node node in spawnNodes)
        {
            GameController.gamecontroller.CheckScore(node);
        }

        GameController.gamecontroller.NextTurn();
    }
    private Node ChooseRandomIdleNode()
    {
        Node[] idleNodes = new Node[0];
        foreach(Node node in nodes)
        {
            if(node.status == Node.STATUS.Idle)
            {
                Array.Resize(ref idleNodes, idleNodes.Length + 1);
                idleNodes[idleNodes.Length - 1] = node;
            }
        }
        int randomIndex;
        if(idleNodes.Length != 0)
        {
            randomIndex = (int)Mathf.Floor(UnityEngine.Random.Range(0.0f, (float)idleNodes.Length - 0.001f));
            return idleNodes[randomIndex];
        }
        else return null;
    }
    void RouteForBall()
    {
        Node selectingStand = selectingBall.GetMyStand();
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j].costToSelectedBall = SUPER_BIG_INT;
                nodes[i,j].isAcceptByRouter = false;
            }
        }

        selectingStand.costToSelectedBall = 0;    

        Node currentNode = null;
        for(int i = 0; i < NODES_IN_ROW * NODES_IN_ROW - 1; i++)
        {
            currentNode = GetNextNodeToCheck();
            CheckAroundNodes(currentNode);
        }
    }
    private Node GetNextNodeToCheck()
    {
        Node cheapestNode = null;
        int cheapestCost = SUPER_BIG_INT * SUPER_BIG_INT;

        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                if (!nodes[i,j].isAcceptByRouter && nodes[i,j].costToSelectedBall < cheapestCost)
                {
                    cheapestNode = nodes[i,j];
                    cheapestCost = nodes[i,j].costToSelectedBall;
                }
            }
        }
        cheapestNode.isAcceptByRouter = true;
        return cheapestNode;
    }
    private void CostCalculate(Node node, Node nextNode)
    {
        int newCostToSelectingBall = node.costToSelectedBall + CostToNextNode(nextNode);

        if(nextNode.costToSelectedBall > newCostToSelectingBall)
        {
            nextNode.costToSelectedBall = newCostToSelectingBall;
            nextNode.previousNode = node;
        }
            
    }
    private void CheckAroundNodes(Node node)
    {
        int node_x = (int)node.GetMyPosition().x;
        int node_y = (int)node.GetMyPosition().y;

        if (node_x > 0) //có node trên
        {
            CostCalculate(node, nodes[node_x - 1, node_y]);
        }
        if (node_x < NODES_IN_ROW -1) //có node dưới
        {
            CostCalculate(node, nodes[node_x + 1, node_y]);
        }
        if (node_y > 0) //có node trái
        {
            CostCalculate(node, nodes[node_x, node_y - 1]);
        }
        if (node_y < NODES_IN_ROW -1) //có node phải
        {
            CostCalculate(node, nodes[node_x, node_y + 1]);
        }
    }
    private int CostToNextNode(Node thisNode)
    {
        int costToNextNode;
        if(thisNode.status == Node.STATUS.Holding)
            costToNextNode = SUPER_BIG_INT;
        else
            costToNextNode = 1;
        return costToNextNode;
    }
    public void SetSelectingBall(NormalBall newBall)
    {
        UnselectBall();
        selectingBall = newBall;
        selectingBall.SetMeAsSelectedBall();
        RouteForBall();
    }

    public void SetTarget(Node newTarget)
    {
        target = newTarget;
        MoveOrUnselect();
    }

    private void MoveOrUnselect()
    {
        if(!isClassic)
        {
            if(selectingBall.GetComponent<GhostBall>())
            {
                MoveBall();
            }
            else if(HasPath(target))
            {
                MoveBall();
            }
            else
            {
                UnselectBall();
                SoundSource.PlaySound("failclick");
            }
        }
        else
        {
            if(HasPath(target))
            {
                MoveBall();
            }
            else
            {
                UnselectBall();
                SoundSource.PlaySound("failclick");
            }
        }
    }
    private bool HasPath(Node target)
    {
        if(target.costToSelectedBall == 0 || target.costToSelectedBall >= SUPER_BIG_INT)
        {
            return false;
        }
        return true;
    }
    private void MoveBall()
    {
        selectingBall.SetTarget(target);
        if(!isClassic && selectingBall.GetComponent<GhostBall>())
        {
            selectingBall.GetComponent<GhostBall>().Move();
        }
        else selectingBall.Move();
    }
    public void NextTurn()
    {
        if(GameController.turn == 1)
        {
            Board.mainBoard.SpawnBalls();
            Board.mainBoard.SetSpawnQueue();
            nextButton.GetComponent<Image>().raycastTarget = false;
            Invoke("NextButtonRecover", 1f);
        }
    }
    void NextButtonRecover()
    {
        nextButton.GetComponent<Image>().raycastTarget = true;
    }
    public void UnselectBall()
    {
        if (selectingBall)
        {
            selectingBall.UnSelectedMe();
            selectingBall = null;
        }
    }
    void initNodes(){
        for (int i = 0; i < NODES_IN_ROW; i++)
        {
            for (int j = 0; j < NODES_IN_ROW; j++)
            {
                nodes[i,j] = transform.GetChild(NODES_IN_ROW * i + j).gameObject.GetComponent<Node>();
                nodes[i,j].SetMyPosition(i, j);
            }
        }
    }
}
