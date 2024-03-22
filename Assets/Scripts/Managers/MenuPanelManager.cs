using System;
using System.Collections.Generic;
using Hover;
using Save_Load;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Namespace declaration
namespace Managers
{
    // Class declaration
    public class MenuPanelManager : MonoBehaviour
    {
        // Serialized fields for Unity inspector
        [SerializeField] private GameObject startPanel; // Panel for start menu
        [SerializeField] private GameObject startGameButton; // Button to start the game

        [SerializeField] private GameObject optionsPanel; // Panel for options menu
        [SerializeField] private GameObject settingButton; // Button to access settings

        [SerializeField] private GameObject loadPanel; // Panel for load game menu
        [SerializeField] private Button loadButton; // Button to load a game

        [SerializeField] private EventSystem eventSystem; // Event system for UI navigation
        [SerializeField] private PlayerInput playerInput; // Player input reference
        [SerializeField] private List<OptionButtonHover> optionButtonHovers = new(); // List of option button hovers
        [SerializeField] private List<GameObject> mainPanels; // List of main panels
        public event Action OnEscapeAction; // Event triggered on escape action

        [SerializeField] private SceneIndex sceneIndex; // Index of the current scene
        [SerializeField] private GameIndex gameIndex; // Index of the current game
        [SerializeField] private PauseCondition pauseCondition; // Pause condition reference
        [SerializeField] private GameObject backButton; // Button to go back

        // Awake method called before Start
        private void Awake()
        {
            // Switch to determine initial panel based on scene index
            switch (sceneIndex.Index)
            {
                case 0: // If it's the start menu scene
                    startPanel.SetActive(true);
                    optionsPanel.SetActive(false);
                    loadPanel.SetActive(false);
                    break;
                case > 0: // If it's not the start menu scene
                    startPanel.SetActive(false);
                    optionsPanel.SetActive(true);
                    loadPanel.SetActive(false);
                    break;
            }
        }

        // Method called when the script instance is enabled
        private void OnEnable()
        {
            // Subscribe to back action event
            if (playerInput != null) playerInput.actions["Back"].performed += BackWithAction;
        }

        // Method called when the script instance is disabled
        private void OnDisable()
        {
            // Unsubscribe from back action event
            if (playerInput != null) playerInput.actions["Back"].performed -= BackWithAction;
        }

        // Method to handle back action
        private void BackWithAction(InputAction.CallbackContext context)
        {
            // Check if any option button hover is active or back button is selected
            for (var i = 0; i < optionButtonHovers.Count; i++)
            {
                if (optionButtonHovers[i].ActiveIndex == -1 && mainPanels[i].activeSelf 
                    || eventSystem.currentSelectedGameObject == backButton)
                {
                    // Invoke escape action event and switch to default panel
                    OnEscapeAction?.Invoke();
                    SwitchPanels(0);
                }
            }
        }

        // Method to switch between panels
        public void SwitchPanels(int index)
        {
            // Deactivate all panels
            if (startPanel.activeSelf)
                startPanel.SetActive(false);
            if (optionsPanel.activeSelf)
                optionsPanel.SetActive(false);
            if (loadPanel.activeSelf)
                loadPanel.SetActive(false);
            
            // Check scene index to determine panel behavior
            if (sceneIndex.Index == 0) // If it's the start menu scene
            {
                switch (index)
                {
                    case 0: // Start panel
                        startPanel.SetActive(true);
                        eventSystem.SetSelectedGameObject(startGameButton);
                        break;
                    case 1: // Load panel
                        loadPanel.SetActive(true);
                        eventSystem.SetSelectedGameObject(loadButton.gameObject);
                        break;
                    case 2: // Options panel
                        optionsPanel.SetActive(true);
                        eventSystem.SetSelectedGameObject(settingButton);
                        break;
                }
            }
            else if (sceneIndex.Index > 0) // If it's not the start menu scene
            {
                pauseCondition.isOptionPressed = false; // Reset option press flag
                ReturnToGame(); // Return to the game scene
            }
        }

        // Method to start a new game with selected index
        public void StartNewGame(int index)
        {
            gameIndex.Index = index; // Set game index
        }

        // Method to load a game with selected index
        public void LoadGameWithButtonIndex(int index)
        {
            gameIndex.Index = index; // Set game index
        }

        // Method to return to the game scene
        private void ReturnToGame()
        {
            SceneManager.UnloadSceneAsync("StartMenu"); // Unload the start menu scene
        }
    }
}
