using System;

[Serializable]
public enum MenuType
{
    Loading = 0, 
    Main,
    CreateRoom, 
    Room, 
    Error,
    FindRoom,
    Settings,
    EnterPlayerName,
    Multiplayer,
    Count
};