using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    protected PlayerState curState;
    protected PlayerMovement playerMove; //通过速度判断该用什么动画
    protected Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void StateMachine()
    {
        switch (curState)
        {
            case PlayerState.Move:
                break;

            case PlayerState.Attack:
                break;

            case PlayerState.Skill:
                break;

            case PlayerState.Ult:
                break;

            case PlayerState.Hurt:
                break;

            case PlayerState.Dead:
                break;
        }
    }
}

public enum PlayerState
{
    Idle,
    Move,
    Attack,
    Skill,
    Ult,
    Hurt,
    Dead
}
