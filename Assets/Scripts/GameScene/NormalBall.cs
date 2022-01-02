using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NormalBall : MonoBehaviour
{
    const int MAX_STEP_BALL_CAN_MOVE = 80;
    protected Node myStand = null;
    public enum STATUS{ Idle, Using}
    public STATUS status = STATUS.Idle;
    const float MOVING_FRAME = 28f;
    const float SEC_TO_60 = 60f;
    public Color myColor = new Color(0,0,0,0);
    //color val: white, green, blue, yellow, red, orange, purple, brown, dark-green, dark-blue, nothing
    protected Node target = null;
    Node nextStep = null;
    protected Animator anim;

    bool walking = false;
    float speed = 3f;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(walking)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextStep.gameObject.transform.position, Time.deltaTime * speed);
        }
    }
    public void SetTarget(Node newTarget)
    {
        target = newTarget;
    }
    public void SetColor(Color newColor)
    {
        myColor = newColor;
        gameObject.GetComponent<SpriteRenderer>().color = myColor;
    }

    public void SetMeAsSelectedBall()
    {
        anim.Play("Selected", 0, 0f);
    }
    public void UnSelectedMe()
    {
        anim.Play("Sleeping", 0, 0f);
    }
    protected void ToMovingAnimation()
    {
        anim.Play("Moving", 0, 0f);
    }
    public void ToAwakeAnimation()
    {
        anim = GetComponent<Animator>();
        anim.Play("Awake", 0, 0f);
    }
    protected void ToQuitAnimation()
    {
        anim = GetComponent<Animator>();
        anim.Play("Quit", 0, 0f);
    }
    public void SetMyStand(Node node)
    {
        myStand = node;
    }
    public Node GetMyStand()
    {
        return myStand;
    }

    public void Move(){
        GameController.turn = 0;
        StartCoroutine(MoveStepByStep());
    }
    IEnumerator MoveStepByStep()
    {
        walking = true;
        ToMovingAnimation();
        Node[] route = new Node[MAX_STEP_BALL_CAN_MOVE];
        int counter = 0;
        Node currentNode = target;
        while(currentNode != myStand)
        {
            route[counter] = currentNode;
            counter++;
            currentNode = currentNode.previousNode;
        }
        for(int i = counter - 1; i >= 0; i--)
        {
            MoveToNextPos(route[i]);
            SoundSource.PlaySound("jump");
            yield return new WaitForSeconds(MOVING_FRAME/SEC_TO_60);
        }
        walking = false;
        Board.mainBoard.UnselectBall();
        ReCalculateStand();
    }
    void MoveToNextPos(Node nextPos)
    {
        nextStep = nextPos;
        //transform.position = nextPos.gameObject.transform.position;
    }
    protected void ReCalculateStand()
    {
        myStand.status = Node.STATUS.Idle;
        myStand.SetMyBall(null);
        if(myStand.nextSpawnBall)
        {
            BallPool.GiveBackBall(myStand.nextSpawnBall);
        }

        myStand = target;
        myStand.SetMyBall(this.GetComponent<NormalBall>());
        if(myStand.nextSpawnBall)
        {
            BallPool.GiveBackBall(myStand.nextSpawnBall);
        }
        myStand.status = Node.STATUS.Holding;
        target = null;

        if(!GameController.gamecontroller.CheckScore(this.myStand))
        {
            Board.mainBoard.SpawnBalls();
            Board.mainBoard.SetSpawnQueue();
        }
        //Delete below code later
        CanSelectAgain();
    }

    public void Score()
    {
        ToQuitAnimation();
    }
    public void CanSelectAgain()
    {
        GameController.turn = 1;
    }
    public void ToThePool()
    {
        BallPool.GiveBackBall(this);
    }
}
