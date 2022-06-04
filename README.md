![SLVC banner](https://github.com/DockFrankenstein/SCPSL-voice/blob/main/.github/Images/SLVC%20banner.png)

![Downloads](https://img.shields.io/github/downloads/DockFrankenstein/SCPSL-voice/total?color=%23&label=Downloads&style=flat-square)
![License](https://img.shields.io/github/license/DockFrankenstein/SCPSL-voice?style=flat-square)

# Table of contents
* [Overview](#overview)
* [Setup](#setup)
* [Usage](#usage)
* [Rebinding buttoms](#rebinding-buttons)
* [Changing commands](#changing-commands)
* [FAQ](#faq)
* [Known issues](#known-issues)
* [Possible upcoming features](#possible-upcoming-features)

# Overview

This application allows you to control SCP: Secret Laboratory using voice commands. It supports custom commands and button remapping.

# Setup

Video tutorial: https://youtu.be/9U8PycDEioo

Download the newest avaliable version [here](/releases) and unpack it. After launching, the app will begin the setup process.

When launching for the first time, the app will say: `[Key Serializer] No keybinds detected. Do you want to use the default keybinds?`. If you are using the default keybinds in SL you can press `Y`. If not, press `N` to rebind. You can do this later by executing command `binds_menu`. More about rebinding [here](#rebinding-buttons).

Once the app finishes setup it will say "Ready". From here, you can start speech recognition by executing command `start`.

# Usage

To start and stop speech recognition, run the `start` or `stop` commands.

Now, tab over to SL and trigger a command (e.g. `forward`).

To view the full list of commands run `voice_command_list`.

**Here's a small summary of all commands:**

```
* shutdown speech recognition - shutdowns speech recognition.
* bang - presses the shoot button once (shooting one bullet, using medkit, etc.)
* shoot auto - holds down the shoot button. Trigger the command again to release.
* zoom - holds down the zoom button. Trigger the command again to release.
* flashlight - presses the flashlight button.
* reload - presses the reload button.
* unload - holds down the reload button (unloading guns, disarming grenades).
* cock - cocks revolver (currently, the middle mouse button doesn't work, rebind the cock key to something else).
* interact - presses the interact key (opening doors, lockers).
* weapon - presses the weapon hotkey.
* weapon 2 - presses the second weapon hotkey.
* inventory - presses the inventory button.
* keycard - presses the keycard hotkey.
* medicals - presses the medicals hotkey.
* grenade - presses the grenade hotkey.
* pick up - holds down the interact button for 8100 miliseconds (enough to pick up the micro, not enough to revive someone as 049)
* cancel - presses the zoom button once (canceling using items, disabling radio, etc.)
* begin the process of zip zaping my medical kit please - starts an automated medkit zip zap loop.
* terminate the process of zip zaping my medical kit please - stops the medkit zip zap loop.
* mission commit spectator - rotates the camera to the ground, equips grenade and throws it.
* throw - throws an item.
* jump - jumps.
* stop looking - stops all mouse movement.
* helicopter - starts rotating your mouse in the X axis (keeps rotating you around).
* forward - makes you walk forward.
* backward - makes you walk backward.
* sprint - toggles sprint (it starts walking forward, if you're standing in place).
* stop - stops all movement.
* sneak - toggles sneaking (it starts walking forward, if you're standing in place).
* voice chat - toggles voice chat.
* alt chat - toggles alt chat (radio, 939 voice).
```

# Rebinding buttons

Firstly, enter the binds menu by running the `binds_menu` command. In there you have 4 options: `display key list: 1`, `rebind specific key: 2`, `rebind all keys: 3` and `exit: 4`.

If you only have a couple of keys to rebind, press `B`. You will have to specify the id of the key that you want to rebind. If you don't know it, print the list of all binds by pressing `1` in the binds menu.

In case that you want to rebind every key, press `3`. The app will wait until you press `Return`. It's advisable that you open up another program while you rebind (e.g. notepad, preferably scpsl) to not trigger random actions.

# Changing commands

Currently, there isn't a simple way to change commands in app, so you will have to modify them manually in the configuration file.

Open the app's location. In there you will find a file called `voice-commands`. Open it in notepad and modify the commands.

# FAQ

### Q1: How do you use it as SCP?

A: Currently, playing as SCP is unsupported. However, I plan to add different modes for different SCPs in the future.

### Q2: Why is this a console application?

A: This app was originally created for a simple video. I also wanted to test out my skills in C# console applications. I might do a GUI version in the future if this app every becomes popular (for some reason).

### Q3: Why?

A: Good question. Playing with voice commands is an objectivly inferior way to play SCPSL. However, I've released this thing for people who would like to also laugh at this ridiculous concept even with other people in SL.

# Known issues

* Cannot press the middle mouse button. You will have to rebind your inputs in SL to cock a gun (it's a problem with the API, I can't really fix this).
* The app takes ages to shut down once initialized.

# Possible upcoming features

* Config file for hardcoded values (e.g. medkit zip zap duration).
* SCP support.
* Automatic keybind assigment (the app automatically locates the SL keybinds and converts them to it's own).
