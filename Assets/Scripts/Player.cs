using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player : MonoBehaviour, IShopCustomer
{
    public float speed = 8f;
    public float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isAffected = false;
    private float effectTimer = 0f;
    private float originalSpeed;
    private float moveInput;
    private int money = 0; // Player's money
    public Text moneyText; // Reference to the UI Text component

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private InputAction movementAction; // This will now be set up to read a float
    [SerializeField] private InputAction jumpAction;

    private void OnEnable()
    {
        movementAction.Enable();
        jumpAction.Enable();
        jumpAction.performed += _ => Jump();
    }

    private void OnDisable()
    {
        movementAction.Disable();
        jumpAction.Disable();
        jumpAction.performed -= _ => Jump();
    }

    void Awake()
    {
        originalSpeed = speed; // Store the original speed of the player
        UpdateMoneyUI(); // Initialize UI
    }
    void Update()
    {
        moveInput = movementAction.ReadValue<float>(); // Read as float for single axis

        if (isAffected)
        {
            effectTimer -= Time.deltaTime;
            if (effectTimer <= 0)
            {
                isAffected = false;
                speed = originalSpeed;
            }
        }
        Flip();
    }

    // Method to add money and possibly update UI
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
        {
            moneyText.text = $"Money: {money}";
        }
    }
    private void FixedUpdate()
    {
        // Apply movement along the horizontal axis
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }

    private void Flip()
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void ApplySlow(float slowFactor, float duration)
    {
        if (!isAffected)
        {
            speed *= slowFactor;
            isAffected = true;
            effectTimer = duration;
        }
    }

    public void ApplySpeedBoost(float boostFactor, float duration)
    {
        if (!isAffected)
        {
            speed *= boostFactor;
            isAffected = true;
            effectTimer = duration;
        }
    }

    public void BoughtItem(Item.ItemType itemType)
    {
        Debug.Log("Bought item: " + itemType);
    }

    public bool TrySpendGoldAmount(int spendGoldAmount)
    {
        if (money >= spendGoldAmount)
        {
            money -= spendGoldAmount;
            UpdateMoneyUI();
            return true;
        }
        else
        {
            return false;
        }
    }
}