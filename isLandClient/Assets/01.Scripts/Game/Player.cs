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
    private Rigidbody2D m_Rigidbody;

    [SerializeField]
    private Animator m_Animator;

    private StatData m_StatData;

    private kCHARACTER_STATE m_CurrentState;
    private kMOVE_DIRECTION m_CurrentDirection;

    private Vector3 m_BaseCharacterScale;

    public delegate void OnActiveButtonPressHandler(bool isPress, Vector3 activeTileStartPos);
    public event OnActiveButtonPressHandler OnActiveButtonPress;

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
        m_StatData = DataManager.Instance.GetStatData();

        m_BaseCharacterScale = this.transform.localScale;
        
        ObjectManager.Instance.CreateObject<ActiveTile>(Constants.kPREFAB_ATIVE_TILE);
    }

    void Update()
    {
#if UNITY_EDITOR
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.Space))
        {
            PressActionButton(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            PressActionButton(false);
        }

        if (h == 0 && v == 0)
        {
            StopMove();
        }
        else
        {
            Move(new Vector2(h, v));
        }
#endif
    }

#region Update
    // 캐릭터 Sprite 갱신.
    private void UpdateSprite(int index)
    {
        string directionName = GetDirectionSpriteName();

        m_SpriteCharacter.sprite = AtlasManager.Instance.GetSprite(kATLAS.Character, string.Format("{0}{1}_{2}", kSPRITE_NAME_CHARATER, directionName, index));
        m_SpriteTop.sprite = AtlasManager.Instance.GetSprite(kATLAS.Character, string.Format("{0}{1}_{2}", kSPRITE_NAME_TOP, directionName, index));
        m_SpritePants.sprite = AtlasManager.Instance.GetSprite(kATLAS.Character, string.Format("{0}{1}_{2}", kSPRITE_NAME_PANTS, directionName, index));
        m_SpriteEyes.sprite = AtlasManager.Instance.GetSprite(kATLAS.Character, string.Format("{0}{1}_{2}", kSPRITE_NAME_EYES, directionName, index));
        m_SpriteHair.sprite = AtlasManager.Instance.GetSprite(kATLAS.Character, string.Format("{0}{1}_{2}", kSPRITE_NAME_HAIR, directionName, index));
    }

    // 캐릭터가 보고 있는 방향 갱신. (좌, 우)
    private void UpdateDirection()
    {
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
    }
#endregion

#region Move
    private void Move(Vector2 movePos)    
    {
        ResetAnimatorParameter();
        
        m_CurrentDirection = GetMoveDirection(movePos);
        UpdateDirection();

        m_CurrentState = kCHARACTER_STATE.Walk;
        m_Animator.SetBool(kPARAMETER_NAME_WALK, true);

        m_Rigidbody.MovePosition((Vector2)this.transform.position + movePos.normalized * m_StatData.SPEED * Time.deltaTime);
    }

    private void StopMove()
    {
        ResetAnimatorParameter();

        m_CurrentState = kCHARACTER_STATE.Idle;
    }
#endregion
    public void PressActionButton(bool isPress)
    {
        var tilePos = TileMapManager.Instance.GetWorldToCell(this.transform.position);
        OnActiveButtonPress(isPress, tilePos + Constants.kTILE_PIVOT_POS + GetMovePos());
    }

    // 애니메이터 파라미터 초기화.
    private void ResetAnimatorParameter()
    {
        string[] arrayParameterName = {kPARAMETER_NAME_WALK};

        for (int i = 0; i < arrayParameterName.Length; i++)
        {
            string parameter = arrayParameterName[i];
            m_Animator.SetBool(parameter, false);
        }          
    }

#region GetData
    // 현재 방향에 맞는 Sprite 이름 반환.
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

    // 움직이는 방향에 맞는 Direction 값 반환.
    private kMOVE_DIRECTION GetMoveDirection(Vector2 movePos)
    {
        // Up
        if (movePos == new Vector2(0,1))
            return kMOVE_DIRECTION.Up;
        // Down
        else if (movePos == new Vector2(0, -1))
            return kMOVE_DIRECTION.Down;
        // Right
        else if (movePos == new Vector2(1, 0))
            return kMOVE_DIRECTION.Right;
        // Left
        else if (movePos == new Vector2(-1, 0))
            return kMOVE_DIRECTION.Left;
        // LeftUp
        else if (movePos == new Vector2(-1, 1))
            return kMOVE_DIRECTION.LeftUp;
        // LeftDown
        else if (movePos == new Vector2(-1, -1))
            return kMOVE_DIRECTION.LeftDown;
        // RightUp
        else if (movePos == new Vector2(1, 1))
            return kMOVE_DIRECTION.RightUp;
        // RightDown
        else if (movePos == new Vector2(1, -1))
            return kMOVE_DIRECTION.RightDown;
        else
        {
            Debug.Log("Player::GetMoveDirection Error");
            return kMOVE_DIRECTION.Down;
        }
    }

    // 현재 Direction에 맞는 움직이는 방향 반환. 
    private Vector3 GetMovePos()
    {
        switch (m_CurrentDirection)
        {
            case kMOVE_DIRECTION.Up :
                return new Vector3(0, 1, 0);
            case kMOVE_DIRECTION.Down :
                return new Vector3(0, -1, 0);
            case kMOVE_DIRECTION.Left :
            case kMOVE_DIRECTION.LeftUp :
            case kMOVE_DIRECTION.LeftDown :
                return new Vector3(-1, 0, 0);
            case kMOVE_DIRECTION.Right :
            case kMOVE_DIRECTION.RightUp :
            case kMOVE_DIRECTION.RightDown :
                return new Vector3(1, 0, 0);
            default :
                return Vector3.zero;
        }
    }    
#endregion
}
