using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class arma : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    [SerializeField]
    private float fireSpeed = 20.0f,
        alcance = 100.0f;
    private float forcaEmpurrao;

    [Header("Verificações do sistema")]
    public LayerMask mask;
    private bool armaNaMao;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable voPegar = GetComponent<XRGrabInteractable>();
        voPegar.activated.AddListener(FireBullet);
    }

    private void FireBullet(ActivateEventArgs zecapagodin)
    {
        GameObject tiro = Instantiate(bullet);
        tiro.transform.position = spawnPoint.position;

        // Indicando a direção e a velocidade do disparo
        tiro.GetComponent<Rigidbody>().velocity = spawnPoint.forward
            * fireSpeed;

        Destroy(tiro, 5);

        Shoot();


    }

    private void Shoot()
    {
        RaycastHit hit;

        // O out hit servirá para armazenar informações entre hit
        // (nosso raycast) e o objeto que entrou em contato com ele
        if (Physics.Raycast(transform.position, transform.forward,
            out hit, alcance, mask))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            if (hit.collider.CompareTag("alvo") && rb != null)
            {
                rb.AddForce(-hit.normal * forcaEmpurrao);
            }
        }

        if (armaNaMao)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.blue);
        }

    }
}

