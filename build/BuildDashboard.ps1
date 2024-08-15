# Name: Build_all.ps1
#
# Description:  Build process for BTE Dashboard
#
# Author:  Chris Kostaras
#
# Date: February 2021
#
# Description:  Build script for BTE Dashboard
#

function CallExit($msg)
{
	Write-Host "****** Fatal Error encountered *******";
	Write-Host " $msg";
	Write-Host "exiting now";
	exit	
}
#function CompileTheSolution([string] $solution, [string] $buildConfiguration, [string] $logFile)
function CompileTheSolution([string] $solution, [string] $logFile)
{
	$pwd = Get-Location;
#	New-Item -Path $pwd -Name $logFile -ItemType "file" -Value "Build.ps1 Starting compile`r`n" -Force;
	Add-Content -Encoding "ascii" -Path $logFile -Value "`r`n";
	Add-Content -Encoding "ascii" -Path $logFile -Value "Building Solution: $solution`r`n";

#	$processOut = C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319\msbuild.exe $solution "/t:rebuild" "/p:Configuration=$buildConfiguration"; 
	$processOut = dotnet build $solution  
	Add-Content -Encoding "ascii" -Path $logFile -Value $processOut;
	Write-Host "Compiler output: $processOut";
	#"/p:ReferencePath=`"????????`"";
	if ($LastExitCode -ne 0)
	{
		CallExit("Compile Error for $solutionFileName.  Return code: $LastExitCode");
	}
	sleep 5;
}


function WriteHeadings([string] $solution, [string] $config, [string] $logFile)
{
	Write-Host "Build_all.ps1 starting...";
	Write-Host " ";
	$theDate = Get-Date;
	Write-Host "    Start Time: $theDate";    
	write-Host "    Building the following solution: |$solution|" ;
	write-Host "    using log file: |$logFile|" ;
	write-host " ";

}


function CheckParameters($theargs)
{
	if ($theargs.Length -eq 2 )
	{
		# noop
		
	}	
	else
	{
		CallExit("You must provide 2 command line argument.  It must be the CompileTarget and Branch.  Example: Debug Master or Release Master");
	}
}

function Init()
{
	cls
	Set-PSDebug -strict
	$ReportErrorShowStackTrace = $false
}

function RemoveOutputTarget([string] $theBranch)
{
	$pathToBuildFiles = "$rootDirectory\build\$theBranch";
	Write-host "PathToBuildFiles: |$pathToBuildFiles|"
	Write-host "removing $pathToBuildFiles"
#	# delete the output location if you find it.  
	if (Test-Path -path $pathToBuildFiles)
	{
		Remove-Item -Path $pathToBuildFiles -Recurse -Force
	}
	Write-host "removed $pathToBuildFiles"

	New-Item -Path $pwd -Name $theBranch -type directory -Force;
	
}
function BuildZipFile([string] $theBranch)
{
	#  Steps required to build a zip file for deployment
	#  1)  Create a new directory using the branch name under \Build
	#  2)  Copy the various file and directories to the new directory
	#  3)  Zip

	$pwd = Get-Location;
	$pathToBuildFiles = "$rootDirectory\build\$theBranch";
	write-host "pathToBuildFiles |$pathToBuildFiles|";

	$pathToOutput = "$rootDirectory\output\netcoreapp3.1";
	write-host "pathToOutput: |$pathToOutput|";
	
	# create a file that holds the version information
	$versionFile = "version.txt";
	New-Item -Path $pathToOutput -Name $versionFile -type file -Force;

	$buildDate = Get-Date;
	$versionContent = "Build branch: $theBranch`r`nBuild Date: $buildDate";
	set-content -encoding ascii $pathToOutput\$versionFile -value $versionContent; 
#
	# copy the binaries
	copy-item -path $pathToOutput -Destination $pathToBuildFiles -recurse -force 


	#  copy the config dir.
	$pathToConfig = "$rootDirectory\build\config";
	Copy-Item -Path $pathToConfig -Destination $pathToBuildFiles -Recurse -force;

	#  copy the deployscript.
	$pathToDeployScript = "$rootDirectory\build\deploy.ps1";
	Copy-Item -Path $pathToDeployScript -Destination $pathToBuildFiles -Recurse -force;

	
	#  copy the deployscript batch file. DEV
	$pathToDeployScriptBatchDEV = "$rootDirectory\build\run_deploy_dev.bat";
	Copy-Item -Path $pathToDeployScriptBatchDEV -Destination $pathToBuildFiles -Recurse -force;
	$pathToDeployScriptBatchQA = "$rootDirectory\build\run_deploy_QA.bat";
	Copy-Item -Path $pathToDeployScriptBatchQA -Destination $pathToBuildFiles -Recurse -force;
	$pathToDeployScriptBatchPROD = "$rootDirectory\build\run_deploy_PROD.bat";
	Copy-Item -Path $pathToDeployScriptBatchPROD -Destination $pathToBuildFiles -Recurse -force;
	
	# $buildZipDirectory = "BuildZip";
	# $pathToPutBuildZip = "$rootDirectory\build";
	# New-Item -Path $pathToPutBuildZip -Name $buildZipDirectory -type directory -Force;
	
	# $zipfile = "$buildZipDirectory\$branch.zip";
	# # delete the zip file 
	# if (Test-Path -Path $zipfile)
	# {	
		# Remove-Item -Path $zipfile;
	# }

#	$winzipexe = "c:\Program Files\winzip\wzzip.exe";
#	$zipParm = "-p -r $zipfile $pathToBuildFiles"; 
#	$process = [Diagnostics.Process]::Start("$winzipexe", "$zipParm")
#	Write-Host "Building zip file";
#	while (!($process.HasExited))
#	{
#		Write-Host ".";
#		sleep 5;
#	}
 
}	

function BuildSolution([string] $solution, [string] $buildConfiguration, [string] $logFile)
{
	WriteHeadings $solution $buildConfiguration $logFile;
	CompileTheSolution $solution $buildConfiguration $logFile;
}

$rootDirectory = Split-Path -Path $pwd -Parent
write-host "Root Directory = $rootDirectory"

function Main($theArgs)
{
	Init
#	CheckParameters $theArgs;
	$buildConfiguration = $theArgs[0];	
	$theBranch = $theArgs[1];
	$logFile = "$theBranch" + "_build.log";
	if (Test-Path -path $logFile)
	{
	   Remove-Item -path $logFile -force;
	}
	
	RemoveOutputTarget $theBranch
	
	$solution = "c:\repos\SAPDashboard\SAPDashboard.sln";
	WriteHeadings $solution $buildConfiguration, $logFile	
 	BuildSolution $solution $buildConfiguration $logFile
	write-host "Build of solution $solution completed"

	BuildZipFile $theBranch
#	WriteHeadings $solution $buildConfiguration $theBranch;
#	CompileTheSolution $solution $buildConfiguration $theBranch;
#	BuildZipFile $theBranch;
}


main $args;
