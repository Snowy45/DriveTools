using System;

namespace DriveTools.Comamnds
{
    public class BackCommand : CommandListener
    {

        public void OnDriveCommand(object source, string[] args, string rawInput, string directory)
        {
            if (args[0].ToLower().Equals("back"))
            {
                Drive drive = (Drive)source;
                drive.commandLoop = false;
                DrivesMenu.LoadDrivesMenu();
            }
        }
    }
}
