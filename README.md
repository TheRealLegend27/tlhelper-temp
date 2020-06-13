# TLHelper for Diablo III

## Features to be implemented:

- [ ] Adjust Witch Doctor Skills

  - Remove Sacrifice
  - Remove Fetish Army
  - Add Big Bad Voodoo
  - Add Hex

- [ ] Change Baseresolution to 1920x1080

- [x] Disable Console by Default  
       Only show Console if defined in Config

- [x] Add Support for XButton1 and XButton2

- [x] Change MouseMove and MouseClick to use PostMessage

- [x] Check if the CTRL-Problem is causing the CubeCoverter problems

- [x] Change key input:  
       Replace TextField with a Button. After pressing the Button the next Key will be registered (Mouse and Keybard)

- [x] Implement the HotkeySelector Window:  
       Implement the Window to select Keycombinations to launch Scripts
      Save selected Hotkeys in hotkeys.ini (hotkeys.ini overrides the scripts default keys)

- [x] Add Mode Control

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

- [ ] Class recognition:  
       Automatically detect the current class

- [ ] Multiresolution Support / Windowed Mode Support  
       Add Support for different Resolutions and Windowed Mode

- [ ] Better Scripts  
       Change the way Scripts work (more below)

- [ ] Skill recognition:  
       Automatically detect current Skills when not in GR or Rift every 10 seconds

- [x] Remove Save Button:  
       Automatically save on change and remove Save Button

- [x] Start / End THud and Helper with D3:  
       Register a Process to automatically start TurboHUD and TLHelper when Diablo III is launched  
       Register a Process to automatically stop TLHelper when Diablo III is closed

- [ ] Add Settings:  
       Add Settings to change a Skills behavior (e.g. Press on CD or on not active)  
       Add Settings to change Script behavior (e.g. Global min Sleep)

- [ ] Implement Essence Support:  
       Create availability functions based on %-Essence

- [x] Implement Licence-Keys:  
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

- [x] Add Cube-Functionality to reforge
      Add functionality to reforge the Item in the top-left Corner of the Inventory

- [ ] Hotkeys to launch / kill 4th-Party programs
      Add a Window to define Programs to be launched / killed on Hotkey
      Let scripts start / kill Processes and Programs
- [ ] cancel running scripts when ESC pressed

- [ ] add settings for ingame buttons (open map, close all windows, etc.)

- [x] add support for AHK scripts
  - ahk scripts can be created and added like normal scripts
- [ ] add online settings

  - settings are stored online and can be edited on the website, aswell as in TLHelper itself
  - settings are synchronized when the helper starts
  - changed settings are synchronized when the helper exits

- [ ] move ticketing to website

  - create an own ticketing system on the website

- [ ] trial license MAC check

  - allow only one trial license per device
  - if a device already used a trial before, trial licenses wont be accepted

- [ ] change the appearence of error popups

  - make the popups bigger
  - split error-code from error-message

- [ ] users can create their own scripts
  - add a function on the website to create new scripts
    - scripts are private by default and can be changed to public
  - add a search function on the scripts page
  - add likes to the scripts page
  - display list-links on the scripts page

## Bugs to be fixed:

- [ ] Shift on LMB **(tlb006)**  
       If needed press Shift for LMB-Actions

<details>
       <summary>Show bug history</summary>

- [x] Fix Auto Potion **(tlb001)** _fixed 05.03.2020_  
       Fix the way the Potion key is pressed to not interfere Shift

- [x] Macro Hotkeys **(tlb002)** _fixed 05.03.2020_  
       Make sure CTRL is released after Hotkey completed

- [x] Create config if not existing **(tlb003)** _fixed 05.03.2020_  
       If no config exists in documents/TLHelper/config.ini, create an empty one

- [x] Fix Skill-Slot 2: **(tlb004)** _fixed 05.03.2020_  
       Skill Slot 2 not working for any Reason

- [x] Fix Converter Scripts **(tlb005)** _fixed 05.03.2020_  
       Fix the 1- and 2-Slot Converting Scripts (unregister and register MouseHandles before and after the Script)

- [x] Helper not starting when no documents/tlhelper/scripts folder is present **(tlb007)** _fixed 10.06.2020_  
       On Startup: check if folder is present, if not create one

</details>

## Better Script Support:

- [ ] Add variables
- [ ] Add functions
- [ ] Add a function to download Scripts from Pastebin and Github
- [ ] Provide ingame-variables to Scripts (%Life or SkillReady)
- [ ] Provide helper-functions to Scripts (InventoryItterator or change current Class)

## Website for TLHelper

[TLHelper Website / Online Portal](https://github.com/FischerEnterprise/tlhelper-website)
