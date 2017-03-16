# ZoomableReference
Make painting with reference much more easier.

basically, this WPF is topmost window with one image element and few buttons, by the time it's become much more: Saving windows, drag-drop, rotation and much more.

# Download
Click on 'Release' or [here](https://github.com/mosheG770/GithubZoomableReference/releases/latest).

than download the Zip file (ZoomableReference.zip).
1. open the zip file and drag all the files in to new folder (I create folder in desktop for that)
2. double click on the file 'ZoomableReference.exe' to start the program.


# How to use:
The program have 3 windows: Manager window, Reference window, and Future window (code name). 

# Manager Window:
![Manager window](/Images/ManagerWindow.PNG?raw=true "Manager Window")

The first window that start is the window manager, when this window close, the program close.

 * 'New Window' - Create new Reference window.
 * 'Close all' - Close all the reference windwos.
 * 'Hide' - Hide all the windows, so you can do something else. (like take a break)
 * 'Show all' - Bring back all the windows.
 * 'Future window' - Create new Future window.
 * 'Soft' - Make all the future windows moveable again (will explain later)
 * 'Save state' - Save the current state of the Reference windows to file, include position and size of the window and the image.
 * 'Load state' - Load state from file, so you can continue from the last you stop.
 * 'Protection' - well, I have program that always make problem when I minimize it, so I make small window on the screen so now I can't click on the Minimize button of that program.
 * **Menu** > Simple (checked) - Simple mode, mean less buttons in reference mode.
 
 
 Move on:
 # Reference window:
 
 ![Reference window](/Images/ReferenceWindow.PNG?raw=true "Reference window")
 
 * '...' - Browse button, where you can select file.
 * 'X' (red) - Close the window.
 * 'URL' - Make field visible so you can paste URL in it and click the 'Load' to load it.
 * 'Move' - Allow the user to drag the window on the screen.
 * 'Reset' - Reset the position and the size of the image in the window to the default values.
 * 'Color' - Change the color of the background of the window to the value of the field below it.
 * 'Hide' - Minimize the window, so you can do somthing else and window won't cover the screen.
 * '<-> - Flip the image (Mirror) horizontal.
 * '^|' (little to the right) - flip the image vertical.
 * 'Rotate' (Checked) - Rotate the image with the mouse wheel.
 
 Mouse and keyboard:
 * Hold and drag left mouse button - Drag the image in the window.
 * Use the mouse wheel - zoom-in and out.
 * hold Left Control (ctrl) - Mouse wheel rotate the image.
 
 You can drag and drop links and files of images on the window and it's will try to show them. (Not everything work yet)
 
 Last one:
 # Future window:
 ![Future window](/Images/FutureWindow.PNG?raw=true "Future window")

this window has no window, it's fullscreen mode, and you can drag the image around and change the opacity.
To make things easier, while you hold the 'left ctrl' you can drag and zoom-in/out anywhere on the screen, things will be more clear if you try it.

the window has 2 states - Solid and Soft, **Solid** mean you can't move the image, zoom or change anything, but you click through the image. **Soft** mean you can move, zoom or even change image.


* 'Solid' - Set the state of the window to 'Solid'
* 'Load' - Browse for image in the window to load on the screen.
* The Slider on the top - Change the opacity.


# Thanks:

[ZoomPan](http://stackoverflow.com/questions/741956/pan-zoom-image)

[DragDrop](http://stackoverflow.com/questions/8442085/receiving-an-image-dragged-from-web-page-to-wpf-window)

[Newtonsoft Json](http://www.newtonsoft.com/json)
