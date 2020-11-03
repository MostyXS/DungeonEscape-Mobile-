using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
namespace Platformer.Control
{
    public class Player : MonoBehaviour, IDamageable
    {
        
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float speed = 1f;
        [SerializeField] float distanceToGround = .6f;

       
        bool canJump = true;
        int gems = 300;

        public int Health { get; set; }

        
        SpriteRenderer mySprite;
        Rigidbody2D myRigidbody;
        PlayerAnimation anim;
        SpriteRenderer swordSprite;

        public int GetGemsAmount()
        {
            return gems;
        }
        public void AddOrRemoveGems(int value)
        {
            gems+=value;
            UIManager.Instance.UpdateText();
        }
        private void Awake()
        {
            SetupDefaults();
        }

        private void SetupDefaults()
        {
            swordSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
            mySprite = GetComponentInChildren<SpriteRenderer>();
            anim = GetComponent<PlayerAnimation>();
            myRigidbody = GetComponent<Rigidbody2D>();
            Health = 4;
        }

        private void Update()
        {
            

            ProcessControl();
        }
        private void ProcessControl()
        {
            AttackBehaviour();
            Jumping();
            HorizontalMovement();
        }

        private void AttackBehaviour()
        {
            if (CrossPlatformInputManager.GetButtonDown("B_Button") && IsGrounded())
            {
                anim.Attack();
            }
        }
        private void Jumping()
        {
            if (IsGrounded() && CrossPlatformInputManager.GetButtonDown("A_Button"))
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                StartCoroutine(JumpResetTime());
                anim.Jump(true);
            }
        }

        private IEnumerator JumpResetTime()
        {
            canJump = false;
            yield return new WaitForSeconds(.1f);
            canJump = true;
        }

        private bool IsGrounded()
        {
            if (!canJump) return false;

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, distanceToGround, 256);
            if (hitInfo.collider != null) 
            {

                anim.Jump(false);
                return true;
            }
            return false;
        }

        private void HorizontalMovement()
        {
            float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
            Vector2 newVelocity = new Vector2(speed * horizontalInput, myRigidbody.velocity.y);
            myRigidbody.velocity = newVelocity;
            anim.Move(horizontalInput);
            FlipCheck(horizontalInput);
        }

        private void FlipCheck(float horizontalInput)
        {
            if (Mathf.Approximately(0, horizontalInput)) return;
            bool faceLeft = horizontalInput < 0;
            mySprite.flipX = faceLeft;
            swordSprite.flipX = faceLeft;
            swordSprite.flipY = faceLeft;
            SetupSwordArcDirection(faceLeft);
            
        }

        private void SetupSwordArcDirection(bool isLeft)
        {
            if (isLeft)
            {
                swordSprite.transform.localScale = new Vector2(-Mathf.Abs(swordSprite.transform.localScale.x), swordSprite.transform.localScale.y);
                swordSprite.transform.localPosition = new Vector2(-Mathf.Abs(swordSprite.transform.localPosition.x), swordSprite.transform.localPosition.y);
            }
            else
            {
                swordSprite.transform.localScale = new Vector2(Mathf.Abs(swordSprite.transform.localScale.x), swordSprite.transform.localScale.y);
                swordSprite.transform.localPosition = new Vector2(Mathf.Abs(swordSprite.transform.localPosition.x), swordSprite.transform.localPosition.y);
            }
        }

        public void TakeDamage()
        {
            
           
            Health--;
            UpdateUIDisplay();
            if (Health<=0)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
                anim.Jump(false);
                anim.Die();
                enabled = false;
                
            }

        }

        private void UpdateUIDisplay()
        {
            UIManager.Instance.UpdateLives(Health);
        }
    }
}