---
description: Game agent
---

# IPlayer

An agent is responsible for evaluating and assigning a confidence against all available moves. This is achieved by returning an IMoveDistribution object.

```csharp
public interface IPlayer
{
    IMoveDistribution<TMove> GetMoves<TMove, TResult>(IGameState<TMove, TResult> state);
}
```

See [Agents ](../engine/agents.md)for documentation on the predefined implementations of IPlayer.



## IMoveDistribution

An IMoveDistribution abstracts choosing a random move from a distribution, where the chance of picking a move is weighted by the agent's assigned confidence.

```csharp
public interface IMoveDistribution<TMove>
{
    TMove GetMove(UnitInterval ui);
    
    bool IsEmpty();
}
```

{% hint style="info" %}
The `UnitInterval`type represents a number between 0 and 1 inclusive.
{% endhint %}

