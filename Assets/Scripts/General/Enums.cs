using System;

public enum CameraModes
{
    FPS, AroundPoint,
}

public enum PlayerActiveState
{
    Normal, RushJet,
}

public enum RushMode
{
    None, RushJetAwaiting, RushJet, RushCoil
}

public enum ItemSpawnMode
{ 
    Random, Concrete
}
public enum EnemyState
{
    Idle, Attack,
}