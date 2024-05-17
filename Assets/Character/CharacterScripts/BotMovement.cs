using System;
using Character.CharacterScriptable;
using UnityEngine;

namespace Character.CharacterScripts
{
    public class BotMovement : MonoBehaviour
    {
        [SerializeField] private BotComponents botComponents;
        [SerializeField] private BotStats botStats;
        [SerializeField] private BotInput botInput;

        private void Update()
        {
            HandleBotInput();
        }

        private void FixedUpdate()
        {
            MoveHorizontally(botStats.MoveSpeed);
            RotateBot();

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

        private void RotateBot()
        {
            // if (botStats.MoveDirection != botStats.PreviousMoveDirection)
            // {
            //     botStats.PreviousMoveDirection = botStats.MoveDirection;
            //
            //     switch (botStats.MoveDirection.x)
            //     {
            //         case 1:
            //             transform.rotation = Quaternion.Euler(0, 90, 0);
            //             break;
            //         case -1:
            //             transform.rotation = Quaternion.Euler(0, -90, 0);
            //             break;
            //     }
            // }
            
            Vector3 currentMoveDirection = botStats.MoveDirection.normalized;

            if (currentMoveDirection != botStats.PreviousMoveDirection)
            {
                botStats.PreviousMoveDirection = currentMoveDirection;

                if (currentMoveDirection.x > 0)
                {
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                    botStats.IsRotating = true;

                }
                else if (currentMoveDirection.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                    botStats.IsRotating = true;

                }
            }
        }

        private void RunToStop() => botStats.IsRunning = false;
        private void RotateToRun() => botStats.IsRotating = false;
    }
}
