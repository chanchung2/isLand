using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum kMOVE_DIRECTION
    {
        None,
        Up,
        LeftUp,
        Left,
        LeftDown,
        Down,
        RightDown,
        Right,
        RightUp,
    }

    public enum kCHARACTER_STATE
    {
        Idle,
        Walk,
    }

    [SerializeField]
    private Animator m_Animator;

    private kCHARACTER_STATE m_CurrentState;
    private kMOVE_DIRECTION m_CurrentDirection;

    private Vector3 m_BaseCharacterScale;

#region Animation Parameter Name
    private readonly string kCHARACTER_WALK_FRONT = "Walk_Front";
    private readonly string kCHARACTER_WALK_SIDE = "Walk_Side";

    private readonly string kCHARACTER_IDLE_FRONT = "Idle_Front";
    private readonly string kCHARACTER_IDLE_SIDE = "Idle_Side";
    private readonly string kCHARACTER_IDLE_BACK = "Idle_Back";
#endregion

    void Start()
    {
        m_BaseCharacterScale = this.transform.localScale;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Move(kMOVE_DIRECTION.Left);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            Move(kMOVE_DIRECTION.Right);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Move(kMOVE_DIRECTION.Down);
        }
        else
        {
            Move(kMOVE_DIRECTION.None);
        }

        if (m_CurrentState == kCHARACTER_STATE.Idle)
        {
            CharacterIdleUpdate();   
        }
#endif
    }

    // 캐릭터의 방향 업데이트.
    private void CharacterIdleUpdate()
    {
        switch (m_CurrentDirection)
        {
            case kMOVE_DIRECTION.Up :
                break;
            case kMOVE_DIRECTION.Down :
                m_Animator.SetBool(kCHARACTER_IDLE_FRONT, true);
                break;
            case kMOVE_DIRECTION.Left :
            case kMOVE_DIRECTION.LeftUp :
            case kMOVE_DIRECTION.LeftDown :
                m_Animator.SetBool(kCHARACTER_IDLE_SIDE, true);
                break;
            case kMOVE_DIRECTION.Right :
            case kMOVE_DIRECTION.RightUp :
            case kMOVE_DIRECTION.RightDown :
                this.transform.localScale = new Vector3(-m_BaseCharacterScale.x, m_BaseCharacterScale.y, m_BaseCharacterScale.z);
                m_Animator.SetBool(kCHARACTER_IDLE_SIDE, true);
                break;            
        }
    }

    private void Move(kMOVE_DIRECTION direciton)    
    {
        ResetWalkParameter();
        ResetIdleParameter();
        
        // 정지한 경우.
        if (direciton == kMOVE_DIRECTION.None)
        {
            m_CurrentState = kCHARACTER_STATE.Idle;
            return ;
        }
        else
        {
            m_CurrentState = kCHARACTER_STATE.Walk;
        }

        m_CurrentDirection = direciton;
        this.transform.localScale = m_BaseCharacterScale;

        switch (direciton)
        {
            case kMOVE_DIRECTION.Up :
                break;
            case kMOVE_DIRECTION.Down :
                m_Animator.SetBool(kCHARACTER_WALK_FRONT, true);
                break;
            case kMOVE_DIRECTION.Left :
            case kMOVE_DIRECTION.LeftUp :
            case kMOVE_DIRECTION.LeftDown :
                m_Animator.SetBool(kCHARACTER_WALK_SIDE, true);
                break;
            case kMOVE_DIRECTION.Right :
            case kMOVE_DIRECTION.RightUp :
            case kMOVE_DIRECTION.RightDown :
                this.transform.localScale = new Vector3(-m_BaseCharacterScale.x, m_BaseCharacterScale.y, m_BaseCharacterScale.z);
                m_Animator.SetBool(kCHARACTER_WALK_SIDE, true);
                break;
        }
    }

    private void ResetWalkParameter()
    {
        string[] arrayParameterName = {kCHARACTER_WALK_FRONT, kCHARACTER_WALK_SIDE};

        for (int i = 0; i < arrayParameterName.Length; i++)
        {
            string parameter = arrayParameterName[i];
            m_Animator.SetBool(parameter, false);
        }      
    }

    private void ResetIdleParameter()
    {
        string[] arrayParameterName = {kCHARACTER_IDLE_BACK, kCHARACTER_IDLE_FRONT, kCHARACTER_IDLE_SIDE};

        for (int i = 0; i < arrayParameterName.Length; i++)
        {
            string parameter = arrayParameterName[i];
            m_Animator.SetBool(parameter, false);
        }
    }
}
