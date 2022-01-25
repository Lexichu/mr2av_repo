...Well, I doubt anyone reads these despite the file name, but I digress.

=====================================
Monster Rancher 2 Advanced Viewer
=====================================
Currently, Monster Rancher 2 Advanced Viewer (MR2AV) supports:
- ePSXe 2.0.5, 
- XEBRA 19/10/02, 
- no$psx 2.0,
- pSX 1.13
- MR2DX v1.0.0.1 (and 1.0.0.2)


----- Special Thanks to:
- Jack of Hearts/JD; for asking me to make this in the first place, knocking on for four years ago!
- Fad; for showing me how to set up a GitHub page, and making the Name Viewer section work with MR2DX!
- anon; for fixing the Move Viewer to work with every monster, as well as adding a Name Edit feature!
- You; for actually reading this!


---- Usage instructions:

- Run Monster Rancher 2 on your emulator of choice from the supported list.
- Start MR2AV.
- Choose the emulator you have running. It will automatically attach itself to the emulator.
- To start viewing monster data, press the "Start Viewer" button. The screen should be populated with monster information.
-- Anything deemed "unusual" will be marked with the relevant box being coloured in pink.

- To stop updating the information, press "Stop Viewer". Currently, the last update will remain on screen.
- To detach MR2AV from the emulator manually, press the "Detach" button.
-- Closing the emulator or MR2AV will auto-detach MR2AV from it.

You will also require the latest .net Framework, or at least version 4.7.2. Download from: https://dotnet.microsoft.com/download/dotnet-framework-runtime/



---- Version History:

-- v0.10: Initial release.
- Can view monster motivation, life stages, battle specials, nature, and general "invisible" stats.

-- v0.20:
- Added ability to switch Guts Rate to display in Guts/Second.
- Corrected "Monster Aging Type" to display 1-4 instead of 0-3.
- Added a "Cup Jelly Eaten" counter for Worm main breeds.
- Moved Swim motivation box slightly to align it with the rest.
- Added some Breed Name recognition with stat checking. (Pixie to Jill main breeds, in code order.)
- For breeds added so far, if the growth stats do not match up with the standard data, it will be flagged as a Non-Standard monster. The breed will have "(N/S)" at the end of it.
- Enemy monster breed names are recognised, but will have [E] at the start.
- If a monster likes Tablets, it will have the liked item box shaded in green. If it dislikes Tablets, the disliked item box will be shaded in red.

-- v0.21:
- Cocooning related info is shown if a Worm is the active monster, and is below B Rank.

-- v0.30:
- Only Monols, Apes, Nagas and Worms are missing for breed checking!
- Added a pop-out window to display Fatigue, Stress, Life Index and Lifespan remaining.
- Added a check-box to enable annoying noises for losing/gaining lifespan with Magic Bananas.

-- v0.31:
- /Worms and Beaclon/Beaclon from cocooned Worms now show their original Worm form rather than being a Non-Standard monster.
- Monsters with non-standard growth rates will turn the monster breed box light pink.
- Every monster breed is now recognised! (hopefully.)

-- v0.40:
- Added basic stat tracking.

-- v0.41:
- Added form modification to the stat tracking. This is the number in brackets.

-- v0.50:
- Added Monster Move checking. Currently, it supports Pixies, Dragons and Jokers.

-- v0.501:
- Fixed pSX 1.13 compatibility.
- Fixed a bug where Suezo/Suezo is considered to have a "correct" Guts Rate of 7 for some dumb reason.
- Added Moo (Hard Mode CD) to compatible monsters.

-- v0.501a:
- Fixed Guts Rate and growths for Kato/Suezo, Kato/Mocchi and Kato/Joker.

-- v0.501b:
- Fixed Beaclon/Henger being called Bethelgius. 
- Fixed Scaled Hare being called Scaled Fur.
- Fixed a bug where generated /Worms would either display no name, or display as a non-standard Worm/Worm.
- Fixed Guts Rate and growths for Pixie/Zuum, Centaur/Durahan, Centaur/Joker, Zuum/Golem, Zuum/Baku, Tiger/Zuum, Baku/Durahan, Gali/Golem, Gali/Suezo, Gali/Monol, Jill/Pixie, Plant/Tiger, Monol/Hare, Monol/Suezo.

-- v0.51 Quality of life update:
- Selecting an emulator will automatically attach MR2AV to it, if it is running.
- The program will prompt the user to install .net Framework 4.7.2, and refuse to start without it.
- Move Viewer move icons have been monochromed: This is to save file space, for less confusion with switched attack types on future updates to Hard Mode... and because I may learn how to colour the icons with C#.

-- v0.55:
- Added drug testing. If a monster is using drugs, hover over the drug name to find its effects.
- Added Centaur, Colorpandora, and Beaclon to the Move Viewer.
- Hopefully fixed unregistered /??? monster Guts Rates from showing up as pink thanks to stat wipes on monster generation.
- Corrected Ultrarl being displayed as a Boxer Bajarl.
- As a result of auto-attach from v0.51, the former Attach button is now just a Detach button. It will be locked for two seconds after attaching.

-- v0.60:
- Fixed Move Viewer for previous monsters. (Can Unlock did not work on certain moves.)
- Move Viewer now actually has a Sub-Genus sent to it. Everything is no longer /Pixie!
- Added Henger, Tiger, Hopper, Zilla, Bajarl, Phoenix, Ghost, Metalner, Suezo, Jill, Mock, Plant, Ape and Naga to the Move Viewer.
- Added a proof-of-concept version of an input viewer for Player 1.
- Added potential RetroArch Beetle (Hardware) compatibility.

-- v0.61:
- Changed the Extra Features button to a checkbox, which pops out the side of MR2AV to show more buttons.
- Added a Training Check window. It shows the stat gains from your drills, as well as which drills you get a +1 to.
- Move Viewer now has Worm coded in.
- A sale timer for Verde's Shop has been added.
- A checkbox for when an Errantry sale is available has been added.

-- v0.61a:
- Bugfix for Emulator selection, having realised that SelectedIndex is a feature of ComboBoxes.
- Detaching MR2AV (or failing to attach it) will now reset the Emulator Selection to a blank state.
- Other fixes too minor to note.

-- v0.61a EX:
- Added NO$PSX 2.0 support. Please start Monster Rancher 2 in NO$PSX before attaching MR2AV!
- ...That's literally it for this update. I just wanted to add NO$PSX support so I could help test CD seed data.

-- v0.62 Internal Update:
- Recoded the memory reading functions, allowing for ditching the Memory.dll library!
- Changed the BST display from a text box to a label, and changed some checkboxes to make them highlightable for tooltips.
- Removed RetroArch Beetle support; The core is updated more than MR2AV. :P
- Added the latest version of XEBRA (19/10/02) to the emulator selection.
- Removed ePSXe 1.9.25 and XEBRA 17/07/11, owing to how I lost the attach points for them in the code update. ;w;

-- v0.621:
- There is no E in this update. If you get this joke... you degenerate. :V
- Added an item list viewer.
- Banana Chimes should now be a little more reliable, as they now poll the amount of bananas owned between ticks to confirm usage of a banana.
- Banana Chimes have been shortened to a 1 second length instead of 2 seconds.
- You can now set how frequently MR2AV polls the emulator for information. Base update speed increased to 4x/second.
---- Setting this below 2 ticks/second will disable banana chimes. They start acting glitchy if the ticks are too slow.

-- v0.7 (formerly 0.621 provisional)
- Thanks to SmilingFaces96, I was able to make the effective Nature value work as it is shown in-game.
-- Thanks to this, the Battle Specials wiil now show whether a monster has Power or Anger (or neither) as its active nature special.
- Also thanks to SmilingFaces96 for requesting it, there is also a debugger that lets you view 4 locations in memory of your choosing. Get you that (up to) 16 bytes of voyeurism!
- Fixed attachment error with ePSXe 2.0.5 after migrating from Memory.dll.
- AV can now read information from external text files! Once this is implemented fully, feel free to translate the texts.
- The "Hard Mode" switch has been replaced with a combo-box with four options. This is currently visual only.
-- Yes, I plan to add support for Monster Farm 2 in future.
- Someone has hopefully realised that this numbering scheme is entirely arbitrary.
- Item Viewer now reads item information from a text file. Saving me hundreds of lines of writing, and you... pretty much nothing.
- Support for MR2DX! (but only the latest Steam release; pls support MR if you can, even if KTG are smelly xoxo)
- Move Viewer is finally complete!
- Several fixes for previously unworking 0.621 provisional content, such as the Errantry Sale tickbox and the Shop Sale counter.
- E numbers have been removed from this update!

----------------
-- Mostly Useless Features:
- Attempting to generate a Henger/Gali will give a custom Breed Name of "「プロト」の力わからないwww!" ("[Proto]'s power is unknown LOLOLOL") instead of "Game Crash".
-- ...shit, I need to do something with this now DX actually *gave us* Proto.
- Clicking the "Eff. Nature" label will show the raw nature adjustment value instead.
- Clicking the "Age" label will switch the Age display from "xY, yM, zW" to simply display in weeks.
- There is a 2% chance that the icon for Magic Card (Ghost tech) will change from greyscale to a Yu-Gi-Oh style for 0.5 seconds.
- The entirety of the input viewer. It's really just a proof-of-concept.

---- To-Do:
- Confirm if Errantry Sale Ready is also ready at 0, or only when it hits 255.
- The new Banana Chime method is currently largely untested. Please report any bugs with it!

---- Bugs (as of 0.7)
- /??? monsters (except Moo, slate exclusives, and DNA Capsule monsters) have no stat checking to see if they're non-standard growth stats.
-- They also have no checks for Guts Rate aside from Moo, Ghost and Phoenix.
- Hard Mode generated monsters may be considered non-standard. This will be patched in due time.
-- MR2DX special spawns may also be considered non-standard.
- The mode switcher for MR2AV currently does nothing. Please leave it on MR2 Vanilla. c:
- Attaching MR2AV may load incorrect data from NO$PSX, if the emulator has been opened more than once.
-- In this case: Manually detatch MR2AV, open Task Manager, and close any extra copies of NO$PSX that are running in the background.
- Certain MR2DX /??? breeds may display the wrong name, as the Breed Name is currently based off of the MR2(PS1) genus list.
- Quite a few of the Item Viewer items do not have a listed effect. Mostly because I forgot them.