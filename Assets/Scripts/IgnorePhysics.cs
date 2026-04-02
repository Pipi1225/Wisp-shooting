using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorePhysics : MonoBehaviour
{
    void Start()
    {
        Physics2D.IgnoreLayerCollision(1, 11, true);
        Physics2D.IgnoreLayerCollision(1, 1, true);

        //6 Enemies        
        Physics2D.IgnoreLayerCollision(6, 6, true);
        Physics2D.IgnoreLayerCollision(6, 1, true);
        Physics2D.IgnoreLayerCollision(6, 11, true);

        //7 Player
        Physics2D.IgnoreLayerCollision(7, 0, true);
        Physics2D.IgnoreLayerCollision(7, 1, true);
        Physics2D.IgnoreLayerCollision(7, 8, true);

        //8 uwus
        Physics2D.IgnoreLayerCollision(8, 0, true);
        Physics2D.IgnoreLayerCollision(8, 1, true);
        Physics2D.IgnoreLayerCollision(8, 8, true);

        //11 Enemies_B

    }
}
