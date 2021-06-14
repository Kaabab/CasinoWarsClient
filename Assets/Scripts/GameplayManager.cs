using UnityEngine;
using System;

public class GameplayManager : MonoBehaviour
{
    public EventsView EventsView;
    public LoadingView LoadingView;
    public CreatePlayerView CreatePlayerView;
    public TableView TableView;

    private CasinoWarsClient client;
    private PreferencesManager preferencesManager;
    // Use this for initialization
    void Start()
    {
        preferencesManager = new PreferencesManager();
        client = new CasinoWarsClient();
        
        Init();        
    }

    private void OnDestroy()
    {
        RemoveListeners();
    }

    private void RemoveListeners()
    {
        GameplayEvents.BetResponse -= OnBetResponse;

        GameplayEvents.CreateGameSession -= OnGameSessionCreated;
        GameplayEvents.CreatePlayerResponse -= OnPlayerCreated;
        GameplayEvents.GetPlayerResponse -= OnPlayerRetrieved;
    }

    private void OnGameSessionCreated(string id)
    {
        SetGameSession(id);
    }

    private void OnBetResponse(BetResponse response, Player player)
    {
        if (player != null)
        {
            TableView.player = player;
        }
        TableView.UpdateView();
        LoadingView.SetVisible(false);
        if (response != null) {
            TableView.ShowBetResponse(response);
        }
    }
    
    private void SetGameSession(string gameSessionId) {
        TableView.gameSessionId = gameSessionId;
        TableView.UpdateView();
        TableView.SetVisible(true);
        LoadingView.SetVisible(false);
        TableView.ShowBets();
    }

    private void OnPlayerRetrieved(Player player)
    {
        SetPlayer(player);
    }

    private void OnPlayerCreated(Player player)
    {
        SetPlayer(player);
    }

    private void SetPlayer(Player player) {
        CreatePlayerView.SetVisible(false);
        preferencesManager.SetPlayerId(player.id);
        preferencesManager.SetPlayerName(player.name);


        String playerId = preferencesManager.GetPlayerId();
        String playerName = preferencesManager.GetPlayerName();
        EventsView.Log(string.Format("Player with id {0} And name {1} Loaded", playerId, playerName));

        EventsView.Log("Retrieving game session");
        TableView.player = player;

        TableView.UpdateView();

        String gameSessionId = preferencesManager.GetGameSessionId();
        if (gameSessionId == null || gameSessionId == "")
        {
            EventsView.Log("Creating game session");
            client.CreateGameSession(playerId);
        }
        else
        {
            SetGameSession(gameSessionId);
        }
    }

    private void AddListeners()
    {
        GameplayEvents.CreateGameSession += OnGameSessionCreated;
        GameplayEvents.CreatePlayerResponse += OnPlayerCreated;
        GameplayEvents.GetPlayerResponse += OnPlayerRetrieved;
        GameplayEvents.BetResponse += OnBetResponse;
    }

    private void Init()
    {
        AddListeners();
        EventsView.SetVisible(true);
        LoadingView.SetVisible(false);
        String playerId = preferencesManager.GetPlayerId();
        if (playerId == null || playerId == "")
        {
            CreatePlayerView.SetVisible(true);
        }
        else
        {
            LoadingView.SetVisible(true);
            EventsView.Log("Loading player");
            client.GetPlayer(playerId);           
        }
    }
    
    public void CreatePlayer()
    {
        client.CreatePlayer(CreatePlayerView.playerName.text);
    }

    public void PlaceBet() {
        try
        {
            int bet = Int32.Parse(TableView.PlaceBetView.betAmount.text);
            int tieBet = Int32.Parse(TableView.PlaceBetView.tieBetAmount.text);
            EventsView.Log(string.Format("Placing bet {0}, tieBet {1}", bet, tieBet));

            client.PlaceBet(new BetRequest
            {
                bet = bet,
                tieBet = tieBet,
                playerId = TableView.player.id
            }, TableView.gameSessionId);
            LoadingView.SetVisible(true);
           }
        catch (Exception) {
            Debug.LogError("Could not process bet");
        }
    }
}
