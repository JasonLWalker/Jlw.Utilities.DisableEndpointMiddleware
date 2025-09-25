param([string]$sourcePath, [string]$outputPath)

Write-Host "===================== Generating Documentation Markdown ====================="
dotnet tool install xmldoc2markdown
dotnet xmldoc2md $sourcePath\Jlw.Utilities.DisableEndpointMiddleware.dll --output $outputPath\Jlw.Utilities.DisableEndpointMiddleware --member-accessibility-level public --back-button --index-page-name README
