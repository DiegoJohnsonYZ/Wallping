using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuroController : MonoBehaviour
{
    [SerializeField]float distanceToMove;
    void Update()
    {
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = MurosManager.instance.GameSpeed;
        if (MurosManager.instance.IsWallping) distanceToMove = MurosManager.instance.GameSpeed * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);


    }
}
