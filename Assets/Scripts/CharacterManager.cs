using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public int shipno = 2;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void SetShipno1()
    {
        shipno = 1;
    }
    public void SetShipno2()
    {
        shipno = 2;
    }
}
