using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    private float throwCounter;

    [SerializeField]
    float throwPeriod;

    [SerializeField]
    GameObject boomerangPrefab;

    GameObject boomerang;

    [SerializeField]
    float boomerangSpeed;

    [SerializeField]
    float range = 0.0f;

    [SerializeField]
    LayerMask StopsMovement;

    private Rigidbody rb;
    private Vector3 originalPos;
    private float currentTime = 0f;
    private Vector3 throwDirection;

    void Start()
    {
        originalPos = transform.position;
        rb = GetComponent<Rigidbody>();

        throwPeriod = Random.Range(5, 10);
        throwCounter = throwPeriod;

        //Clean up
        if (boomerang != null)
            Destroy(boomerang);
    }

    void OnDestroy()
    {
        Destroy(boomerang);
    }
    void Update()
    {
        // Check if game object is in camera view.
        if (!IsInCameraView.Instance.IsTargetVisible(gameObject))
        {
            return;
        }
        
        throwCounter -= Time.deltaTime;

        if (throwCounter < 0)
        {
            // Create boomerang
            boomerang = Instantiate(boomerangPrefab, transform.position, Quaternion.identity);
            Destroy(boomerang, 10f);

            // Get postion to retun to
            originalPos = transform.position;

            // Get direction to move
            throwDirection = GetComponent<GoryiaMovement>().throwDirection;

            // Stop Goryia movement
            GetComponent<GoryiaMovement>().enabled = false;
            rb.velocity = Vector3.zero;

            // Throw
            StartCoroutine(Throw());

            // Reset interval
            throwPeriod = Random.Range(5, 10);
            throwCounter = throwPeriod;
        }
   
    }


    IEnumerator Throw()
    {
        // Throw
        while (Vector3.Distance(originalPos, boomerang.transform.position) < range && !Physics.Raycast(boomerang.transform.position, throwDirection, 0.2f, StopsMovement))
        {
            boomerang.GetComponent<Rigidbody>().velocity = throwDirection * boomerangSpeed;
            yield return null;
        }

        // Return
        while (Vector3.Distance(boomerang.transform.position, originalPos) > 0.1f)
        {
            boomerang.GetComponent<Rigidbody>().velocity =
                (transform.position - boomerang.transform.position).normalized * boomerangSpeed;
            yield return null;
        }

        Destroy(boomerang);

        // Enable Goryia movement
        GetComponent<Movement>().enabled = true;
    }

}
