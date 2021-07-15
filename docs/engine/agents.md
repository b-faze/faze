---
description: Predefined implementations of IPlayer
---

# Agents

## Monkey

[Faze.Engine.Players.MonkeyAgent.cs](https://github.com/b-faze/faze/blob/78454a4f3a571e8fdc321855094402a153871759/src/libraries/Faze.Engine/Faze.Engine/Players/MonkeyAgent.cs)

The simplest kind of agent, it will return a uniform move distribution of all available moves.



## Minimax

[Faze.Engine.Players.MinimaxAgent.cs](https://github.com/b-faze/faze/blob/78454a4f3a571e8fdc321855094402a153871759/src/libraries/Faze.Engine/Faze.Engine/Players/MonkeyAgent.cs)

Performs the [Minimax ](https://en.wikipedia.org/wiki/Minimax)algorithm and chooses the best move. Takes an argument 'foresight', which defines the max number of moves ahead the agent will look.



