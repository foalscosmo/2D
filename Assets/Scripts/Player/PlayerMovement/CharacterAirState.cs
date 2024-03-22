using CombatInterface;
using UnityEngine;

namespace Player.PlayerMovement
{
    // Represents the character state when in the air
    public class CharacterAirState : CharacterBaseState, IAttacker
    {
        // Constructor to initialize the air state
        public CharacterAirState(CharacterStateMachine currentContext, CharacterStateFactory characterStateFactory, CharacterStats characterStats, DetectionStats detectionStats, CharacterDetection characterDetection, CharacterComponents components, CharacterInput input, CharacterDash characterDash, CharacterMovement characterMovement, CharacterJump characterJump, CharacterAnimation characterAnimation) : base(currentContext, characterStateFactory, characterStats, detectionStats, characterDetection, components, input, characterDash, characterMovement, characterJump, characterAnimation)
        {
        }

        // Called when entering the air state
        public override void EnterState()
        {
        }
        
        public override void UpdateState()
        { 
            JumpAnimations();
            AirDashAnimation();
            FallAnimation();
            if (stats.IsDashing && !stats.IsAttacking)
            {
                characterDash.Dash();
            }
            if (characterInput.Attack.action.triggered) PerformAttack();
        }

        public override void FixedUpdate()
        {
            if (characterInput.MoveLeft.action.ReadValue<float>() > 0.1f || characterInput.MoveRight.action.ReadValue<float>() > 0.1f || stats.IsDashing)
            {
                characterMovement.MoveHorizontally(stats.MoveVector.x * stats.MoveSpeed);
            }

            if(stats.IsDashing) characterDash.PerformDash();
            characterJump.ApplyGravity();
            characterJump.HandleJumpEnd();
            characterJump.Jump();
        }

        public override void ExitState()
        { 
            stats.AirAttackCounter = 0;
        }

        private void JumpAnimations()
        {
            if (!characterInput.JumpAction.action.triggered || !stats.IsJump) return;
            switch (stats.NumberOfJumps)
            {
                case 1:
                    characterAnimation.ChangeAnimationState(characterAnimation.jumpAnim, 0f);
                    break;
                case 2:
                    characterAnimation.ChangeAnimationState(characterAnimation.doubleJumpAnim, 0f);
                    break;
            }

        }

        private void FallAnimation()
        {
            if (!stats.IsDashing && !stats.IsAttacking)
            {
                characterAnimation.ChangeAnimationState(characterAnimation.jumpAnim, 0.1f);
            }
        }

        private void AirDashAnimation()
        {
            stats.DashCooldownTimer = Mathf.Max(0f, stats.DashCooldownTimer - Time.deltaTime);

            if (characterInput.DashAction.action.triggered && stats.DashCooldownTimer <= 0)
            {
                stats.IsDashing = true;
                characterAnimation.ChangeAnimationState(characterAnimation.dashAnim,0f);
                stats.DashCooldownTimer = 1f;
            }
        }
        
        public void PerformAttack()
        {
            if(stats.AirAttackCounter > 1) return;
            characterComponents.Rb.velocity = new Vector2(0, 5); 
            characterAnimation.ChangeAnimationState(characterAnimation.kickAnim, 0f);
            stats.IsAttacking = true;
            stats.AirAttackCounter++;
        }
        
    }
}