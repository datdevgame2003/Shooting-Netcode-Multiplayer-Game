using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ShootingScript : NetworkBehaviour
{
    [SerializeField] private GameObject fireBall;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private List<GameObject> spawnedFireBalls = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootServerRPC();
            
        }
    }

    [ServerRpc]
    private void ShootServerRPC()
    {
        GameObject go = Instantiate(fireBall, shootTransform.position, shootTransform.rotation);
        spawnedFireBalls.Add(go);
        go.GetComponent<ProjectileMovement>().parent = this;
        go.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRPC()
    {
        GameObject toDestroy = spawnedFireBalls[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        spawnedFireBalls.Remove(toDestroy);
        Destroy(toDestroy);
    }
}
