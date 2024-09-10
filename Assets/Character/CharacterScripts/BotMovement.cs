using System;
using Character.CharacterScriptable;
using UnityEngine;

namespace Character.CharacterScripts
{
    internal enum MoveState
    {
        None,
        MovingLeft,
        MovingRight
    }
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] private BotComponents botComponents;
        [SerializeField] private BotStats botStats;
        [SerializeField] private BotInput botInput;
        private MoveState lastMoveState = MoveState.MovingRight;

        private void Update()
        {
            
            HandleBotInput();
        }

        private void FixedUpdate()
        {
            MoveHorizontally(botStats.MoveSpeed);
            UpdateMoveState();
        }

        private void HandleBotInput()
        {
            botStats.MoveDirection = new Vector2(
                botInput.MoveRight.action.ReadValue<float>() - botInput.MoveLeft.action.ReadValue<float>(),
                0
            ).normalized;
        }

        public void MoveHorizontally(float horizontalSpeed)
        {
            if (botStats.MoveDirection.x != 0)
            {
                botStats.VelocityX = Mathf.MoveTowards(botComponents.Rb.velocity.x,
                    botStats.MoveDirection.x * horizontalSpeed, botStats.SmoothTime * Time.fixedTime);
                botComponents.Rb.velocity =
                    new Vector2(botStats.VelocityX, botComponents.Rb.velocity.y);
            }
            else if(botStats.MoveDirection.x == 0)
            {
                botComponents.Rb.velocity =
                    new Vector2(0, botComponents.Rb.velocity.y);
            }
        }
        

        private void UpdateMoveState()
        {
            if (botStats.MoveDirection.x > 0)
            {
                if (lastMoveState != MoveState.MovingRight)
                {
                    RotateBot(90);
                    botStats.IsRotating = true;
                    lastMoveState = MoveState.MovingRight;
                }
            }
            else if (botStats.MoveDirection.x < 0)
            {
                if (lastMoveState != MoveState.MovingLeft)
                {
                    RotateBot(-90);
                    botStats.IsRotating = true;
                    lastMoveState = MoveState.MovingLeft;
                }
            }
        }
        
        private void RotateBot(float angle)
        {
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }

        private void RunToStop() => botStats.IsRunning = false;
        private void RotateToRun() => botStats.IsRotating = false;
    }
}
