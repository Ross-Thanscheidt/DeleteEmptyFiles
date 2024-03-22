# Delete Empty Files Utility
This is a console utility typically to be run at the Command Prompt that can recursively delete empty files and subfolders given a top folder location.
This will also delete the top folder if there is nothing but empty files and subfolders in it.
Any files that are not empty will not be deleted and will be listed after all the empty files and subfolders have been deleted.

I created this utility because periodically I have file synchronization issues with my cloud storage and end up with a lot of empty files and folders.

## Switches
| Switch | Description |
| :--- | :--- |
| `--recursive` or `-r` | Look in subfolders for empty files and subfolders to delete |
| `--folders` or `-f` | Delete empty subfolders in addition to empty files |
| `--verbose` or `-v` | List the full path of each empty file and subfolder deleted (one per line) |

## Usage

- Delete all empty files/subfolders in or under `TargetFolder`
- Delete `TargetFolder` if all files in and under it are empty
- List the files/subfolders deleted
```bat
C:\> DeleteEmptyFiles --recursive --folders --verbose "C:\Users\username\Desktop\TargetFolder"
```

- Same as before but with shorter switches
```bat
C:\> DeleteEmptyFiles -r -f -v "C:\Users\username\Desktop\TargetFolder"
```

- Delete all empty files/subfolders in or under `TargetFolder`
- Delete `TargetFolder` if all files in and under it are empty
- Do not list the files/subfolders deleted
```bat
C:\> DeleteEmptyFiles -r -f "C:\Path\To\TargetFolder"
```

- Only delete empty files in `TargetFolder`
- Do not delete any empty files in subfolders nor any subfolders
- Delete `TargetFolder` if it has no subfolders and all files in it are empty
- Do not list files deleted
```bat
C:\> DeleteEmptyFiles "C:\Path\To\TargetFolder"
```
