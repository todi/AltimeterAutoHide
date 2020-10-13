# AltimeterAutoHide

Supported KSP Versions: 1.8.0 - 1.10.x

A small mod for KSP that hides the altimeter during flight to save screen estate.

Move your mouse to the top edge (where the altimeter usually is) to access the recover/abort/gear/etc buttons (See demo image below).
You can temporarily disable the auto-hide function by right-clicking on the altimeter. Right-click again to reenable it.

I recommend using mods like [Kerbal Engineer Redux](https://github.com/jrbudda/KerbalEngineer), [MechJeb](https://github.com/MuMech/MechJeb2) or [Speed Unit Annex](https://forum.kerbalspaceprogram.com/index.php?/topic/169611-*) to display the altitude information in a more space-efficient manner. If you need even more space: [QuickHide](https://forum.kerbalspaceprogram.com/index.php?/topic/174445-*) does a similar thing for the stock toolbar and the staging list.
Works also well with [Draggable Altimeter](https://github.com/andrew-vant/dragalt).

![usage example](https://raw.githubusercontent.com/todi/AltimeterAutoHide/master/demo.gif)

## Building

Use the provided csproj file to open the project in Visual Studio. Change the references so they point to your KSP install.

## Installing

The easiest way is to install it with CKAN.

For manual installation download the latest release for your version of KSP and unzip the AltimeterAutoHide folder into your GameData directory.

## Configuration

There is no in-game configuration GUI. To change settings you have to edit `GameData/AltimeterAutoHide/PluginData/AltimeterAutoHide`.

 - `activationPadding`: number of pixels the activation area is extended beyond the altimeters dimensions.
 - `stickyOnLoad`: setting this to 1 will show the altimeter by default on flight scene load. Right-click it to enable auto-hide. 

## Links

 - [KSP Forum Thread](https://forum.kerbalspaceprogram.com/index.php?/topic/197164-1101-altimeterautohide-v10/)
 - [SpaceDock Page](https://spacedock.info/mod/2541/AltimeterAutoHide)
 - [Releases](https://github.com/todi/AltimeterAutoHide/releases)
 - [Issues](https://github.com/todi/AltimeterAutoHide/issues)
 - [Source](https://github.com/todi/AltimeterAutoHide)
