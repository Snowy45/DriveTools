 
┏━━━┓╋╋╋╋╋╋╋┏━━━━┓╋╋╋╋┏┓╋╋╋╋ ╋┏┓╋┏━━━┓<br>
┗┓┏┓┃╋╋╋╋╋╋╋┃┏┓┏┓┃╋╋╋╋┃┃╋╋╋╋ ┏┛┃╋┃┏━┓┃<br>
╋┃┃┃┣━┳┳┓┏┳━┻┫┃┃┣┻━┳━━┫┃┏━━┓ ┗┓┃╋┃┃┃┃┃<br>
╋┃┃┃┃┏╋┫┗┛┃┃━┫┃┃┃┏┓┃┏┓┃┃┃━━┫ ╋┃┃╋┃┃┃┃┃<br>
┏┛┗┛┃┃┃┣┓┏┫┃━┫┃┃┃┗┛┃┗┛┃┗╋━━┃ ┏┛┗┳┫┗━┛┃<br>
┗━━━┻┛┗┛┗┛┗━━┛┗┛┗━━┻━━┻━┻━━┛ ┗━━┻┻━━━┛<br>
[Creating a new Command](#creating-the-command) | [Registering Commands](#register-the-command) | [List of the Commands](#commands-list)
#
DriveTools is a console application designed to act as a tool to help
you do navigation and more complex operations inside your computer.
With DriveTools you can: 
**navigate thru folders, 
create or delete files,
encrypt / decrypt files,
Send mails containing files,**
And much more to come.

# How to Modify
DriveTools was meant to be very easy to modify and add your own
code on top of it, underneath is a tutorial explaining how to modify 
the code and add your own.
## How to add Custom Commands
### Creating The Command
In order to create a command that would be easy to access and to read,
first of all you will need to create a new file inside the commands folder.
<img src="https://i.ibb.co/kSFmKtz/Create-ACommand-File.jpg" alt="commandCreating"/>
Once you created the command file make it extend the CommandListener class as showed in the example
```
public class TestCommand : CommandListener
```
The **CommandListener** class allows the program to track changes across all commands and allows you
to change the class inteslf and have it update to the other commands, however be 
aware that changing the CommandListener class and the general stracture of the program can make it not
work properly if done poorly

### Register The Command
After you created the command file it won't automatically load into the program
instead you will need to register the command into the event that it uses.<br>
In order to regster the command go to the **Program.cs** file and into the **LoadCommands()** function,
inside add the following lines in order to register the command:<br>
<a href="https://i.ibb.co/b35LwC7/Command-In-Action.png">Command Example</a>

```
 public static void LoadCommands()
 {
      // Commands
      TestCommand testCMD = new TestCommand();  


      //Subscribe to Event
      Drive.DriveCommand += testCMD.OnDriveCommand;
}
```
*Keep in mind to try and use the same naming conventions the program uses*

## Commands List
[Navigation Commands](#navigation-commands) | [Action Commands](#action-commands)<br>
Down below is a list of all the commands currently in the program aswell as explaination and how to use them.
The list will be updated with the program and you can suggest commands that should be added.
### Navigation Commands
expand(ex,exp) - The *ex* command is used to change your current directory. For example: users/documents -> users/documents/MyFolder<br>
**Usage**: ```ex <Directory To Expand> | Example: ex myfolder```
# 
previous directory(pd,predi) - The *pd* command is used in order to go back one directory. For example: users/document/MyFolder -> users/documents<br>
**Usage**: ```pd```
#
back(back) - The *back* command is used to go back to the drives list, this will clean the command history and change your menu.<br>
**Usage**: ```back```
#
files list(fl) - The *fl* command is used to see the files and folders inside your current directory, the command will print a list of the items inside your current directory and the last date they were modified.<br>
**Usage**: ```fl```
#
### Action Commands
create(crt) - the *crt* command is used in order to create new files on the computer, the files will be created in your current directory if you got permission to write into that directory.<br>
**Usage**: ```crt <File/Directory> <Name> | Example: crt -f Notes.txt```
#
delete(del) - the *del* command is used to delete files/directories in your current directory. Be aware that files you delete won't come back and will not appear in trash bin.<br>
**Usage**: ```del <File/Directory> <Name> | Example: del -f Notes.txt```
