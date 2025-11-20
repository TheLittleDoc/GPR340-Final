using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 1.0f;
    public float MaxHealth = 20.0f;
    private float currentHealth;

    void Start()
    {
         Rigidbody rb = GetComponent<Rigidbody>();
        currentHealth = MaxHealth;


    }

    void Update()
    {

        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.position += Vector3.forward * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rigidbody.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rigidbody.position += Vector3.left * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rigidbody.position += Vector3.right * Time.deltaTime * movementSpeed;
        }
        // else if (Input.GetKey(KeyCode.Space))
        // {
        //     rigidbody.position += Vector3.
        // }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        Debug.Log("Player Health: " + currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        
    }
    private void Die() { 
        Destroy(gameObject);
    }
    public float getCurrentHealth()
    {
        return currentHealth;
    }
    public float GetHealthPercentage()
    {
        return currentHealth / MaxHealth;
    }
}