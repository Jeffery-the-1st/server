﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitizenMP.Server.Commands
{
    class ResourceCommands
    {
        [ConsoleCommand("stop")]
        static void Stop_f(CommandManager manager, string command, string[] args)
        {
            var resourceName = args[0];
            var resourceManager = manager.GameServer.ResourceManager;

            var resource = resourceManager.GetResource(resourceName);

            if (resource == null)
            {
                Game.RconPrint.Print("No such resource: {0}.\n", resourceName);
                return;
            }

            if (resource.State != Resources.ResourceState.Running)
            {
                Game.RconPrint.Print("Resource isn't running: {0}.\n", resourceName);
                return;
            }

            try
            {
                if (!resource.Stop())
                {
                    Game.RconPrint.Print("err stop {0}\n", resourceName);
                    return;
                }

                Game.RconPrint.Print("stop {0}\n", resourceName);
            }
            catch (Exception e)
            {
                resource.Log().Error(() => "Error stopping resource.", e);
                Game.RconPrint.Print("Error stopping resource {0}: {1}.\n", resourceName, e.Message);
            }
        }

        [ConsoleCommand("start")]
        static void Start_f(CommandManager manager, string command, string[] args)
        {
            var resourceName = args[0];
            var resourceManager = manager.GameServer.ResourceManager;

            var resource = resourceManager.GetResource(resourceName);

            if (resource == null)
            {
                Game.RconPrint.Print("No such resource: {0}.\n", resourceName);
                return;
            }

            if (resource.State != Resources.ResourceState.Stopped)
            {
                Game.RconPrint.Print("Resource isn't stopped: {0}.\n", resourceName);
                return;
            }

            try
            {
                if (!resource.Start())
                {
                    Game.RconPrint.Print("err start {0}\n", resourceName);
                    return;
                }

                Game.RconPrint.Print("start {0}\n", resourceName);
            }
            catch (Exception e)
            {
                resource.Log().Error(() => "Error starting resource.", e);
                Game.RconPrint.Print("Error starting resource {0}: {1}.\n", resourceName, e.Message);
            }
        }

        [ConsoleCommand("restart")]
        static void Restart_f(CommandManager manager, string command, string[] args)
        {
            var resourceName = args[0];
            var resourceManager = manager.GameServer.ResourceManager;

            var resource = resourceManager.GetResource(resourceName);

            if (resource == null)
            {
                Game.RconPrint.Print("No such resource: {0}.\n", resourceName);
                return;
            }

            if (resource.State != Resources.ResourceState.Running)
            {
                Game.RconPrint.Print("Resource isn't running: {0}.\n", resourceName);
                return;
            }

            try
            {
                if (!resource.Stop() || !resource.Start())
                {
                    Game.RconPrint.Print("err restart {0}\n", resourceName);

                    return;
                }

                Game.RconPrint.Print("restart {0}\n", resourceName);
            }
            catch (Exception e)
            {
                resource.Log().Error(() => "Error restarting resource.", e);
                Game.RconPrint.Print("Error restarting resource {0}: {1}.\n", resourceName, e.Message);
            }
        }
    }
}
