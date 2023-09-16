using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoController : MonoBehaviour
{
    [SerializeField] float distanceToMove;
    void Update()
    {
        if (MurosManager.instance.IsHolding) distanceToMove = 0;
        else distanceToMove = -0.01f;
        if (MurosManager.instance.IsWallping) distanceToMove = -0.01f * MurosManager.instance.WallpingSpeed;
        transform.position += new Vector3(0, distanceToMove, 0);
        if (transform.position.y <= -27.16f) transform.position = new Vector3(transform.position.x, 14, transform.position.z);

    }
}
