using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HasarYazisiYokEt : MonoBehaviour
{
    public void AnneniYokEt()
    {
        Destroy(transform.parent.gameObject);
    }
    public void bildirimPaneliYoket()
    {
        GameManager.gm.DunyaBildirimi.SetActive(false);
    }
}
