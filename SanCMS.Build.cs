// Copyright 1998-2016 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;
using System;

public class SanCMS : ModuleRules
{
	public SanCMS(TargetInfo Target)
	{
		PublicIncludePaths.Add("Runtime/Launch/Public");

		PrivateIncludePaths.Add("Runtime/Launch/Private");      // For LaunchEngineLoop.cpp include

        PrivateIncludePaths.AddRange(new string[]
        {
            Path.Combine(ThirdPartyPath, "v8", "include"),
            Path.Combine(ThirdPartyPath, "v8"),
            Path.Combine("V8", "Private")
        });

        PublicIncludePaths.AddRange(new string[]
        {
            Path.Combine("V8", "Public")
        });

        PrivateDependencyModuleNames.Add("Core");
		PrivateDependencyModuleNames.Add("Projects");
        LoadV8(Target);
    }


    private string ThirdPartyPath
    {
        get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "ThirdParty")); }
    }


    public bool LoadV8(TargetInfo Target)
    {
        bool isLibrarySupported = false;

        if ((Target.Platform == UnrealTargetPlatform.Win64) || (Target.Platform == UnrealTargetPlatform.Win32))
        {
            isLibrarySupported = true;

            string LibrariesPath = Path.Combine(ThirdPartyPath, "v8", "lib");

            if (Target.Platform == UnrealTargetPlatform.Win64)
            {
                LibrariesPath = Path.Combine(LibrariesPath, "Win64");
            }
            else
            {
                LibrariesPath = Path.Combine(LibrariesPath, "Win32");
            }

            if (Target.Configuration == UnrealTargetConfiguration.Debug)
            {
                LibrariesPath = Path.Combine(LibrariesPath, "Debug");
            }
            else
            {
                LibrariesPath = Path.Combine(LibrariesPath, "Release");
            }
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_base_0.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_base_1.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_base_2.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_base_3.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_libbase.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_libplatform.lib"));
            PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "v8_nosnapshot.lib"));

            Definitions.Add(string.Format("WITH_V8=1"));
            Definitions.Add(string.Format("WITH_V8_FAST_CALL=0"));
            Definitions.Add(string.Format("WITH_JSWEBSOCKET=1"));

           
        }
        else
        {

            Definitions.Add(string.Format("WITH_V8=0"));
            Definitions.Add(string.Format("WITH_V8_FAST_CALL=0"));
            Definitions.Add(string.Format("WITH_JSWEBSOCKET=0"));

        }



        return isLibrarySupported;
    }
}
