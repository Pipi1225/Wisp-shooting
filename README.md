# 🌠 Wisp-Shooting

> **🎮 [PLAY THE GAME HERE](https://pipi1225.github.io/Wisp-shooting/)**
> 
> **Attention:** This game is still under development. Please play the game in fullscreen for the best experience. Have fun!

![Gameplay Overview](https://github.com/user-attachments/assets/630f9b1f-b896-40b3-84e8-f6d9aff8cef4)
---

## 🎯 How the Game Works
Your goal in this game is to stay alive as long as possible while killing enemies to get points. 
You can start the game by jumping off the cliff.

## 🖥️ User Interface
The screen is divided into two main information sections:

* **Top Left:** Health bar, Projectile counter, Projectile cooldown bar, and Dash cooldown bar.
  
  <img width="600" alt="Top Left UI Elements" src="https://github.com/user-attachments/assets/ec563692-7c4e-4d5a-8b5b-d0e8a47b5edf" />

* **Top Right:** Point counter.

  <img width="600" alt="Top Right UI Elements" src="https://github.com/user-attachments/assets/cd5afef5-a90b-43c0-80e9-5ba379fe87d8" />

---

## 🕹️ Movement & Controls

* **Move:** Press `A` / `D` or `Left` / `Right` arrow keys.
<video src="https://github.com/user-attachments/assets/4a91834a-49b3-4751-8055-d233eb79dd75" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

* **Jump:** Press `Backspace` to jump. While jumping, press `Backspace` again to perform a **Double Jump**.
<video src="https://github.com/user-attachments/assets/04a4224a-12eb-4e64-ad67-e32deda33abb" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

* **Dash:** Press `Left Ctrl` to dash. *(Note: While dashing, you cannot perform other actions and are vulnerable to damage).*
<video src="https://github.com/user-attachments/assets/25aa730a-8491-4081-86ad-f92d5eb2e8f9" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

---

## ⚔️ Attack Mechanics

* **Melee Attack:** Press `Z` to slash.
<video src="https://github.com/user-attachments/assets/80b0efa8-c8c5-457e-a166-5162b01b907e" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

* **Create Projectile:** Press the `Right Mouse Button` to create a projectile of light (can be cast while moving).
    * *Limit:* Only **4 projectiles** can exist at the same time. If you create a 5th one, the oldest one will explode immediately.
<video src="https://github.com/user-attachments/assets/de2e0f8f-773f-4e52-b187-67dbc2dd87ee" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>
<video src="https://github.com/user-attachments/assets/d9668d29-241a-4760-bd03-ccb611b69fd8" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

* **Control Projectile:** Hold the `Left Mouse Button` to pull a projectile. 
    * Its trajectory will be displayed on the screen. 
    * Change direction and applied force by moving your cursor after clicking (maximum force is capped).
<video src="https://github.com/user-attachments/assets/fa470f6d-db6e-469d-95f7-89dee983e6b1" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>
<video src="https://github.com/user-attachments/assets/783b0fbe-70b9-44f5-b709-45bc24bed4e5" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

---

## 🎒 Inventory & Shop

* **Shop:** You can open/close the shop inventory by pressing the prompt key when it shows up.
<video src="https://github.com/user-attachments/assets/e4a460c8-820e-4da3-9176-aa52e5870761" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

* **Inventory:** You can open/close your personal inventory by pressing `I`.
<video src="https://github.com/user-attachments/assets/9101e97a-7bc3-4d40-b8a7-15c9a875c667" controls="controls" muted="muted" autoplay="autoplay" loop="loop"></video>

---

## 👾 Enemies & Stats

**Player HP:** You start with **100 HP**. Once your HP reaches 0, the game is over. 

There are currently 5 types of enemies:

* 🟢 **Soldier Slime:** Jumps toward you and hurts you by contact damage. 
    * **Damage:** -20 HP 
    * **Reward:** +20 points
* 🏹 **Ranger Slime:** Jumps toward you until within range, then shoots dark energy balls at your position. 
    * **Damage:** -10 HP (Contact & Projectile) 
    * **Reward:** +20 points
* 🦇 **Blop:** Hovers toward you and attacks with its spikes. 
    * **Damage:** -10 HP 
    * **Reward:** +30 points
* 🪰 **Fly:** Flies continuously toward you. It is extremely fragile and will be destroyed in a single hit.
    * **Damage:** -15 HP
    * **Reward:** +20 points
* 💣 **Spawner:** Slowly approaches you. Once it gets close enough, it explodes and leaves behind a hazardous zone that deals continuous damage over time.
    * **Damage:** -30 HP (Continuous Area of Effect)
    * **Reward:** +20 points

**Loot Drop:** After killing a monster, there is a **10% chance** it will drop a Health Potion. Touch the potion to restore **40 HP** (Up to the maximum of 100 HP).

---

## 🚀 Future Updates
* [ ] **Skill Tree System:** Coming soon!
