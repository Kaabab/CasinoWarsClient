using Proyecto26;
using System;
using UnityEngine;

public class CasinoWarsClient
{
    const string TARGET_SERVER = "http://localhost:8080";
    const string GET_PLAYER = TARGET_SERVER  + "/player/{0}";
    const string CREATE_PLAYER = TARGET_SERVER + "/player";
    const string CREATE_GAME = TARGET_SERVER + "/game";
    const string PLACE_BET = TARGET_SERVER + "/game/{0}/bet";

    public void GetPlayer(string playerId) {

        RequestHelper currentRequest = new RequestHelper
        {
            Uri = String.Format(GET_PLAYER, playerId),
            EnableDebug = true
        };
        RestClient.Get<Player>(currentRequest)
        .Then(res => {
            GameplayEvents.DispatchGetPlayerResponse(res);
            Debug.Log("Success" + JsonUtility.ToJson(res, true));
        })
        .Catch(err => 
            Debug.LogError("Error " + err)
        );
    }

    public void CreatePlayer(string playerName)
    {
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = CREATE_PLAYER,
            EnableDebug = true,
            Body = new PlayerCreationRequest
            {
                name = playerName
            },
        };
        RestClient.Post<Player>(currentRequest)
        .Then(res => {
            GameplayEvents.DispatchGetPlayerResponse(res);
            Debug.Log("Success" + JsonUtility.ToJson(res, true));
        })
        .Catch(err =>
            Debug.LogError("Error " + err)
        );
    }

    public void CreateGameSession(string playerId)
    {
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = CREATE_GAME,
            EnableDebug = true,
            Body = new GameCreationRequest
            {
                playerid = playerId
            },
        };
        RestClient.Post<GameSession>(currentRequest)
        .Then(res => {
            GameplayEvents.DispatchCreateGameSessionResponse(res.id);
            Debug.Log("Success" + JsonUtility.ToJson(res, true));
        })
        .Catch(err =>
            Debug.LogError("Error " + err)
        );
    }


    public void PlaceBet(BetRequest betRequest, String sessionId)
    {
        RequestHelper currentRequest = new RequestHelper
        {
            Uri = String.Format(PLACE_BET, sessionId),
            EnableDebug = true,
            Body = betRequest
        };
        RestClient.Post<BetResponse>(currentRequest)
        .Then(bet => {

            RequestHelper playerRequest = new RequestHelper
            {
                Uri = String.Format(GET_PLAYER, betRequest.playerId),
                EnableDebug = true
            };
            RestClient.Get<Player>(playerRequest)
            .Then(player =>
            {
                GameplayEvents.DispatchBetResponse(bet, player);

            });
        })
        .Catch(err =>
        {
            GameplayEvents.DispatchBetResponse(null, null);
            Debug.LogError("Error " + err);
        }
        );
    }
}
