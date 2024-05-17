using Character.CharacterScriptable;
using UnityEngine;

namespace Character.CharacterScripts
{
    public class BotStateMachine : MonoBehaviour
    {
        private BotStateFactory states;
        private BotBaseState currentState;
        [SerializeField] private BotStats botStats;
        [SerializeField] private BotDetectionStats botDetectionStats;
        [SerializeField] private BotComponents botComponents;
        [SerializeField] private BotInput botInput;
        [SerializeField] private BotMovement botMovement;
        [SerializeField] private BotAnimatorController botAnimatorController;
        private void Awake()
        {
            states = new BotStateFactory(this,botDetectionStats,botStats,botComponents,botMovement,botInput,botAnimatorController);
            currentState = states.Grounded();
            currentState.EnterState();
        }
        private void FixedUpdate()
        {
            currentState.FixedUpdate();
        }
        private void Update()
        {
            currentState.UpdateState();
            StateSwitcher();
        }
        
        private void StateSwitcher()
        {
            CheckState(states.Grounded());
        }
    
        private void CheckState(BotBaseState newState)
        {
            if (currentState == newState) return;
            currentState.ExitState();
            currentState = newState;
            currentState.EnterState();
        }
    }
}
