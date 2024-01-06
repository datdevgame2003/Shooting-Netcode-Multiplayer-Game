using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class AutoDestroyParticles : NetworkBehaviour
{
    public float delayTime;

    [ServerRpc(RequireOwnership = false)]
    private void DestroyParticlesServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject, delayTime);
    }
}
