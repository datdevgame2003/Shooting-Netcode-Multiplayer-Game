using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ProjectileMovement : NetworkBehaviour
{
    //[SerializeField] private GameObject hitParticles;
    [SerializeField] private float shootForce;
    private Rigidbody rigy;
    public ShootingScript parent;

    // Start is called before the first frame update
    void Start()
    {
        rigy = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigy.velocity = rigy.transform.forward * shootForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner)
        {
            return;
        }


        InstantiateHitParticlesServerRpc();
        parent.DestroyServerRPC();
    }

    [ServerRpc]
    private void InstantiateHitParticlesServerRpc()
    {
        //GameObject hitImpact = Instantiate(hitParticles, transform.position, Quaternion.identity);
        //hitImpact.GetComponent<NetworkObject>().Spawn();
        //hitImpact.transform.localEulerAngles = new Vector3(0, 0, -90f);
    }

}
