using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    public enum kMOVE_DIRECTION
    {
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

    private SpriteAtlas m_SpriteAtlasCharacter;

    [Header("SpriteSlot")]
    [SerializeField]
    private SpriteRenderer m_SpriteCharacter;
    [SerializeField]
    private SpriteRenderer m_SpriteTop;
    [SerializeField]
    private SpriteRenderer m_SpritePants;
    [SerializeField]
    private SpriteRenderer m_SpriteEyes;
    [SerializeField]
    private SpriteRenderer m_SpriteHair;

    [Header("SpriteName")]
    [SerializeField]
    private string kSPRITE_NAME_CHARATER;
    [SerializeField]
    private string kSPRITE_NAME_TOP;
    [SerializeField]
    private string kSPRITE_NAME_PANTS;
    [SerializeField]
    private string kSPRITE_NAME_EYES;
    [SerializeField]
    private string kSPRITE_NAME_HAIR;

    [Header("SpriteDirectionName")]
    [SerializeField]
    private string kSPRITE_NAME_DIRECTION_SIDE;
    [SerializeField]
    private string kSPRITE_NAME_DIRECTION_BACK;

#region Animation Parameter Name
    private readonly string kPARAMETER_NAME_WALK = "Walk";
#endregion

    void Start()
    {
        m_BaseCharacterScale = this.transform.localScale;

        m_SpriteAtlasCharacter = Resources.Load<SpriteAtlas>("Atlas/Character");
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Move(kMOVE_DIRECTION.Up);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
            StopMove();
        }
#endif
    }

    private void UpdateSprite(int index)
    {
        string directionName = GetDirectionSpriteName();

        m_SpriteCharacter.sprite = m_SpriteAtlasCharacter.GetSprite(string.Format("{0}{1}_{2}", kSPRITE_NAME_CHARATER, directionName, index));
        m_SpriteTop.sprite = m_SpriteAtlasCharacter.GetSprite(string.Format("{0}{1}_{2}", kSPRITE_NAME_TOP, directionName, index));
        m_SpritePants.sprite = m_SpriteAtlasCharacter.GetSprite(string.Format("{0}{1}_{2}", kSPRITE_NAME_PANTS, directionName, index));
        m_SpriteEyes.sprite = m_SpriteAtlasCharacter.GetSprite(string.Format("{0}{1}_{2}", kSPRITE_NAME_EYES, directionName, index));
        m_SpriteHair.sprite = m_SpriteAtlasCharacter.GetSprite(string.Format("{0}{1}_{2}", kSPRITE_NAME_HAIR, directionName, index));
    }

    private void Move(kMOVE_DIRECTION direciton)    
    {
        ResetAnimatorParameter();
        
        m_CurrentDirection = direciton;

        m_CurrentState = kCHARACTER_STATE.Walk;
        m_Animator.SetBool(kPARAMETER_NAME_WALK, true);
        
        switch (m_CurrentDirection)
        {
            case kMOVE_DIRECTION.Right :
            case kMOVE_DIRECTION.RightDown :
            case kMOVE_DIRECTION.RightUp :
                this.transform.localScale = new Vector3(-m_BaseCharacterScale.x, m_BaseCharacterScale.y, m_BaseCharacterScale.z);
                break;
            default :
                this.transform.localScale = m_BaseCharacterScale;
                break;
        }

        // 이동 로직 추가.
    }

    private void StopMove()
    {
        ResetAnimatorParameter();

        m_CurrentState = kCHARACTER_STATE.Idle;

        // todo :: Idle 애니메이션이 추가되면 수정.
        //m_Animator.SetBool(kPARAMETER_NAME_IDLE, true);
    }

    private void ResetAnimatorParameter()
    {
        string[] arrayParameterName = {kPARAMETER_NAME_WALK};

        for (int i = 0; i < arrayParameterName.Length; i++)
        {
            string parameter = arrayParameterName[i];
            m_Animator.SetBool(parameter, false);
        }          
    }

    private string GetDirectionSpriteName()
    {
        switch (m_CurrentDirection)
        {
            case kMOVE_DIRECTION.Up :
                return kSPRITE_NAME_DIRECTION_BACK;
            case kMOVE_DIRECTION.Down :
                return string.Empty;
            default :
                return kSPRITE_NAME_DIRECTION_SIDE;
        }
    }
}
