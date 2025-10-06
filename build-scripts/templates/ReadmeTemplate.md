<!-- $(
	## Add Poweshell template variables Here ##
	$projectName = "Jlw.Utilities.DisableEndpointMiddleware"
) -->
# $projectName

A utility package providing the ability to block endpoint URLs in the middleware pipeline. 
This is useful for disabling endpoints in production that are only intended for use in development or testing environments, 
or to disable unused RCL endpoints in packages such as the Microsoft Identity UI.

## Pipeline Status

| Test | Alpha | Staging | Release |
|-----|-----|-----|-----|
| [![Build and Test](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-test.yml/badge.svg)](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-test.yml) | [![Build and Deploy - Alpha](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy-alpha.yml/badge.svg)](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy-alpha.yml) | [![Build and Deploy - RC](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy-rc.yml/badge.svg?branch=staging)](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy-rc.yml) |[![Build and Deploy](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy.yml/badge.svg)](https://github.com/JasonLWalker/$($projectName)/actions/workflows/build-deploy.yml) | 


# Middleware to disable endpoints in ASP.NET Core applications.
<!-- $( 
	$projectPath = "$($buildPath)**\$($projectName).csproj"
) -->
[![Nuget](https://img.shields.io/nuget/v/$($projectName)?label=$($projectName)%20%28release%29)](https://www.nuget.org/packages/$($projectName)/#versions-body-tab) [![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/$($projectName)?label=$($projectName)%20%28preview%29)](https://www.nuget.org/packages/$($projectName)/#versions-body-tab)

## Information / Requirements
$(Get-ProjectInfoTable $projectName $projectPath)

## Dependencies

$(Get-ProjectDependencyTable $projectPath)
