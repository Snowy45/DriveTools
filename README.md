 
┏━━━┓╋╋╋╋╋╋╋┏━━━━┓╋╋╋╋┏┓╋╋╋╋ ╋┏┓╋┏━━━┓<br>
┗┓┏┓┃╋╋╋╋╋╋╋┃┏┓┏┓┃╋╋╋╋┃┃╋╋╋╋ ┏┛┃╋┃┏━┓┃<br>
╋┃┃┃┣━┳┳┓┏┳━┻┫┃┃┣┻━┳━━┫┃┏━━┓ ┗┓┃╋┃┃┃┃┃<br>
╋┃┃┃┃┏╋┫┗┛┃┃━┫┃┃┃┏┓┃┏┓┃┃┃━━┫ ╋┃┃╋┃┃┃┃┃<br>
┏┛┗┛┃┃┃┣┓┏┫┃━┫┃┃┃┗┛┃┗┛┃┗╋━━┃ ┏┛┗┳┫┗━┛┃<br>
┗━━━┻┛┗┛┗┛┗━━┛┗┛┗━━┻━━┻━┻━━┛ ┗━━┻┻━━━┛<br>
[Creating a new Command](#creating-the-command) | [Registering Commands](#register-the-command)
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


