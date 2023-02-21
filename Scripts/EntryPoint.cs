using Assets.SimpleLocalization;
using Cinemachine;
using Core.States;
using Data;
using Gameplay;
using ReactiveSystem;
using UI;
using UI.UiLocalization;
using UnityEngine;
using UnityEngine.Audio;
using Utilities;

namespace Core
{
    public partial class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Loader2 loader2; 
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private ViewDataBase viewDataBase;
        [SerializeField] private MonoEventBus monoEventBus;
        [SerializeField] private Script script;
        [SerializeField] private AudioPlayer audioPlayer;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private CameraConfig cameraConfig;
        [SerializeField] private LocalizationDownloader localizationDownloader;
        [SerializeField] private DecorationsPlayer decorationsPlayer;
        [SerializeField] private AnswerPlayer answerPlayer;
        [SerializeField] private TutorialPlayer tutorialPlayer;
        [SerializeField] private LocalizationView[] localizationViews;

        private IEventBus eventBus;
        private GameStateMachine gameStateMachine;
        private IServiceContainer serviceContainer;

        private GameStateModel gameModel;
        private SettingsModel settingsModel;

        
        private WindowsMediator windowsMediator;

        private InputActions inputActions;
        private CameraController cameraController;

        private void Awake()
        {
            Debug.Log($"|||<color={C.Highlight}>Start The Game</color>|||");
            
            inputActions = new InputActions();
            serviceContainer = new ServiceContainer();
            serviceContainer.InputActions = inputActions;
            CreateEventBus();
            localizationDownloader.LoadLocalizationText(LoadedResources);
        }

        private void LoadedResources(TextAsset text)
        {
            Localization.FillDictionary(text);
            LoadData();
        }
        private void LoadData()
        {
            loader2.GameModelLoaded += obj => gameModel = obj;
            loader2.SettingsModelLoaded += obj => settingsModel = obj;
            loader2.LoadData(() => InitializeGame());
        }
        private void InitializeGame()
        {
            
            cameraController = new CameraController(inputActions, virtualCamera, cameraConfig);
            
            gameStateMachine = new GameStateMachine(serviceContainer, gameModel);
            
            LoadUI(serviceContainer, gameModel, settingsModel);

            var lineScriptController = new LineScriptController(script, audioPlayer, eventBus, gameModel);
            var decorationsController = new DecorationsController(decorationsPlayer, eventBus, gameModel);
            var answerController = new AnswerController(answerPlayer, tutorialPlayer, script, eventBus, decorationsController, gameModel);
            var commentController = new CommentsController(eventBus, script, lineScriptController, decorationsController, gameModel);
            var stageController = new StageController(gameModel, decorationsController, 
                answerController, lineScriptController, script, eventBus, windowsMediator);
            var skipper = new Skipper(lineScriptController, commentController, decorationsController);
            
            var stateContainer = new StateContainer(gameStateMachine, skipper, stageController);
            gameStateMachine.InjectStates(stateContainer);

            StartGame();
        }

        private void Update()
        {
            gameStateMachine?.Tick();
            cameraController?.Tick();
        }
        private void CreateEventBus()
        {
            eventBus = new EventBus();
            serviceContainer.EventBus = eventBus;
            monoEventBus.Init(eventBus);
        }
        
        private void LoadUI(IServiceContainer container, GameStateModel gameModel, SettingsModel settingsModel)
        {
            viewDataBase.Init();
            windowsMediator = new WindowsMediator(viewDataBase, container, gameStateMachine);
            container.WindowsMediator = windowsMediator;

            var localizationController =
                new LocalizationController(viewDataBase.GetView<LocalizationMenuView>(), windowsMediator);
            var menuController = new MenuController(viewDataBase.GetView<MenuView>(), container, gameModel, script, windowsMediator);
            var settingsController = new SettingsController(viewDataBase.GetView<SettingsView>(), mixer, settingsModel, windowsMediator, localizationViews);
            var creditsController = new CreditsController(viewDataBase.GetView<CreditsView>(), windowsMediator);
            var helpController = new HelpController(viewDataBase.GetView<HelpView>(), windowsMediator);
        }

        private void StartGame()
        {
            windowsMediator.OpenWindow(WindowType.Localization);
        }

        private void OnApplicationQuit()
        {
            DataSystem.SaveFileJson("GameModel", gameModel);
            DataSystem.SaveFileJson("SettingsModel", settingsModel);
        }
    }
}