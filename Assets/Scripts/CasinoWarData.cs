using System;

[Serializable]
public class Player 
{
    public string id;
    public string tokenCount;
    public string name;
}


[Serializable]
public class GameSession
{
    public string id;
}

[Serializable]
public class PlayerCreationRequest
{
    public string name;
}

[Serializable]
public class GameCreationRequest
{
    public string playerid;
}

[Serializable]
public class BetRequest
{
    public int bet;
    public int tieBet;
    public string playerId;
}

[Serializable]
public class Card
{
    public int id;
    public int value;
    public string name;
}

[Serializable]
public class BetResponse
{
    public Card playerCard;
    public Card casinoCard;
    public String result;
    public int tokenCount;
}
