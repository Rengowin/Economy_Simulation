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

## Pictures

### from 18.09
every 5s 1 woodcutter produces 1 wood
<img width="1611" height="897" alt="first production cycle of 5 woodcutters" src="https://github.com/user-attachments/assets/0d1b5957-5a93-4fc8-aef8-2a98b0e0c60a" />

1 sawmill cost 5 wood here is a test to build more, it would produce 1 plank per 2 wood every 5s
<img width="1602" height="901" alt="can Only spawn 1 sawmill since to little wood" src="https://github.com/user-attachments/assets/77864bd2-b461-4cdf-af38-76a13829f856" />

### from 21.09

population growth every 2s for every home that still can
<img width="1442" height="361" alt="showcaseOfPopGrow" src="https://github.com/user-attachments/assets/7a48c1cc-696f-4bdb-ba20-0532ebf290b8" />

right now the populationgrowth is just looking for if 1 population have 1 wood without reducing it next step will be to say somethink like per 100 population you use x ressources (later with more and induviale ressources give they own values maybe :D)
<img width="1422" height="620" alt="PopulationGroth" src="https://github.com/user-attachments/assets/bbf38082-3594-4462-a7dd-c0ae3dd10d97" />



