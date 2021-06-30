---
description: Overview
---

# Documentation

Faze can be broken down into the following sub-components:

### Game State

Implements `IGameState` which provides the available moves, a new state given a valid move and the result of a game. With this simple model it is possible for a generic simulation engine to simulate and collect results for any implementation.

### Simulation Engine

A generic service which provides helpful methods to explore and aggregate results for any give `IGameState`.

### Tree Renderer

In charge of rendering images from tree data structures and keeping track of what portion of the tree needs to be rendered for a given viewport.

