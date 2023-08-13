using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform groundParticleTransform;
    [SerializeField] ParticleSystem groundParticleSystem;
    GameObject playerGameObject;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerGameObject = GameObject.Find("Player");
        if (playerGameObject != null)
        {
            playerController = playerGameObject.GetComponent<PlayerController>();
        }
        ParticleFollow();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleFollow();
    }
    public void ParticleFollow()
    {
        groundParticleTransform.position = new Vector3(playerTransform.position.x - 0.45f, playerTransform.position.y - 0.13f, 0f);
        if(playerController != null && playerController.GetIsGrounded()&&!playerController.GetIsGameOver()) 
        {
            groundParticleTransform.DOScale(Vector3.one, 0.3f);
        }
        else 
        {
            groundParticleTransform.DOScale(Vector3.zero, 0f);
        }

    }

}
