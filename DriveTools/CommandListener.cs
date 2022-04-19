using System;
namespace DriveTools
{
    interface CommandListener
    {
        void OnDriveCommand(object source, string[] args, string rawInput, string directory);

    }
}
