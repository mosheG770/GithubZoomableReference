# ZoomableReference
Make painting with reference much more easier.

basically, this WPF is topmost window with one image element and few buttons, by the time it's become much more: Saving windows, drag-drop, rotation and much more.

# Download
Click on 'Release' or [here](https://github.com/mosheG770/GithubZoomableReference/releases/latest).

than download the Zip file (ZoomableReference.zip).
1. open the zip file and drag all the files in to new folder (I create folder in desktop for that)
2. double click on the file 'ZoomableReference.exe' to start the program.


# How to use:
The program have 3 windows: Manager window, Reference window, and Layout window (and setting window). 

# Manager Window:
![Manager window](/Images/ManagerWindow.PNG?raw=true "Manager Window")

The first window that start is the window manager, when this window close, the program close.

 * 'Add windows commands' - those commands applies to all the windows.
 * 'Soft' - Make all the layout windows moveable/editable again (will explain later)
 * 'Save state' - Save the current state of the windows to .zrf file, include position and size of the window and path of the image.
 * 'Load state' - Close open windows and load state from file, so you can continue from the last you stop.
 * 'Save preset' - Save the reference windows without the path to the image in 'Presets' folder in the program path.
 * 'Load preset' - Load preset, and NOT close the windows.
 * **Options** > Settings - open setting manager.
 (Right size of the window:)
 * 'Close' 'Toggle lock' 'Hide' Show' - Apply those commands on selected window in the list.
 * 'Refresh' - Refresh the list to include or exlude windows from the old list.
 * The list - Show all the windows that opened by this manager and show them as path to the image.

 * 'Protection' - I have program that always make problem when I minimize it, so I make small window on the screen so now I can't click on the Minimize button of that program.

 # Reference window:
 
 ![Reference window](/Images/ReferenceWindow.PNG?raw=true "Reference window")
 
 * '...' - Browse button, where you can select file.
 * 'X' (red) - Close the window.
 * 'URL' - Make field visible so you can paste URL in it and click the 'Load' to load it.
 * 'Move' - Allow the user to drag the window on the screen.
 * 'Reset' - Reset the position and the size of the image in the window to the default values.
 * 'Color' - Change the color of the background of the window to the value of the field below it.
 * '<->' 'Rotate'... - Flip the image horizontal or verrical, and allow to rotate the image with the mouse wheel.
 * 'Hide' - Minimize the window, so you can do somthing else and window won't cover the screen.
 * 'Lock'/'Unlock' - Toggle lock of the window. when lock, you can't drag, zoom or rotate the image, also you can't drag or resize the window.
 
 Mouse and keyboard:
 * Hold and drag left mouse button - Drag the image in the window.
 * Use the mouse wheel - zoom-in and out.
 * hold Left Control (ctrl) - Mouse wheel rotate the image.
 
 You can drag and drop links and files of images on the window and it's will try to show them. (Not everything work yet)
 
 # Future window:
 ![Layout window](/Images/FutureWindow.PNG?raw=true "Layout window")

this window has no window, it's fullscreen mode, and you can drag the image around and change the opacity.
To make things easier, while you hold the 'left ctrl' you can drag and zoom-in/out anywhere on the screen, things will be more clear if you try it.

the window has 2 states - Solid and Soft, **Solid** mean you can't move the image, zoom or change anything, but you click through the image. **Soft** mean you can move, zoom or even change image.


* 'X' - Close the window.
* _ - minimize the window.
* 'Solid' - Mouse clicks move through the image to the programs below it.
* 'Load' - Browse for image in the window to load on the screen.
* Slider - Change the opacity.
* flip and rotate buttons are also here.

# Settings window:
![Settings window](/Images/SettingWindow.PNG?raw=true "Settings window")

Here you can change the reference window's mode.

'Bring focus to...' - Choose if the command 'Show' also bring focus of the window.

# Thanks:

[ZoomPan](http://stackoverflow.com/questions/741956/pan-zoom-image)

[DragDrop](http://stackoverflow.com/questions/8442085/receiving-an-image-dragged-from-web-page-to-wpf-window)

[Newtonsoft Json](http://www.newtonsoft.com/json)
