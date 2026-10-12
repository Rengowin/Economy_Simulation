# Economy Simulation

A small Unity/C# project inspired by economy and production systems from games
such as Anno. The project currently focuses on implementing and learning the
underlying gameplay and simulation systems before working on visuals. Visuals
may be added later using simple assets or custom models.

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

- **Population System:** Residential buildings track their current and maximum
  population. Population grows over time, with the growth speed currently
  influenced by the availability of a test resource.

- **Population Needs:** Population needs are defined using ScriptableObjects, allowing different resources to have configurable consumption rates and population requirements for unlocking. The PopulationNeedsController tracks individual needs and consumes resources from storage over time.
  
## Current Development State

The project currently focuses on gameplay logic and system architecture.
Production, resource storage, building costs and basic population growth are
implemented and connected.
The project currently includes production, resource storage, building construction, basic population growth, and an initial population needs system.

Population needs can now be unlocked based on population size and consume resources over time. Wood is currently used as the first test resource. Population growth still uses the previous resource-availability calculation and has not yet been integrated with the new needs system.

Visuals are intentionally kept as simple placeholder cubes while the core
simulation systems are developed.

## Pictures

### 18.09

Each Woodcutter produces 1 Wood every 5 seconds.

<img width="1611" height="897" alt="First production cycle of 5 Woodcutters" src="https://github.com/user-attachments/assets/0d1b5957-5a93-4fc8-aef8-2a98b0e0c60a" />

A Sawmill costs 5 Wood. This test attempts to build multiple Sawmills while
only having enough resources to build one. A Sawmill consumes 2 Wood and
produces 1 Plank every 5 seconds.

<img width="1602" height="901" alt="Only one Sawmill can be spawned because there is not enough Wood" src="https://github.com/user-attachments/assets/77864bd2-b461-4cdf-af38-76a13829f856" />

### 21.09

Basic population growth test. Each residential building gains 1 population
every 2 seconds until reaching its maximum population.

<img width="1442" height="361" alt="Basic population growth test" src="https://github.com/user-attachments/assets/7a48c1cc-696f-4bdb-ba20-0532ebf290b8" />

First resource-dependent population growth test. For now, 1 Wood per current
population is treated as 100% resource availability. Resources are not consumed
yet. If less Wood is available, the population growth rate is reduced
proportionally.

The next step is to implement actual resource consumption, for example a
certain amount of a resource per 100 population over a defined time period.
Different needs can later have their own consumption rates.

<img width="1422" height="620" alt="Resource-dependent population growth test" src="https://github.com/user-attachments/assets/bbf38082-3594-4462-a7dd-c0ae3dd10d97" />

### 12.10

Implemented the first version of the population needs system. Needs are defined through ScriptableObjects and can be unlocked based on population size.

Each need tracks its resource consumption over time, including fractional amounts. Once enough consumption has accumulated, whole resource units are removed from storage.

The first test uses 100 population consuming 1 Wood per minute. The next step is to connect the needs system to population growth and handle insufficient resources.

<img width="1550" height="332" alt="populationEatsNowStuff" src="https://github.com/user-attachments/assets/273b9f25-dc19-4799-aebd-8c6b5fd49133" />

