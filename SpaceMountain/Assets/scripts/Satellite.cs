using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satellite : MonoBehaviour
{
    GameObject sun;
    Admiral governor;
    [SerializeField]
    float year = 1, growth = 1, population = 10000, popgrowth = 1.1f, prosperity = 1.1f, tax = 1f;
    private float GDP
    {
        get { return population * prosperity; }
    }
    // Use this for initialization
    void Start()
    {
        sun = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public float EconomicUpdate(float tax)
    {
        this.tax = tax;
        return EconomicUpdate();
    }
    float EconomicUpdate()
    {
        population *= popgrowth / tax;
        prosperity *= growth / tax;

        return GDP * tax;
    }
    private void FixedUpdate()
    {
        transform.RotateAround(sun.transform.position, Vector3.forward, year);
    }
}
