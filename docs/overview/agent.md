# Agent

An agent is responsible for choosing between available moves.

```csharp
public interface IPlayer
{
    TMove ChooseMove<TMove, TResult, TPlayer>(IGameState<TMove, TResult, TPlayer> state);
}
```

## Predefined Agents



### Monkey

The simplest kind of agent, it will pick a random move from all available.

#### Requires

* Set of available moves

### Minimax

Performs the [Minimax ](https://en.wikipedia.org/wiki/Minimax)algorithm and chooses the best move. Takes an argument 'foresight', which defines the max number of moves ahead the agent will look.

#### Requires

* Set of available moves
* Get the next state of the game for a given move
* Game result comparator



## Considerations

{% hint style="warning" %}
What should an agent do?

* Return its chosen move
* Return a probability/confidence against all available moves
{% endhint %}

{% hint style="warning" %}
How to handle exponentially large available move sets?

It may not be feasible to list all the available moves. This could still be visualised if there was some mapping from a move to the image.

* An agent would have to know information specific to the game itself and produce a move or set of moves according to some heuristic
{% endhint %}

