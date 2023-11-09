# Accelerometer Sample

This sample shows how to read the accelerometer sensor on Windows Phone 7.

## Sample Overview

Windows Phone 7 devices include a number of hardware sensors, one of which is the accelerometer. The accelerometer can be used to detect tilt, shaking, and other motion in three dimensions. This sample illustrates use of an accelerometer class to handle accelerometer data and update the position of a sprite on the screen.

## Sample Controls

This sample uses the following keyboard and gamepad controls.
| Action      | Windows Phone              | Windows Phone - Emulator                      |
| ----------- | -------------------------- | --------------------------------------------- |
| Move Sprite | Tilt Up, Down, Left, Right | Up Arrow, Down Arrow, Left Arrow, Right Arrow |
| Exit.       | **BACK**                   | **BACK**                                      |

#How the Sample Works

The accelerometer sample utilizes the static Accelerometer class provided in the sample. After calling **Accelerometer.Initialize**, the accelerometer can be polled by calling **Accelerometer.GetState**. In the **Game.Update** function, the X and Y acceleration values are used to update the velocity and position of the sprite so that it slides around the screen as if on an angled surface.

 

When using the Emulator, the controls for manipulating the sprite will not remain consitent when rotating the Emulator. This is because the accelerometer values are not modified inside the emulator to take orientation into account.

## Extending the Sample

A further exploration would be to turn the accelerometer sample into a simple game where the user collects or avoids items by using the accelerometer as input. A similar input mechanic is used in the "Snow Shovel" mini-game.

© 2010 Microsoft Corporation. All rights reserved.