# ContainerReader
A command line tool to parse and print information about Windows UWP containers.index files. These files, located in AppData/Local/Packages/packagename/SystemAppData/, contain information about the settings of UWP apps, and game saves of UWP games. Without parsing this file, it is hard to tell what the files are truly named, because the file and folder names are GUIDs. This fixes that problem and should make editing UWP app settings and game saves easier. Some UWP game titles, such as Gears of War 4, use this system to store their saves.

## Usage
ContainerReader containers.index

## Restrictions
Some types of containers.index files are not supported by this program yet. You will know if it is not supported - it will tell you. The most common type, 0xD, is fully supported.

I plan on adding support for all types, as well as containers.\# files, which enumerate files inside a folder. These are *extremely* simple so I can add support quickly.

## Pull requests?
Feel free. If you feel you can improve this, do not hesitate to submit a pull request.
