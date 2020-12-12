using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_SpriteItem;

    private ItemData m_ItemData;

    void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other.name);
        if (other.tag != kTAG.Player.ToString())
            return ;

        var player = ObjectManager.Instance.GetObject<Player>();
        StartCoroutine(FollowPlayerCorotuine(player));
    }
    public void UpdateItemData(ItemData data)
    {
        m_ItemData = data;
    }

    private IEnumerator FollowPlayerCorotuine(Player player)
    {
        float directionTime = 2f;

        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            float timeRatio = time / directionTime;

            Debug.Log("TimeRatio : " + timeRatio);
            this.transform.position = Vector3.Lerp(this.transform.position, player.transform.position, timeRatio);

            if (Vector3.Distance(this.transform.position, player.transform.position) <= 0.1f)
                break;

            yield return null;
        } 

        // todo :: 획득 코드 추가.
        this.gameObject.SetActive(false);
    }
}
