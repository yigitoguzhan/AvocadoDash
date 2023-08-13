using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Sprite sadFace;
    [SerializeField] Sprite avocadoFace;
    [SerializeField] Sprite spaceShip;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] GameObject spaceShipTrail;
    [SerializeField] GameObject spaceShipParticleTrail;

    [SerializeField] BoxCollider2D jumpCollider;
    [SerializeField] BoxCollider2D flyCollider;

    [SerializeField] Transform rayTransform;
    [SerializeField] Transform playerTransform;
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float levelSpeed;
    [SerializeField] float flySpeed;
    [SerializeField] float rotationSpeed;
    float targetRotation = 0f;
    float currentRotation = 0f;
    float maxRotation = 30f;
    float minRotation = -30f;
    float groundHitY;
    [SerializeField] float animDuration;
    [SerializeField] float jumpPower;
    [SerializeField] Transform levelCompleteTransform;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject groundTouchEffect;

    [SerializeField] MovementType movementType = MovementType.Jump;
    float currentZRotation = 0f;
    bool isGrounded = true;
    bool isGameOver = false;
    bool isLevelCompleted = false;

    const int GROUND_LAYER = 6;
    const int OBSTACLE_LAYER = 7;
    const int PORTAL_GREEN_LAYER = 8;
    const int PORTAL_PINK_LAYER = 9;


    // Update is called once per frame
    void Update()
    {
        playerTransform.position = new Vector2(playerTransform.position.x + levelSpeed * Time.deltaTime, playerTransform.position.y);
        rayTransform.position = playerTransform.position;
        PlayerMovement();
        GroundHitDetect();
        LevelCompleteCheck();
    }


    public void Jump()
    {
        if (Input.GetMouseButton(0) && isGrounded && !isGameOver && movementType == MovementType.Jump)
        {
            playerRB.AddForce(Vector2.up * jumpPower);
            ChangeFace();

            if (playerTransform != null) { playerTransform.DORotate(new Vector3(0, 0, currentZRotation - 90f), animDuration); }
            currentZRotation -= 90f;
            isGrounded = false;

        }
    }
    public void Fly()
    {
        if (Input.GetMouseButton(0) && !isGameOver && movementType == MovementType.Fly)
        {
            playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y + flySpeed * Time.deltaTime);
            targetRotation = maxRotation;
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }
        //RAY ILE 1.97 DEGERI DEGISKENE BAGLANACAK!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        else if (playerTransform.position.y - flySpeed * Time.deltaTime > groundHitY + 0.53f)
        {
            targetRotation = minRotation;
            playerTransform.position = new Vector2(playerTransform.position.x, playerTransform.position.y - flySpeed * Time.deltaTime);
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        }
        else
        {
            targetRotation = 0f;
            currentRotation = Mathf.MoveTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == OBSTACLE_LAYER && !isGameOver)
        {
            explosionEffect.transform.position = playerTransform.position;
            explosionEffect.SetActive(true);
            playerSpriteRenderer.enabled = false;
            isGameOver = true;
            levelSpeed = 0f;
            Debug.Log("GAME OVER");
            DOTween.Sequence().SetDelay(3f).OnComplete(() => SceneManager.LoadScene("Game"));
        }
        else if (collision.gameObject.layer == GROUND_LAYER)
        {
            ActivateGroundHitEffect();

            isGrounded = true;
            Debug.Log(playerTransform.position.x);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == PORTAL_GREEN_LAYER)
        {
            playerTransform.rotation = Quaternion.Euler(Vector3.zero);
            //FLYING MODE
            movementType = MovementType.Fly;

            spaceShipTrail.SetActive(true);
            spaceShipParticleTrail.SetActive(true);

            //Enable Flying Collider
            jumpCollider.enabled = false;
            flyCollider.enabled = true;

            DOTween.Kill(playerTransform);
            playerSpriteRenderer.sprite = spaceShip;
            playerRB.gravityScale = 0f;
        }
        else if (collision.gameObject.layer == PORTAL_PINK_LAYER)
        {
            playerTransform.rotation = Quaternion.Euler(Vector3.zero);

            //JUMP MODE
            movementType = MovementType.Jump;

            spaceShipTrail.SetActive(false);
            spaceShipParticleTrail.SetActive(false);


            //Enable Jump Collider
            flyCollider.enabled = false;
            jumpCollider.enabled = true;

            playerSpriteRenderer.sprite = avocadoFace;
            playerRB.gravityScale = 10f;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GROUND_LAYER)
        {
            isGrounded = false;
        }
    }
    public void GroundHitDetect()
    {
        if (movementType == MovementType.Fly)
        {
            RaycastHit2D hit = Physics2D.Raycast(rayTransform.position, Vector2.down);
            //Debug.Log("Çarpýþma Noktasý: " + hit.point);
           // Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.gameObject.layer == GROUND_LAYER)
            {
                groundHitY = hit.point.y;
               // Debug.Log("Çarpýþma Noktasý: " + hit.point);
            }
            else
            {
                groundHitY = -1.97f;
            }
        }

    }
    public void LevelCompleteCheck()
    {
        if (playerTransform.position.x >= levelCompleteTransform.position.x&& !isLevelCompleted)
        {
            isLevelCompleted = true;
            levelSpeed = 0f;
            DOTween.Sequence().SetDelay(1f).OnComplete(() => SceneManager.LoadScene("MainMenu"));

        }
    }
    public void PlayerMovement()
    {
        if (movementType == MovementType.Jump)
        {
            Jump();
        }
        else
        {
            Fly();
        }
    }
    public void ChangeFace()
    {
        if ((currentZRotation - 90) % 360 == 0)
        {
            playerSpriteRenderer.sprite = avocadoFace;
        }
        else
        {
            playerSpriteRenderer.sprite = sadFace;
        }
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
    public bool GetIsGameOver()
    { return isGameOver; }

    public void ActivateGroundHitEffect()
    {
        groundTouchEffect.transform.position = playerTransform.position;
        groundTouchEffect.SetActive(true);
        DOTween.Sequence().SetDelay(0.25f).OnComplete(() => groundTouchEffect.SetActive(false));
    }
    enum MovementType
    {
        Jump,
        Fly
    }
}
