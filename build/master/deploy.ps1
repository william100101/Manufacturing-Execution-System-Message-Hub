# Name: Deploy.ps1
#
# Description:  Deploy process for BTE Dashboard 
# 
# Author:  Chris Kostaras
#
# Date: February 2021
#
# Description:
 
 function CallExit($msg)
{
	Write-Host "****** Fatal Error encountered *******";
	Write-Host " $msg";
	Write-Host "exiting now";
	exit	
}

function WriteHeadings([string] $label, [string] $solutionFileName, [string] $view)
{
	Write-Host "Deploy.ps1 starting...";
	Write-Host " ";
	$theDate = Get-Date;
	Write-Host "    Start Time: $theDate";    
	Write-Host "    Environment: $enviroment";
}


function CheckParameters([string[]] $theargs)
{
	if ($theargs.Length -lt 1)
	{
		CallExit("You must provide 1 command line argument.  It must be the target deployment enviroment.  The only options are DEV, QA or PROD");
	}
	$global:env = $theargs[0];
	if (($global:env -eq "DEV") -or ($global:env -eq "QA") -or ($global:env -eq "PROD"))
	{}
	else
	{
		CallExit("The command line arg you provided $global:env is not valid.  The only options are DEV, TEST or PROD");
	}
	
	if ($global:env -eq "DEV")
	{
		$global:appServer = "VMDATAamwappst";
		$global:targetDirectory = "D:\WebApps\SAPDashboard"
	}
	if ($global:env -eq "QA")
	{
		$global:appServer = "VMDATAAMWAPPST";
		$global:targetDirectory = "D:\WebApps\SAPDashboardQA"
	}
	if ($global:env -eq "PROD")
	{
		$global:appServer = "VMDATAAMWAPP";
		$global:targetDirectory = "D:\WebApps\BTEDashboard"
	}
}

function Init()
{
	cls
	Set-PSDebug -strict
	$ReportErrorShowStackTrace = $false
}


function StopTheDefaultWebSite()
{
	Write-Host "Stopping the default website on this server"
	& "c:\windows\system32\inetsrv\appcmd.exe" stop site "Default Web Site"
}

function StartTheDefaultWebSite()
{
	Write-Host "Starting the default website on this server"
	& "c:\windows\system32\inetsrv\appcmd.exe" start site "Default Web Site"
}

function CopyTheFiles()
{
	Write-Host "CopyTheFiles";
	write-host "    Copying the code"
	$webappCompiledCodePath = ".\netcoreapp3.1\*"
	Write-Host "       Copying from $webappCompiledCodePath to $global:targetDirectory"
	copy-item -Path $webappCompiledCodePath -Destination $global:targetDirectory -filter *.* -Recurse -Force
	
	write-host "    Copying the congif"
	$pathToConfigFiles = ".\config\$global:env\*" 
	Write-Host "   Copying from $pathToConfigFiles to $global:targetDirectory"
	copy-item -Path $pathToConfigFiles -Destination $global:targetDirectory -filter "*.*" -Recurse -Force
	write-host "Coying completed."
}


function Main($theArgs)
{
	Init
	CheckParameters $theArgs;
	StopTheDefaultWebSite;    
	CopyTheFiles;
	StartTheDefaultWebSite;    
}

$global:env = "";
$global:appServer = "";
$global:targetDirectory;
main $args;
