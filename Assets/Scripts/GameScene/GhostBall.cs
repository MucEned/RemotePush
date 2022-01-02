using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBall : NormalBall
{
    public void Move(){
        GameController.turn = 0;
        ToMovingAnimation();
        // Fly();
    }
    void Fly()
    {
        SoundSource.PlaySound("wind");
        transform.position = target.gameObject.transform.position;
        ReCalculateStand();
    }
    public void ToThePool()
    {
        myStand.status = Node.STATUS.Idle;
        myStand.myBall = null;
        Destroy(gameObject);
    }
}
