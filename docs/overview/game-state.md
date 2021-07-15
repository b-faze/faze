---
description: Define the rules
---

# IGameState

A game state encapsulate the rules of a game and exposes common methods required to play it.

{% hint style="info" %}
A game state should be made immutable.
{% endhint %}

```csharp
public interface IGameState<TMove, out TResult>
{
    PlayerIndex CurrentPlayerIndex { get; }

    IEnumerable<TMove> GetAvailableMoves();

    TResult GetResult();

    IGameState<TMove, TResult> Move(TMove move);
}
```

## Generic Types

### TMove

A type used to represent a move in the game. For simpler games like noughts and crosses, this is often an integer representing the index of a tile on the grid. There is a type available for representing a tile index called GridMove.

### TResult

A type used to represent the end result of a game. For games still in progress, a null value should be returned. Faze provides some predefined types which are common across games like WinLoseDrawResult. 

You may want to define your own result type for your game. The benefits of this can be:

* Use to collect additional information about the end state. E.g. in chess you could have a 'IsCheckmate' flag.
* Handle multiple winners and querying the result for a given player.

Special tree result mappers can be defined to aggregate the data in a game's result.

## Members

### CurrentPlayerIndex

```csharp
PlayerIndex CurrentPlayerIndex { get; }
```

A game state is responsible for keeping track of who the current player is. Some games will have a fixed number of players or will initialised with a value. The PlayerIndex type encapsulate this value and is essentially an id which uniquely identifies a player. It is up to the caller which player that id will map to.

### GetAvailableMoves

```csharp
IEnumerable<TMove> GetAvailableMoves();
```

This method will return all available moves the current player can make. This, in conjunction with the `Move` method is used to create the game tree from a state.

This should return an empty list if the game has ended

{% hint style="warning" %}
Currently there is only support for games with a finite move set and there is an assumption that all the moves are only available for the current player.
{% endhint %}

### GetResult

```csharp
TResult GetResult();
```

Null if the game is still in progress, otherwise a value will signal the end of the game.

### Move

```csharp
IGameState<TMove, TResult> Move(TMove move);
```

Returns the next state given a move. This method should not alter the original game state in any way.

## Example

The example code below shows a very simple one player game `LeftOrRightHand`. There are two available moves, pick the left hand \(true\) or the right \(false\). If you guess correctly you win \(true\), otherwise you lose \(false\).

```csharp
public class LeftOrRightHand : IGameState<bool, bool?>
{
    private readonly bool leftHand;
    private readonly bool? result;

    public static LeftOrRightHand Initial(bool leftHand) 
        => new LeftOrRightHand(leftHand, result: null);

    private LeftOrRightHand(bool leftHand, bool? result)
    {
        this.leftHand = leftHand;
        this.result = result;
    }

    public PlayerIndex CurrentPlayerIndex => PlayerIndex.P1;

    public IEnumerable<bool> GetAvailableMoves() => new[] { true, false };

    public bool? GetResult() => result;

    public IGameState<bool, bool?> Move(bool move)
    {
        return new LeftOrRightHand(leftHand, move == leftHand);
    }
}
```



