# TLHelper for Diablo III

## Features to be implemented:

- [ ] Adjust Witch Doctor Skills

  - Remove Sacrifice
  - Remove Fetish Army
  - Add Big Bad Voodoo
  - Add Hex

- [ ] Change Baseresolution to 1920x1080

- [ ] Disable Console by Default  
       Only show Console if defined in Config

- [ ] Add Support for XButton1 and XButton2

- [ ] Change MouseMove and MouseClick to use PostMessage

- [x] Check if the CTRL-Problem is causing the CubeCoverter problems

- [ ] Change key input:  
       Replace TextField with a Button. After pressing the Button the next Key will be registered (Mouse and Keybard)

- [ ] Implement the HotkeySelector Window:  
       Implement the Window to select Keycombinations to launch Scripts
      Save selected Hotkeys in hotkeys.ini (hotkeys.ini overrides the scripts default keys)

- [ ] Add Mode Control

  - F1: Always press Keys
  - F2: Press Keys inside GRs and Rifts
  - F3: Never press Keys

- [ ] Kadala Autogamble:  
       If activated, gamble a defined Item every Time Kadala's Window is opened

- [ ] Auto Gemup:  
       Automaticaly upgrade Gems at the end of a GR

- [ ] Profiles:  
       Save and Load Skill-Profiles

- [ ] Auto Rift Opener:  
       Automatically open a Rift or GR when Orek's Widow is opened
      (later) Open a defined GR-Level via Text Recognition

- [ ] Add Auto-Reforger:  
       Add a Script to automatically reforge a selected Stat on an Item

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

- [ ] Multiple Auto-Ports:  
       Set multiple Hotkeys to port to different Locations

- [ ] Add Cube-Functionality to reforge
      Add functionality to reforge the Item in the top-left Corner of the Inventory

- [ ] Hotkeys to launch / kill 4th-Party programs
      Add a Window to define Programs to be launched / killed on Hotkey
      Let scripts start / kill Processes and Programs

## Bugs to be fixed:

- [x] Fix Auto Potion  
       Fix the way the Potion key is pressed to not interfere Shift

- [x] Macro Hotkeys  
       Make sure CTRL is released after Hotkey completed

- [ ] Shift on LMB  
       If needed press Shift for LMB-Actions

- [x] Create config if not existing  
       If no config exists in documents/TLHelper/config.ini, create an empty one

- [ ] Fix Skill-Slot 2:  
       Skill Slot 2 not working for any Reason

- [x] Fix Converter Scripts
      Fix the 1- and 2-Slot Converting Scripts (unregister and register MouseHandles before and after the Script)

## Better Script Support:

- [ ] Add variables
- [ ] Add functions
- [ ] Add a function to download Scripts from Pastebin and Github
- [ ] Provide ingame-variables to Scripts (%Life or SkillReady)
- [ ] Provide helper-functions to Scripts (InventoryItterator or change current Class)

## Website for TLHelper

[TLHelper Website / Online Portal](https://github.com/FischerEnterprise/tlhelper-temp/blob/master/WEBSITE.md)
