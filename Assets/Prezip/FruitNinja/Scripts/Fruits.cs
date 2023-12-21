using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    private GameManager gm;
    public GameObject slicedFruit;
    public GameObject fruitJuice;
    private float rotationForce = 200;
    private Rigidbody rb;

    public int scorePoints;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        transform.Rotate(Vector2.right * Time.deltaTime * rotationForce);
    }

    private void InstantiateSlicedFruit()
    {
        GameObject instantiateFruit = Instantiate(slicedFruit, transform.position, transform.rotation);
        GameObject instantiateJuice = Instantiate(fruitJuice, new Vector3(transform.position.x, transform.position.y, 0), fruitJuice.transform.rotation);

        Rigidbody[] slicedRb = instantiateFruit.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody srb in slicedRb)
        {
            srb.AddExplosionForce(150f, transform.position, 10);
            srb.velocity = rb.velocity * 1.2f;
        }
        Destroy(instantiateFruit, 5);
        Destroy(instantiateJuice, 5);
    }

    /* private void OnMouseDown()
     {
         InstantiateSlicedFruit();
         Destroy(gameObject);
     }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Blade")
        {
            gm.UpdateTheScore(scorePoints);
            Destroy(gameObject);
            InstantiateSlicedFruit();
        }

        if(other.tag == "BottomTrigger")
        {
            gm.UpdateLives();
        }
    }
}
