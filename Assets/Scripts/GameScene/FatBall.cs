using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatBall : NormalBall
{
    public int state = 1;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        SoundSource.PlaySound("fat");
    }
    public void SetMeToState2()
    {
        anim.Play("Sleeping2", 0, 0f);
    }
    public void TransitionToState2()
    {
        anim = GetComponent<Animator>();
        anim.Play("Quit1", 0, 0f);
    }
    public void Score()
    {
        if(state == 1)
        {
            state = 2;
            TransitionToState2();
        }
        else ToQuitAnimation();
    }
    public void ToThePool()
    {
        myStand.status = Node.STATUS.Idle;
        myStand.SetMyBall(null);
        DataController.datacontroller.AddScore(1);
        Destroy(gameObject);
    }
}
