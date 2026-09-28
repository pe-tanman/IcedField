# 🐧 PenguinField (IcedField)

> *Four penguins, one ice field — claim the most ground to win.*

[![Play on Unity Play](https://img.shields.io/badge/▶%20Play%20now-Unity%20Play-000000?logo=unity)](https://play.unity.com/en/games/46f31512-425d-4490-805e-b511bd9733a2/webglpenguin)
![Unity](https://img.shields.io/badge/Unity-6000.1-black?logo=unity)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)


## 🌟 Highlights

- 🎮 **Play in your browser** — nothing to install, just [open it on Unity Play](https://play.unity.com/en/games/46f31512-425d-4490-805e-b511bd9733a2/webglpenguin)
- 👥 **Couch multiplayer** — 2 or 4 players on one keyboard (plus mouse)
- 🧊 **Paint the ice** — every tile you slide over turns your color
- 💥 **Push and surround** — shove rivals off contested ground and enclose areas to take them over
- 🔐 **No data collected** — the game itself stores no personal information


## ℹ️ Overview

PenguinField is a local-multiplayer territory game. Each player steers a penguin across an ice field, claiming tiles in their own color. You can bump opponents out of the way and surround regions to fill them in. When the timer runs out, the player controlling the largest area wins.

It's built with Unity and C#. The main systems are area claiming and flood-fill, penguin-to-penguin pushing, a split-screen camera over a shared map, and four-player input on a single keyboard.


### ✍️ Author

Designed and developed solo by [Yuki Ishihara](https://github.com/pe-tanman). More projects on my [portfolio](https://portfolio-pe-tanmans-projects.vercel.app).


## 🚀 How to Play

👉 **[Play PenguinField on Unity Play](https://play.unity.com/en/games/46f31512-425d-4490-805e-b511bd9733a2/webglpenguin)**

| Mode | Controls |
| --- | --- |
| 2 players | `WASD` · `Arrow keys` |
| 4 players | `WASD` · `YGHJ` · `Arrow keys` · Mouse |

- Slide over tiles to paint them your color.
- Push opponents away and surround areas to take them over.
- The largest territory when time runs out wins.
- Open **Help** from the main menu for the full rules.


## ⬇️ Running from Source

Requirements: **Unity 6 (6000.1.5f1)** or newer.

1. Clone the repository:
   ```bash
   git clone https://github.com/pe-tanman/IcedField.git
   ```
2. Open the folder in Unity Hub (**Add → Add project from disk**).
3. Open the main scene and press **Play**, or build for **WebGL** via *File → Build Profiles*.


## 🔒 Privacy Policy

PenguinField does not collect any personal information. Unity may collect your username or email when you sign in to Unity Play. See [Unity's Privacy Policy](https://unity.com/legal/privacy-policy) for details.


## 💭 Feedback and Contributing

Bugs, balance ideas or new map suggestions are all welcome. [Open an issue](https://github.com/pe-tanman/IcedField/issues) or submit a pull request.
