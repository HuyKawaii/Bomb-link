using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    public static LayerManager instance;
    [HideInInspector]  public int burnLayer;
    [HideInInspector]  public int connectLayer;
    [HideInInspector]  public int explosiveLayer;
    [HideInInspector]  public int enemyLayer;
    [HideInInspector]  public int uiLayer;
    

    private void Awake()
    {
        LayerManager.instance = this;
        burnLayer = LayerMask.NameToLayer("Burn");
        connectLayer = LayerMask.NameToLayer("Connect");
        explosiveLayer = LayerMask.NameToLayer("Explosive");
        enemyLayer = LayerMask.NameToLayer("Enemy");
        uiLayer = LayerMask.NameToLayer("UI");
    }
}
