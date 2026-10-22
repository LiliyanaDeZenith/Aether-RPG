using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f, runSpeed = 8f, jumpForce = 6f, glideGravity = 2f;
    public float stamina = 100f, maxStamina = 100f, staminaRegen = 15f, sprintCost = 25f, glideCostPerSecond = 8f;

    CharacterController controller;
    Vector3 velocity;
    bool isGrounded, isGliding, isSprinting;

    void Awake() { controller = GetComponent<CharacterController>(); }

    void Update()
    {
        isGrounded = controller.isGrounded;
        HandleMovement();
        HandleJumpAndGlide();
        HandleStamina();
        HandleCharacterSwitchAndSkills();
    }

    void HandleMovement()
    {
        Transform cam = Camera.main != null ? Camera.main.transform : transform;
        float h = Input.GetAxis("Horizontal"), v = Input.GetAxis("Vertical");
        Vector3 dir = cam.forward * v + cam.right * h;
        dir.y = 0; dir.Normalize();

        isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0 && dir.magnitude > 0.1f;
        float speed = isSprinting ? runSpeed : walkSpeed;
        if (isSprinting) stamina -= sprintCost * Time.deltaTime;

        controller.Move(dir * speed * Time.deltaTime);
        if (dir.magnitude > 0.1f)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 10f);
    }

    void HandleJumpAndGlide()
    {
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) { velocity.y = jumpForce; isGliding = false; }

        if (!isGrounded && Input.GetKey(KeyCode.Space) && velocity.y < 0 && stamina > 0)
        { isGliding = true; velocity.y = -glideGravity; stamina -= glideCostPerSecond * Time.deltaTime; }
        else
        { isGliding = false; velocity.y += Physics.gravity.y * Time.deltaTime; }

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleStamina()
    {
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        if (!isSprinting && !isGliding && isGrounded)
            stamina = Mathf.Min(maxStamina, stamina + staminaRegen * Time.deltaTime);
    }

    void HandleCharacterSwitchAndSkills()
    {
        var cm = CharacterManager.Instance;
        if (Input.GetKeyDown(KeyCode.Alpha1)) cm.SwitchCharacter(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) cm.SwitchCharacter(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) cm.SwitchCharacter(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) cm.SwitchCharacter(3);
        if (Input.GetMouseButtonDown(0)) cm.NormalAttack();
        if (Input.GetKeyDown(KeyCode.E)) cm.UseElementalSkill();
        if (Input.GetKeyDown(KeyCode.Q)) cm.UseElementalBurst();
        if (Input.GetKeyDown(KeyCode.T)) LevelSystem.Instance.ToggleTrainingMode();
    }
}
