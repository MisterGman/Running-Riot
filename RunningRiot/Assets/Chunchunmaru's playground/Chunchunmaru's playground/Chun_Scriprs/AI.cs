using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    Player player;

    void Start()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {

        player.SetDirectionalInput(new Vector2(1,0));
    }
}
