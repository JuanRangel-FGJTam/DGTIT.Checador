$logName = "DGTIT"
$sourceName = "Checador3"

if (-not [System.Diagnostics.EventLog]::SourceExists($sourceName)) {
    New-EventLog -LogName $logName -Source $sourceName
    Write-Host "Event log '$logName' with source '$sourceName' created successfully."
} else {
    Write-Host "The source '$sourceName' already exists and is associated with an event log."
}