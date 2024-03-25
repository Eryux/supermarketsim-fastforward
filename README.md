# FastForward for Supermarket Simulator

FastForward is a mod for Supermarket Simulator, it allows to increase or decrease game speed.
No more eternal waiting for restocker, customers and days to pass.


## How it works

On the game, after installing the mod, press :

- F4 to increase game speed
  
- F2 to decrease game speed
  
- F3 to reset speed to default (x1)

Game speed is written at the top right corner of your screen next to day time.

You can change the shortcuts and speeds available in the mod's configuration file.


## Installation

### Requirements

- Compatible version of Supermarket Simulator
- BepInEx 5.x ([download](https://github.com/BepInEx/BepInEx/releases) and [installation guide](https://docs.bepinex.dev/articles/user_guide/installation/index.html))


### Steps

- Install BepInEx 5 on the game if you don't already have it.
- Open `BepInEx.cfg` and set `HideManagerGameObject` to `true` then save and close the file. (not needed if you use pre-configured Tobey's BepInEx for Supermarker Simulator)
- Download the latest release of the mod compatible with your game version in [release section](https://github.com/Eryux/supermarketsim-fastforward/releases).
- Extract the archive and copy the `BepInEx` folder from archive in the game root folder `%steamapp%\common\Supermarket Simulator`. If it asks you to replaces files, say yes to all.
- Launch the game, continue or create a new game, if the mod is correctly installed you will have the game speed at the top right corner of the screen next to day time.


### Configuration

Mod configuration is available in `config/tf.bark.sms.FastForward.cfg`. The change will apply after restarting the game.

```toml
## Settings file was created by plugin FastForward v1.0.0
## Plugin GUID: tf.bark.sms.FastForward

[FastForward]

## Available speeds
# Setting type: Single[]
# Default value: [ 0.5,1,1.5,2,3,5,10 ]
game_speed = [ 0.5,1,1.5,2,3,5,10 ]

[FastForward.Shortcut]

## Decrease Game speed
# Setting type: KeyboardShortcut
# Default value: F2
decrease_speed = F2

## Increase Game speed
# Setting type: KeyboardShortcut
# Default value: F4
increase_speed = F4

## Reset Game speed
# Setting type: KeyboardShortcut
# Default value: F3
reset_speed = F3
```

## Troubleshooting

If you encounter a bug, please report it in [issues section](https://github.com/Eryux/supermarketsim-fastforward/issues) on the official [github repository](https://github.com/Eryux/supermarketsim-fastforward/) from the mod.

### Incompatibility with other mods

The mod act `Time.timeScale` of Unity Engine and player interaction. If you have other mod acting on one of those or more it can cause some incompatibility. If an incompatibility is detected with other mods it will be listed here.


## Building

### Requirement

- .NET SDK 7.0.407
- Supermarket Simulator installed

### Steps

- Clone the repository on your machine.
- Open the `FastForward.csproj` file with a text editor and edit the path `HintPath` from `Reference` tags to match your game installation directory.
- Open a terminal and run the command :
```
dotnet build --no-self-contained -c Release
```

## License

GPLv3 License