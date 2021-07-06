---
description: Define the rules
---

# Game

A game state encapsulate the rules of a game and exposes common methods required by the IGameState interface.

```csharp
public interface IGameState<TMove, out TResult, out TPlayer>
{
    TPlayer CurrentPlayer { get; }

    TMove[] AvailableMoves { get; }

    TResult Result { get; }

    IGameState<TMove, TResult, TPlayer> Move(TMove move);
}
```

### TMove

A type used to represent a move in the game. For simpler games like noughts and crosses, this is often an integer representing the index of a tile on the grid.

### TPlayer

A type used to represent the player. Advanced applications would be requiring players of a game to handle special commands. E.g. ??

### TResult

A type used to represent the end result of a game. Faze provides some predefined types which are common across games like WinLoseDrawResult. 

You may want to define your own result type for your game. The benefits of this can be:

* Use to collect additional information about the end state. E.g. in chess you could have a 'IsCheckmate' flag.
* Handle multiple winners and querying the result for a given player.

