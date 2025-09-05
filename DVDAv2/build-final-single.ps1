# Simple Working Single Executable Creator
# Creates a single file that actually works

param(
    [string]$Configuration = "Release"
)

Write-Host "Creating WORKING Single Executable..." -ForegroundColor Green

$binDir = "bin\$Configuration"
if (-not (Test-Path $binDir)) {
    Write-Host "ERROR: Build output not found. Please build project first!" -ForegroundColor Red
    exit 1
}

$finalExe = "ShopVault-FINAL-SingleFile.exe"
$workDir = "temp-single-build"

# Clean up
if (Test-Path $workDir) { Remove-Item $workDir -Recurse -Force }
if (Test-Path $finalExe) { Remove-Item $finalExe -Force }
New-Item -ItemType Directory -Path $workDir | Out-Null

Write-Host "Step 1: Copying all required files..." -ForegroundColor Cyan

# Copy main executable
$mainExe = "$binDir\ShopVault-VulnerableApp.exe"
if (Test-Path $mainExe) {
    Copy-Item $mainExe "$workDir\ShopVault.exe"
    Write-Host "  ✓ Main executable" -ForegroundColor Green
} else {
    Write-Host "  ❌ Main executable not found!" -ForegroundColor Red
    exit 1
}

# Copy config file
$configFile = "$binDir\ShopVault-VulnerableApp.exe.config" 
if (Test-Path $configFile) {
    Copy-Item $configFile "$workDir\ShopVault.exe.config"
    Write-Host "  ✓ Config file" -ForegroundColor Green
}

# Copy all DLLs
$dlls = Get-ChildItem "$binDir\*.dll"
foreach ($dll in $dlls) {
    Copy-Item $dll.FullName $workDir\
    Write-Host "  ✓ $($dll.Name)" -ForegroundColor Green
}

# Copy native directories
$nativeDirs = @("x64", "x86")
foreach ($dir in $nativeDirs) {
    $sourcePath = "$binDir\$dir"
    if (Test-Path $sourcePath) {
        Copy-Item $sourcePath "$workDir\" -Recurse
        Write-Host "  ✓ $dir native libraries" -ForegroundColor Green
    }
}

# Copy or create database
if (Test-Path "vulnmart.db") {
    Copy-Item "vulnmart.db" "$workDir\"
    Write-Host "  ✓ Database file" -ForegroundColor Green
}

Write-Host ""
Write-Host "Step 2: Creating self-extracting executable..." -ForegroundColor Cyan

# Create a simple batch self-extractor
$batchContent = @'
@echo off
title ShopVault - Vulnerable Application Launcher

color 0C
echo.
echo    ███████╗██╗  ██╗ ██████╗ ██████╗ ██╗   ██╗ █████╗ ██║   ██║██║  ████████╗
echo    ██╔════╝██║  ██║██╔═══██╗██╔══██╗██║   ██║██╔══██╗██║   ██║██║  ╚══██╔══╝
echo    ███████╗███████║██║   ██║██████╔╝██║   ██║███████║██║   ██║██║     ██║   
echo    ╚════██║██╔══██║██║   ██║██╔═══╝ ╚██╗ ██╔╝██╔══██║██║   ██║██║     ██║   
echo    ███████║██║  ██║╚██████╔╝██║      ╚████╔╝ ██║  ██║╚██████╔╝███████╗██║   
echo    ╚══════╝╚═╝  ╚═╝ ╚═════╝ ╚═╝       ╚═══╝  ╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝   
echo.
color 0E
echo                        VULNERABLE DESKTOP APPLICATION
echo                             FOR EDUCATIONAL USE ONLY
echo.
color 0C
echo  ⚠️  WARNING: This application contains REAL security vulnerabilities!
echo.
echo     VULNERABILITIES INCLUDED:
echo     • SQL Injection (try: admin' OR '1'='1' --)
echo     • Registry Session Storage
echo     • Plain Text Password Storage  
echo     • Command Injection
echo     • And more...
echo.
color 0A
echo  🎯 TEST CREDENTIALS: admin/admin123
echo  🔍 After login: reg query "HKCU\SOFTWARE\DVDA"
echo.
color 0F

set TEMP_DIR=%TEMP%\ShopVault_%RANDOM%
if exist "%TEMP_DIR%" rmdir /s /q "%TEMP_DIR%"
mkdir "%TEMP_DIR%"

echo Extracting application files...
powershell -Command "& { Add-Type -AssemblyName System.IO.Compression.FileSystem; $bytes = [System.IO.File]::ReadAllBytes('%~f0'); $marker = [System.Text.Encoding]::ASCII.GetBytes('PAYLOAD_START'); for($i=0; $i -lt $bytes.Length-$marker.Length; $i++) { $match = $true; for($j=0; $j -lt $marker.Length; $j++) { if($bytes[$i+$j] -ne $marker[$j]) { $match = $false; break } } if($match) { $zipBytes = $bytes[($i+$marker.Length)..($bytes.Length-1)]; [System.IO.File]::WriteAllBytes('%TEMP_DIR%\app.zip', $zipBytes); [System.IO.Compression.ZipFile]::ExtractToDirectory('%TEMP_DIR%\app.zip', '%TEMP_DIR%'); break } } }"

if exist "%TEMP_DIR%\ShopVault.exe" (
    echo.
    echo Starting ShopVault...
    cd /d "%TEMP_DIR%"
    start "" "%TEMP_DIR%\ShopVault.exe"
    echo.
    echo Application started! This window will close in 5 seconds...
    timeout /t 5 >nul
) else (
    color 0C
    echo.
    echo ERROR: Failed to extract application!
    pause
)
exit

PAYLOAD_START
'@

$batchFile = "$workDir\launcher.bat"
$batchContent | Out-File $batchFile -Encoding ASCII

Write-Host "Step 3: Creating ZIP archive..." -ForegroundColor Cyan

# Create ZIP using PowerShell
$zipFile = "$workDir\payload.zip"
Add-Type -AssemblyName System.IO.Compression.FileSystem

$filesToZip = Get-ChildItem $workDir -Exclude "launcher.bat", "payload.zip"
$zip = [System.IO.Compression.ZipFile]::Open($zipFile, [System.IO.Compression.ZipArchiveMode]::Create)

try {
    foreach ($item in $filesToZip) {
        if ($item.PSIsContainer) {
            # Directory
            $dirFiles = Get-ChildItem $item.FullName -Recurse -File
            foreach ($file in $dirFiles) {
                $relativePath = $file.FullName.Substring($workDir.Length + 1)
                $entry = $zip.CreateEntry($relativePath)
                $entryStream = $entry.Open()
                $fileStream = [System.IO.File]::OpenRead($file.FullName)
                $fileStream.CopyTo($entryStream)
                $fileStream.Close()
                $entryStream.Close()
            }
        } else {
            # File
            $entry = $zip.CreateEntry($item.Name)
            $entryStream = $entry.Open()
            $fileStream = [System.IO.File]::OpenRead($item.FullName)
            $fileStream.CopyTo($entryStream)
            $fileStream.Close()
            $entryStream.Close()
        }
    }
} finally {
    $zip.Dispose()
}

Write-Host "Step 4: Combining launcher with payload..." -ForegroundColor Cyan

# Combine batch file with ZIP
$batchBytes = [System.IO.File]::ReadAllBytes($batchFile)
$zipBytes = [System.IO.File]::ReadAllBytes($zipFile)
$combined = $batchBytes + $zipBytes

[System.IO.File]::WriteAllBytes($finalExe, $combined)

# Cleanup
Remove-Item $workDir -Recurse -Force

if (Test-Path $finalExe) {
    $fileSize = (Get-Item $finalExe).Length / 1MB
    Write-Host ""
    Write-Host "🎉 SUCCESS! Single Executable Created!" -ForegroundColor Green
    Write-Host "File: $finalExe ($([math]::Round($fileSize, 1)) MB)" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "✅ WHAT YOU GET:" -ForegroundColor Yellow
    Write-Host "• ONE file with everything embedded" -ForegroundColor White
    Write-Host "• No dependencies needed" -ForegroundColor White
    Write-Host "• Self-extracting with nice banner" -ForegroundColor White
    Write-Host "• All vulnerabilities working" -ForegroundColor White
    Write-Host ""
    Write-Host "🔧 TO TEST:" -ForegroundColor Yellow  
    Write-Host "1. Double-click: $finalExe" -ForegroundColor White
    Write-Host "2. Use SQL injection: admin' OR '1'='1' --" -ForegroundColor White
    Write-Host "3. Check registry: reg query `"HKCU\SOFTWARE\DVDA`"" -ForegroundColor White
    Write-Host ""
    Write-Host "🚀 Ready for students!" -ForegroundColor Green
} else {
    Write-Host "❌ Failed to create executable!" -ForegroundColor Red
    exit 1
}
