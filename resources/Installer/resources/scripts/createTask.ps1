param (
    [string]$p1
)

# Validate the path
if (-not (Test-Path -Path "$p1\Checador.exe")) {
    Write-Error "The path '$p1\Checador.exe' does not exist."
    exit 1
}

Write-Host "Creating task ..."
$action = New-ScheduledTaskAction -Execute "$p1\Checador.exe" -Argument "--ch"
$trigger = New-ScheduledTaskTrigger -AtLogOn
$principal = New-ScheduledTaskPrincipal -UserId (Get-CimInstance -ClassName Win32_ComputerSystem | Select-Object -ExpandProperty UserName) -LogonType Interactive -RunLevel Highest
Register-ScheduledTask -Action $action -Trigger $trigger -Principal $principal -TaskName "ChecadorV3" -Description "Runs checador.exe on user logon." -Force

Write-Host "Task created successfully."