param([string]$buildType="Release", [string]$versionSuffix="", [string]$versionPrefix="")

if (-Not ($versionPrefix)){
	$versionPrefix="1.0.$([System.TimeSpan]::FromTicks($([System.DateTime]::UtcNow.Ticks)).Subtract($([System.TimeSpan]::FromTicks(630822816000000000))).TotalDays.ToString().SubString(0,9))"
}

#$versionSuffix = "$versionSuffix+$([System.DateTime]::UtcNow.ToString("yyyyMMdd-HHmmss"))"
$global:vPrefix = $versionPrefix
$global:vSuffix = $versionSuffix
$global:versionPrefix = $versionPrefix

Write-Host $versionPrefix$versionSuffix