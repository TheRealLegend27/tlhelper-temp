# TLHelper for Diablo III

## Features to be implemented:
- [ ] Adjust Witch Doctor Skills
  - Remove Sacrifice
  - Remove Fetish Army
  - Add Big Bad Voodoo
  - Add Hex
  
- [ ] Add Mode Control
  - F1: Always press Keys
  - F2: Press Keys inside GRs and Rifts
  - F3: Never press Keys
  
- [ ] Kadala Autogamble:  
  If activated, gamble a defined Item every Time Kadala's Window is opened
  
- [ ] Auto Gemup:  
  Automaticaly update Gems at the end of a GR
  
- [ ] Profiles:  
  Save and Load Skill-Profiles
  
- [ ] Class recognition:  
  Automatically detect the current class
  
- [ ] Multiresolution Support / Windowed Mode Support  
  Add Support for different Resolutions and Windowed Mode
  
- [ ] Better Scripts  
  Change the way Scripts work (more below)
  
- [ ] Skill recognition:  
  Automatically detect current Skills when not in GR or Rift every 10 seconds
  
- [ ] Remove Save Button:  
  Automatically save on change and remove Save Button
  
- [ ] Start / End THud and Helper with D3:  
  Register a Process to automatically start TurboHUD and TLHelper when Diablo III is launched  
  Register a Process to automatically stop TLHelper when Diablo III is closed
  
- [ ] Add Settings:  
  Add Settings to change a Skills behavior (e.g. Press on CD or on not active)
  Add Settings to change Script behavior (e.g. Global min Sleep)
  
- [ ] Implement Essance Support:  
  Create availability functions based on %-Essence
  
- [ ] Implement Licence-Keys:  
  Require a Licence-Key to use the Helper
  
- [ ] Add Streamer Mode:  
  Press Spamming-Skills a bit slower
  Press all Skills with a slightly random Delay
  
- [ ] Add Cookie-Bot support:  
  Send Actions to the Cookie-Bot (Pause / Stop / Restart)
  Get notified if the Bot is done
  Send Actions to the Bot's Diablo (Automatically invite someone, make a new Game, etc.)
  
## Bugs to be fixed:
- [x] Fix Auto Potion  
  Fix the way the Potion key is pressed to not interfere Shift

- [ ] Macro Hotkeys  
  Make sure CTRL is released after Hotkey completed
  
- [ ] Shift on LMB  
  If needed press Shift for LMB-Actions
  
- [ ] Create config if not existing  
  If no config exists in documents/TLHelper/config.ini, create an empty one
  
## Better Script Support:
- [ ] Add variables
- [ ] Add functions
- [ ] Add a function to download Scripts from Pastebin and Github
- [ ] Provide ingame-variables to Scripts (%Life or SkillReady)
- [ ] Provide helper-functions to Scripts (InventoryItterator or change current Class)
