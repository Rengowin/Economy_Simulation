# Economy Simulation

A small Unity/C# project inspired by economy and production systems from games
such as Anno. The project currently focuses on implementing and
learning the underlying gameplay systems (before working on visuals maybe will work with assces later or make somethink simple later but im not sure).

## Core Features

- **Central Tick System:** Gameplay systems can register with a central
  TickController instead of requiring their own Update() loops.

- **Data-Driven Production:** Production buildings use reusable recipes to
  define their inputs, outputs and production time. Currently implemented
  examples include a Woodcutter and Sawmill.

- **Resource Storage:** A global storage system manages available resources
  used by production and construction.

- **Building System:** Buildings use reusable building data and construction
  costs. The BuildingManager checks whether the required resources are
  available before spawning a building and deducting its cost.

- **Input System:** Unity's Input System is used for test controls and
  spawning buildings during development.

- **Population System (Early Development):** Basic residential/consumer
  buildings and population tracking are currently being implemented.

## Current Development State

The project currently focuses on gameplay logic and system architecture.
Visuals are intentionally kept as simple placeholder cubes while the core
simulation systems are developed.
